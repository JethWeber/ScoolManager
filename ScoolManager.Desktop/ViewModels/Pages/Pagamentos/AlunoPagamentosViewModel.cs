using Avalonia.Input.Platform;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Core.Enums;
using ScoolManager.Core.Services.Alunos;
using ScoolManager.Desktop.Services;

namespace ScoolManager.Desktop.ViewModels.Pages.Pagamentos
{
    public enum CategoriaPagamento
    {
        Propina,
        Cartao,
        Prova,
        Uniforme,
        Outro
    }

    /// <summary>Dados do pagamento confirmado, para quem escuta (DetalhesAlunoViewModel).</summary>
    public sealed class PagamentoRealizadoEventArgs : EventArgs
    {
        public CategoriaPagamento Categoria { get; }
        public string Descricao { get; }
        public string NumeroRecibo { get; }
        public decimal Valor { get; }
        public DateTime Data { get; }

        /// <summary>Nº de referências pagas nesta transação (ex.: nº de meses, para Propina). 1 nas restantes categorias.</summary>
        public int QuantidadeReferencias { get; }

        public PagamentoRealizadoEventArgs(CategoriaPagamento categoria, string descricao, string numeroRecibo,
            decimal valor, DateTime data, int quantidadeReferencias = 1)
        {
            Categoria = categoria;
            Descricao = descricao;
            NumeroRecibo = numeroRecibo;
            Valor = valor;
            Data = data;
            QuantidadeReferencias = quantidadeReferencias;
        }
    }

    /// <summary>Item de mês selecionável na categoria "Propina" (chip com toggle).</summary>
    public partial class MesSelecionavelItem : ObservableObject
    {
        public string Nome { get; }
        public int NumeroMes { get; }
        [ObservableProperty] private bool _selecionado;
        [ObservableProperty] private bool _disponivel = true;

        public MesSelecionavelItem(string nome, int numeroMes, bool selecionado = false)
        {
            Nome = nome;
            NumeroMes = numeroMes;
            Selecionado = selecionado;
        }


    }

    /// <summary>
    /// ViewModel do fluxo "Efetuar Pagamento" (View 3 - Detalhes do Aluno), totalmente separada
    /// de <see cref="DetalhesAlunoViewModel"/> para não a poluir com 5 formulários diferentes.
    ///
    /// Fluxo:
    ///   1) DetalhesAlunoViewModel chama SetAluno(...) e AbrirCommand -> mostra a "Legenda dos Tipos de Pagamento".
    ///   2) Utilizador escolhe uma categoria (SelecionarCategoriaCommand) -> mostra o formulário respetivo.
    ///   3) ConfirmarPagamentoCommand valida, dispara o evento PagamentoConfirmado e fecha o modal.
    ///
    /// DetalhesAlunoViewModel apenas subscreve PagamentoConfirmado para atualizar o
    /// histórico financeiro / saldo devedor - não conhece os detalhes de cada categoria.
    ///
    /// Ano Lectivo e Classe (categoria Propina) deixaram de ser texto livre: vêm do
    /// ScoolManager.Core (IEscolaService) via CarregarOpcoesAsync, chamado pelo "pai"
    /// (DetalhesAlunoViewModel.InitializeAsync). Nome/Código/Ano Lectivo/Classe do aluno
    /// continuam a ser apenas exibidos - nunca editáveis pelo utilizador.
    /// </summary>
    public partial class AlunoPagamentosViewModel : ViewModelBase
    {
        // ===== Identificação do aluno (definida pelo "pai" antes de abrir) =====
        [ObservableProperty] private string _nomeEstudante = string.Empty;
        [ObservableProperty] private string _codigoMatricula = string.Empty;
        private readonly IFinanceiroService? _financeiroService;
        private readonly int _alunoId;
        private readonly IReciboPagamentoService? _reciboService;
        [ObservableProperty] private string? _erroConfirmacao;
        [ObservableProperty] private bool _isSucessoAberto;
        [ObservableProperty] private string _sucessoMensagem = "Pagamento realizado com sucesso!";
        [ObservableProperty] private string _sucessoDetalhe = string.Empty;

        public bool TemErroConfirmacao => !string.IsNullOrWhiteSpace(ErroConfirmacao);

        // ===== Estado do fluxo =====
        [ObservableProperty] private bool _isAberto;
        [ObservableProperty] private CategoriaPagamento? _categoriaSelecionada;

        public bool IsLegendaAberta => IsAberto && CategoriaSelecionada is null;
        public bool IsFormularioAberto => IsAberto && CategoriaSelecionada is not null;

        public bool IsPropinaAberta => CategoriaSelecionada == CategoriaPagamento.Propina;
        public bool IsCartaoAberto => CategoriaSelecionada == CategoriaPagamento.Cartao;
        public bool IsProvaAberta => CategoriaSelecionada == CategoriaPagamento.Prova;
        public bool IsUniformeAberto => CategoriaSelecionada == CategoriaPagamento.Uniforme;
        public bool IsOutroAberto => CategoriaSelecionada == CategoriaPagamento.Outro;

        partial void OnIsAbertoChanged(bool value)
        {
            OnPropertyChanged(nameof(IsLegendaAberta));
            OnPropertyChanged(nameof(IsFormularioAberto));
        }

        partial void OnCategoriaSelecionadaChanged(CategoriaPagamento? value)
        {
            OnPropertyChanged(nameof(IsLegendaAberta));
            OnPropertyChanged(nameof(IsFormularioAberto));
            OnPropertyChanged(nameof(IsPropinaAberta));
            OnPropertyChanged(nameof(IsCartaoAberto));
            OnPropertyChanged(nameof(IsProvaAberta));
            OnPropertyChanged(nameof(IsUniformeAberto));
            OnPropertyChanged(nameof(IsOutroAberto));
            RecalcularSubtotal();
        }

        // ===== Campos comuns (coluna "Método" + "Resumo") =====
        [ObservableProperty] private string? _formaPagamento;
        [ObservableProperty] private string? _numeroTalao;
        [ObservableProperty] private string? _descricaoAdicional;

        [ObservableProperty] private decimal _subtotal;
        [ObservableProperty] private decimal _multasJuros;
        [ObservableProperty] private decimal _desconto;

        public decimal Total => Subtotal + MultasJuros - Desconto;
        public string SubtotalLabel => FormatKz(Subtotal);
        public string MultasJurosLabel => FormatKz(MultasJuros);
        public string DescontoLabel => "-" + FormatKz(Desconto);
        public string TotalLabel => FormatKz(Total);

        public string[] FormasPagamento { get; } = { "Dinheiro", "Transferência Bancária", "TPA / Multicaixa" };

        partial void OnSubtotalChanged(decimal value) => AtualizarTotais();
        partial void OnMultasJurosChanged(decimal value) => AtualizarTotais();
        partial void OnDescontoChanged(decimal value) => AtualizarTotais();
        partial void OnFormaPagamentoChanged(string? value) => ConfirmarPagamentoCommand.NotifyCanExecuteChanged();
        partial void OnErroConfirmacaoChanged(string? value) => OnPropertyChanged(nameof(TemErroConfirmacao));
        partial void OnDescricaoOutroChanged(string? value) => ConfirmarPagamentoCommand.NotifyCanExecuteChanged();

        private void AtualizarTotais()
        {
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(SubtotalLabel));
            OnPropertyChanged(nameof(MultasJurosLabel));
            OnPropertyChanged(nameof(DescontoLabel));
            OnPropertyChanged(nameof(TotalLabel));
            ConfirmarPagamentoCommand.NotifyCanExecuteChanged();
        }

        private static string FormatKz(decimal valor) =>
            valor.ToString("N2", CultureInfo.GetCultureInfo("pt-PT")) + " Kz";

        // ===== Categoria: PROPINA =====
        // TODO: substituir por valor de mensalidade vindo do plano/curso do aluno quando existir no Core.
        private const decimal ValorMensalidade = 15000m;

        public ObservableCollection<MesSelecionavelItem> MesesDisponiveis { get; } = new(
            new[]
            {
                ("Setembro", 9), ("Outubro", 10), ("Novembro", 11), ("Dezembro", 12),
                ("Janeiro", 1), ("Fevereiro", 2), ("Março", 3), ("Abril", 4), ("Maio", 5), ("Junho", 6)
            }.Select(m => new MesSelecionavelItem(m.Item1, m.Item2)));

        /// <summary>Opções do combobox "Ano Lectivo", carregadas do Core (ver CarregarOpcoesAsync).</summary>
        public ObservableCollection<string> AnosLectivosDisponiveis { get; } = new();

        /// <summary>Opções do combobox "Classe", carregadas do Core (ver CarregarOpcoesAsync).</summary>
        public ObservableCollection<string> ClassesDisponiveis { get; } = new();

        private readonly HashSet<(int Ano, int Mes)> _mesesPropinaPagos = new();

        [ObservableProperty] private string _anoLectivoPropina = string.Empty;
        [ObservableProperty] private string? _classePropina;

        public string MesesSelecionadosLabel
        {
            get
            {
                var n = MesesDisponiveis.Count(m => m.Selecionado);
                return n switch
                {
                    0 => "Nenhum mês selecionado",
                    1 => "1 mês selecionado",
                    _ => $"{n} meses selecionados"
                };
            }
        }

        [RelayCommand]
        private void AlternarMes(MesSelecionavelItem? mes)
        {
            if (mes is null) return;
            if (!mes.Disponivel)
            {
                ErroConfirmacao = $"Não é possível pagar {mes.Nome} porque existem meses anteriores de propina por regularizar.";
                return;
            }

            mes.Selecionado = !mes.Selecionado;
            ErroConfirmacao = null;
            AtualizarDisponibilidadeMeses();
            OnPropertyChanged(nameof(MesesSelecionadosLabel));
            RecalcularSubtotal();
        }

        // ===== Categoria: CARTÃO =====
        public string[] TiposCartao { get; } = { "Cartão de Estudante", "Cartão de Biblioteca" };
        public string[] MotivosCartao { get; } = { "1ª Via", "2ª Via (Perda)", "2ª Via (Dano)" };

        [ObservableProperty] private string? _tipoCartao;
        [ObservableProperty] private string? _motivoCartao;

        partial void OnMotivoCartaoChanged(string? value) => RecalcularSubtotal();

        // ===== Categoria: PROVA =====
        public string[] TiposProva { get; } = { "Exame Normal", "Exame de Recuperação", "Exame Especial" };
        public string[] Disciplinas { get; } =
            { "Matemática", "Português", "Física", "Química", "Biologia", "História", "Geografia", "Inglês" };

        [ObservableProperty] private string? _tipoProva;
        [ObservableProperty] private string? _disciplinaProva;

        partial void OnTipoProvaChanged(string? value) => RecalcularSubtotal();

        // ===== Categoria: UNIFORME =====
        public string[] TiposUniforme { get; } = { "Educação Física", "Uniforme Escolar Completo" };
        public string[] Tamanhos { get; } = { "PP", "P", "M", "G", "GG" };

        [ObservableProperty] private string? _tipoUniforme;
        [ObservableProperty] private string? _tamanhoUniforme;

        partial void OnTipoUniformeChanged(string? value) => RecalcularSubtotal();

        // ===== Categoria: OUTRO =====
        [ObservableProperty] private string? _tipoOutro;
        [ObservableProperty] private string? _descricaoOutro;
        [ObservableProperty] private decimal _valorOutro;

        partial void OnValorOutroChanged(decimal value) => RecalcularSubtotal();

        private void RecalcularSubtotal()
        {
            Subtotal = CategoriaSelecionada switch
            {
                CategoriaPagamento.Propina => MesesDisponiveis.Count(m => m.Selecionado) * ValorMensalidade,
                CategoriaPagamento.Cartao => MotivoCartao switch
                {
                    "1ª Via" => 2000m,
                    "2ª Via (Perda)" => 2500m,
                    "2ª Via (Dano)" => 2500m,
                    _ => 0m
                },
                CategoriaPagamento.Prova => TipoProva switch
                {
                    "Exame Normal" => 1500m,
                    "Exame de Recuperação" => 3000m,
                    "Exame Especial" => 4000m,
                    _ => 0m
                },
                CategoriaPagamento.Uniforme => TipoUniforme switch
                {
                    "Educação Física" => 8000m,
                    "Uniforme Escolar Completo" => 12000m,
                    _ => 0m
                },
                CategoriaPagamento.Outro => ValorOutro,
                _ => 0m
            };
        }

        public AlunoPagamentosViewModel(
            IFinanceiroService? financeiroService,
            int alunoId,
            IReciboPagamentoService? reciboService = null)
        {
            _financeiroService = financeiroService;
            _alunoId = alunoId;
            _reciboService = reciboService;
        }

        public AlunoPagamentosViewModel() { }

        // ===== Navegação do fluxo =====

        /// <summary>Chamado pela view "pai" antes de abrir o modal, para identificar o aluno (dados só de leitura).</summary>
        public void SetAluno(string nomeCompleto, string codigoMatricula, string anoLectivo, string classe)
        {
            NomeEstudante = nomeCompleto;
            ErroConfirmacao = null;
            CodigoMatricula = codigoMatricula;
            AnoLectivoPropina = anoLectivo;
            ClassePropina = classe;
        }

        /// <summary>
        /// Carrega as opções dos comboboxes "Ano Lectivo" e "Classe" a partir do Core.
        /// Chamado pelo "pai" (DetalhesAlunoViewModel.InitializeAsync) - esta ViewModel não
        /// guarda referência ao IEscolaService, só usa a instância recebida aqui.
        /// </summary>
        private async Task AtualizarMesesPropinaAsync()
        {
            if (_financeiroService is null || _alunoId <= 0)
                return;

            try
            {
                var pagamentos = await _financeiroService.ObterHistoricoPagamentosAsync(_alunoId);
                _mesesPropinaPagos.Clear();
                foreach (var p in pagamentos.Where(p => p.Tipo == TipoCobranca.Propina && p.Estado == EstadoPagamento.Pago && !p.Anulado))
                    _mesesPropinaPagos.Add((p.MesReferencia.Year, p.MesReferencia.Month));

                // Ano lectivo corrente: Setembro -> Junho.
                var inicioAno = DateTime.Now.Month >= 9 ? DateTime.Now.Year : DateTime.Now.Year - 1;
                AtualizarDisponibilidadeMeses(inicioAno);

                OnPropertyChanged(nameof(MesesSelecionadosLabel));
            }
            catch
            {
                // A validação definitiva continua a ser feita no fluxo de confirmação.
            }
        }

        private void AtualizarDisponibilidadeMeses(int? anoLectivo = null)
        {
            var inicioAno = anoLectivo ?? (DateTime.Now.Month >= 9 ? DateTime.Now.Year : DateTime.Now.Year - 1);
            var podeEscolher = true;

            foreach (var mes in MesesDisponiveis)
            {
                var ano = mes.NumeroMes >= 9 ? inicioAno : inicioAno + 1;
                var jaPago = _mesesPropinaPagos.Contains((ano, mes.NumeroMes));

                mes.Disponivel = jaPago || podeEscolher;
                if (!jaPago && !podeEscolher)
                    mes.Selecionado = false;

                if (!jaPago && !mes.Selecionado)
                    podeEscolher = false;
            }
        }

        public async Task CarregarOpcoesAsync(IEscolaService escolaService)
        {
            try
            {
                var anos = await escolaService.ObterAnosLectivosAsync();
                AnosLectivosDisponiveis.Clear();
                foreach (var ano in anos.OrderByDescending(a => a.DataInicio))
                    AnosLectivosDisponiveis.Add(ano.Nome);

                var classes = await escolaService.ObterClassesAsync();
                ClassesDisponiveis.Clear();
                foreach (var classe in classes.OrderBy(c => c.Numero))
                    ClassesDisponiveis.Add($"{classe.Numero}ª Classe");
            }
            catch
            {
                // Mantém as listas como estavam (comboboxes ficam vazios/preservam seleção actual).
            }
        }

        [RelayCommand]
        private void Abrir()
        {
            CategoriaSelecionada = null;
            IsAberto = true;
            _ = AtualizarMesesPropinaAsync();
        }

        [RelayCommand]
        private void SelecionarCategoria(CategoriaPagamento categoria)
        {
            LimparCampos();
            CategoriaSelecionada = categoria;
        }

        [RelayCommand]
        private void VoltarParaLegenda() => CategoriaSelecionada = null;

        [RelayCommand]
        private void Fechar()
        {
            IsAberto = false;
            ErroConfirmacao = null;
            CategoriaSelecionada = null;
            LimparCampos();
        }

        private void LimparCampos()
        {
            FormaPagamento = null;
            NumeroTalao = null;
            DescricaoAdicional = null;
            MultasJuros = 0;
            Desconto = 0;

            foreach (var mes in MesesDisponiveis) mes.Selecionado = false;
            OnPropertyChanged(nameof(MesesSelecionadosLabel));
            // NOTA: AnoLectivoPropina/ClassePropina NÃO são limpos aqui de propósito -
            // são dados do aluno (SetAluno), não do formulário; devem manter-se entre
            // categorias e após Fechar/Cancelar.

            TipoCartao = null;
            MotivoCartao = null;

            TipoProva = null;
            DisciplinaProva = null;

            TipoUniforme = null;
            TamanhoUniforme = null;

            TipoOutro = null;
            DescricaoOutro = null;
            ValorOutro = 0;

            Subtotal = 0;
        }

        private bool PodeConfirmar()
        {
            if (string.IsNullOrWhiteSpace(FormaPagamento) || CategoriaSelecionada is null || Total <= 0)
                return false;

            return CategoriaSelecionada switch
            {
                CategoriaPagamento.Propina => MesesDisponiveis.Any(m => m.Selecionado),
                CategoriaPagamento.Cartao => !string.IsNullOrWhiteSpace(MotivoCartao),
                CategoriaPagamento.Prova => !string.IsNullOrWhiteSpace(TipoProva),
                CategoriaPagamento.Uniforme => !string.IsNullOrWhiteSpace(TipoUniforme),
                CategoriaPagamento.Outro => !string.IsNullOrWhiteSpace(DescricaoOutro) && ValorOutro > 0,
                _ => false
            };
        }

        /// <summary>
        /// Disparado quando o pagamento é confirmado. DetalhesAlunoViewModel deve subscrever
        /// este evento (tal como já faz com ExclusaoConfirmada) para atualizar o histórico e o saldo.
        /// </summary>
        public event EventHandler<PagamentoRealizadoEventArgs>? PagamentoConfirmado;

        private async Task MostrarSucessoAsync(string mensagem, string detalhe)
        {
            SucessoMensagem = mensagem;
            SucessoDetalhe = detalhe;
            IsSucessoAberto = true;
            await Task.Delay(1800);
            IsSucessoAberto = false;
        }

        [RelayCommand(CanExecute = nameof(PodeConfirmar))]
        private async Task ConfirmarPagamento()
        {
            if (CategoriaSelecionada is null || _financeiroService is null || _alunoId <= 0)
                return;

            ErroConfirmacao = null;

            if (CategoriaSelecionada == CategoriaPagamento.Propina)
            {
                await AtualizarMesesPropinaAsync();
                if (MesesDisponiveis.Any(m => m.Selecionado && !m.Disponivel))
                {
                    ErroConfirmacao = "Não é possível pagar uma propina enquanto existir um mês anterior por regularizar.";
                    return;
                }
            }

            var descricao = CategoriaSelecionada switch
            {
                CategoriaPagamento.Propina => MesesSelecionadosLabel,
                CategoriaPagamento.Cartao => $"Cartão · {MotivoCartao}",
                CategoriaPagamento.Prova => $"Prova · {TipoProva} ({DisciplinaProva})",
                CategoriaPagamento.Uniforme => $"Uniforme · {TipoUniforme} ({TamanhoUniforme})",
                CategoriaPagamento.Outro => DescricaoOutro ?? "Outro",
                _ => CategoriaSelecionada.ToString() ?? string.Empty
            };

            var quantidadeReferencias = CategoriaSelecionada == CategoriaPagamento.Propina
                ? MesesDisponiveis.Count(m => m.Selecionado)
                : 1;

            var tipo = CategoriaSelecionada switch
            {
                CategoriaPagamento.Propina => TipoCobranca.Propina,
                CategoriaPagamento.Cartao => TipoCobranca.CartaoEscolar,
                CategoriaPagamento.Prova => TipoCobranca.Outros,
                CategoriaPagamento.Uniforme => TipoCobranca.Uniforme,
                CategoriaPagamento.Outro => TipoCobranca.Outros,
                _ => TipoCobranca.Outros
            };

            try
            {
                var pagamento = await _financeiroService.RegistarPagamentoAsync(
                    _alunoId, tipo, Total, FormaPagamento);

                PagamentoConfirmado?.Invoke(this, new PagamentoRealizadoEventArgs(
                    CategoriaSelecionada.Value,
                    descricao,
                    pagamento.NumeroRecibo,
                    pagamento.Valor,
                    pagamento.DataPagamento ?? DateTime.Now,
                    quantidadeReferencias));

                string detalheRecibo;
                try
                {
                    if (_reciboService is not null)
                    {
                        var recibo = await _reciboService.GuardarEImprimirAsync(
                            pagamento,
                            NomeEstudante,
                            descricao);

                        detalheRecibo = recibo.ImpressaoEnviada
                            ? $"Recibo {pagamento.NumeroRecibo} salvo e enviado para impressão · {FormatKz(pagamento.Valor)}"
                            : $"Recibo {pagamento.NumeroRecibo} salvo · impressora térmica não disponível · {FormatKz(pagamento.Valor)}";
                    }
                    else
                    {
                        detalheRecibo = $"{pagamento.NumeroRecibo} · {FormatKz(pagamento.Valor)}";
                    }
                }
                catch (Exception ex)
                {
                    detalheRecibo = $"Recibo {pagamento.NumeroRecibo} registado, mas não foi possível imprimir: {ex.Message}";
                }

                await MostrarSucessoAsync(
                    "Pagamento realizado com sucesso!",
                    detalheRecibo);

                Fechar();
            }
            catch (Exception ex)
            {
                ErroConfirmacao = ex.Message;
            }
        }
    }
}