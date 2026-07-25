using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ScoolManager.Desktop.ViewModels.Pages
{

/// <summary>
/// ViewModel da view "Financeiro" (View 4 da Secretaria Escolar).
///
/// 4 abas internas: Recebimentos, Entradas, Saídas, Caixa. Segue o mesmo
/// padrão de AlunosViewModel/DetalhesAlunoViewModel: dados locais/mock,
/// modais em overlay, sem dependência do ScoolManager.Core.
///
/// IMPORTANTE (spec, ver Relatório de Organização do Módulo Financeiro):
/// a aba "Recebimentos" é APENAS CONSULTA. Este módulo não efetua cobranças
/// nem cria pagamentos — todos os registos chegam exclusivamente do módulo
/// Alunos (ficha individual do aluno), que atualiza este ecrã automaticamente.
/// Por isso não existe aqui nenhum comando "Novo Pagamento"; apenas
/// pesquisa/filtros, consulta de detalhes, reimpressão de recibo, anulação
/// (mediante autorização) e exportação de listagens.
///
/// IMPORTANTE (spec): "Recibo NÃO é View". O fluxo Recebimento -> Detalhes
/// do Recebimento (Modal) -> Ver Recibo (Modal) é implementado como uma
/// troca de CONTEÚDO dentro do mesmo modal "Detalhes do Recebimento" (ver
/// <see cref="MostrandoRecibo"/>), nunca como um segundo modal empilhado.
/// </summary>
public partial class FinanceiroViewModel : ViewModelBase
{
    public enum Aba
    {
        Recebimentos,
        Entradas,
        Saidas,
        Caixa
    }

    // ================================================================
    // Abas
    // ================================================================
    [ObservableProperty] private Aba _abaSelecionada = Aba.Recebimentos;

    public bool AbaRecebimentosAtiva => AbaSelecionada == Aba.Recebimentos;
    public bool AbaEntradasAtiva => AbaSelecionada == Aba.Entradas;
    public bool AbaSaidasAtiva => AbaSelecionada == Aba.Saidas;
    public bool AbaCaixaAtiva => AbaSelecionada == Aba.Caixa;

    partial void OnAbaSelecionadaChanged(Aba value)
    {
        OnPropertyChanged(nameof(AbaRecebimentosAtiva));
        OnPropertyChanged(nameof(AbaEntradasAtiva));
        OnPropertyChanged(nameof(AbaSaidasAtiva));
        OnPropertyChanged(nameof(AbaCaixaAtiva));
    }

    [RelayCommand] private void AbrirAbaRecebimentos() => AbaSelecionada = Aba.Recebimentos;
    [RelayCommand] private void AbrirAbaEntradas() => AbaSelecionada = Aba.Entradas;
    [RelayCommand] private void AbrirAbaSaidas() => AbaSelecionada = Aba.Saidas;
    [RelayCommand] private void AbrirAbaCaixa() => AbaSelecionada = Aba.Caixa;

    // ================================================================
    // Aba "Recebimentos" (SÓ CONSULTA — ver nota no cabeçalho da classe)
    // ================================================================
    private readonly List<PagamentoItem> _todosPagamentos;
    public ObservableCollection<PagamentoItem> Pagamentos { get; } = new();

    [ObservableProperty] private string _pesquisaPagamento = string.Empty;

    // ---- Filtros rápidos (Período / Método de Pagamento / Tipo de Cobrança) ----
    public IReadOnlyList<string> OpcoesPeriodo { get; } =
        new[] { "Todos", "Hoje", "Esta semana", "Este mês", "Ano letivo", "Intervalo personalizado" };

    public IReadOnlyList<string> OpcoesMetodoPagamento { get; } =
        new[] { "Todos", "Dinheiro", "Transferência Bancária", "TPA / Multicaixa" };

    public IReadOnlyList<string> OpcoesTipoCobranca { get; } =
        new[] { "Todos", "Matrícula", "Propina", "Confirmação", "Uniforme", "Cartão Escolar", "Declaração", "Certificado", "Outros" };

    [ObservableProperty] private string _filtroPeriodo = "Todos";
    [ObservableProperty] private string _filtroMetodoPagamento = "Todos";
    [ObservableProperty] private string _filtroTipoCobranca = "Todos";

    public bool MostrarIntervaloPersonalizado => FiltroPeriodo == "Intervalo personalizado";

    // Intervalo personalizado (só relevante quando FiltroPeriodo == "Intervalo personalizado")
    [ObservableProperty] private DateTimeOffset? _filtroDataInicio;
    [ObservableProperty] private DateTimeOffset? _filtroDataFim;

    partial void OnPesquisaPagamentoChanged(string value) => AplicarFiltroPagamentos();

    partial void OnFiltroPeriodoChanged(string value)
    {
        OnPropertyChanged(nameof(MostrarIntervaloPersonalizado));
        AplicarFiltroPagamentos();
    }
    partial void OnFiltroMetodoPagamentoChanged(string value) => AplicarFiltroPagamentos();
    partial void OnFiltroTipoCobrancaChanged(string value) => AplicarFiltroPagamentos();
    partial void OnFiltroDataInicioChanged(DateTimeOffset? value) => AplicarFiltroPagamentos();
    partial void OnFiltroDataFimChanged(DateTimeOffset? value) => AplicarFiltroPagamentos();

    private void AplicarFiltroPagamentos()
    {
        IEnumerable<PagamentoItem> query = _todosPagamentos;

        if (!string.IsNullOrWhiteSpace(PesquisaPagamento))
        {
            var termo = PesquisaPagamento.Trim();
            query = query.Where(p =>
                p.Aluno.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                p.Referencia.Contains(termo, StringComparison.OrdinalIgnoreCase));
        }

        if (FiltroMetodoPagamento != "Todos")
            query = query.Where(p => p.Metodo == FiltroMetodoPagamento);

        if (FiltroTipoCobranca != "Todos")
            query = query.Where(p => p.TipoCobranca == FiltroTipoCobranca);

        if (FiltroPeriodo != "Todos")
            query = query.Where(p => DentroDoPeriodo(p.Data, FiltroPeriodo, FiltroDataInicio, FiltroDataFim));

        Pagamentos.Clear();
        foreach (var p in query) Pagamentos.Add(p);
    }

    private static bool DentroDoPeriodo(string dataTexto, string periodo, DateTimeOffset? inicio, DateTimeOffset? fim)
    {
        if (!DateTime.TryParseExact(dataTexto, "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out var data))
            return true;

        var hoje = DateTime.Now.Date;
        return periodo switch
        {
            "Hoje" => data.Date == hoje,
            "Esta semana" => data.Date >= hoje.AddDays(-(int)hoje.DayOfWeek) && data.Date <= hoje,
            "Este mês" => data.Year == hoje.Year && data.Month == hoje.Month,
            "Ano letivo" => data >= AnoLetivoInicio(hoje),
            "Intervalo personalizado" =>
                (!inicio.HasValue || data.Date >= inicio.Value.Date) &&
                (!fim.HasValue || data.Date <= fim.Value.Date),
            _ => true
        };
    }

    /// <summary>Início do ano letivo: 1 de setembro do ano corrente (ou anterior, se ainda não chegou setembro).</summary>
    private static DateTime AnoLetivoInicio(DateTime hoje)
    {
        var inicioEsteAno = new DateTime(hoje.Year, 9, 1);
        return hoje >= inicioEsteAno ? inicioEsteAno : new DateTime(hoje.Year - 1, 9, 1);
    }

    [ObservableProperty] private PagamentoItem? _pagamentoSelecionado;

    // Detalhes do Recebimento / Ver Recibo (mesmo modal, troca de conteúdo)
    [ObservableProperty] private bool _isDetalhesPagamentoAberto;
    [ObservableProperty] private bool _mostrandoRecibo;

    [RelayCommand]
    private void AbrirDetalhesPagamento(PagamentoItem? pagamento)
    {
        if (pagamento is null) return;
        PagamentoSelecionado = pagamento;
        MostrandoRecibo = false;
        IsDetalhesPagamentoAberto = true;
    }

    [RelayCommand] private void AbrirVerRecibo() => MostrandoRecibo = true;
    [RelayCommand] private void VoltarDetalhesPagamento() => MostrandoRecibo = false;

    // TODO: gerar PDF/impressão real do recibo quando existir o serviço.
    [RelayCommand] private void ImprimirRecibo() { }

    // ---- Anular pagamento (mediante autorização) ----
    [ObservableProperty] private bool _isAnularPagamentoAberto;
    [ObservableProperty] private string _motivoAnulacao = string.Empty;

    [RelayCommand]
    private void AbrirAnularPagamento()
    {
        if (PagamentoSelecionado is null) return;
        MotivoAnulacao = string.Empty;
        IsDetalhesPagamentoAberto = false;
        IsAnularPagamentoAberto = true;
    }

    // TODO: exigir autorização (perfil/permissão) real antes de confirmar,
    // e propagar a anulação para o módulo Alunos quando existir o serviço.
    [RelayCommand]
    private void ConfirmarAnularPagamento()
    {
        if (PagamentoSelecionado is not null)
        {
            var indice = _todosPagamentos.IndexOf(PagamentoSelecionado);
            if (indice >= 0)
                _todosPagamentos[indice] = PagamentoSelecionado with { Estado = "Anulado" };
            AplicarFiltroPagamentos();
            AtualizarIndicadoresDashboard();
        }
        FecharModal();
    }

    // ---- Exportação de listagens ----
    // TODO: gerar ficheiro real (PDF/Excel) quando existir o serviço de exportação.
    [RelayCommand] private void ExportarPdf() { }
    [RelayCommand] private void ExportarExcel() { }

    // ---- Numeração sequencial de recibos (REC-AAAA-NNNNNN) ----
    private int _proximoNumeroRecibo = 1;

    private string GerarNumeroRecibo() => $"REC-{DateTime.Now.Year}-{_proximoNumeroRecibo++:D6}";

    // ================================================================
    // Abas "Entradas" e "Saídas" (mesma forma, coleções separadas)
    // ================================================================
    public ObservableCollection<MovimentoItem> Entradas { get; } = new();
    public ObservableCollection<MovimentoItem> Saidas { get; } = new();

    /// <summary>"Entrada" ou "Saída" - identifica o contexto do modal partilhado.</summary>
    [ObservableProperty] private string _movimentoTipoModal = "Entrada";

    [ObservableProperty] private MovimentoItem? _movimentoSelecionado;

    [ObservableProperty] private bool _isNovoMovimentoAberto;
    [ObservableProperty] private bool _isEditarMovimentoAberto;
    [ObservableProperty] private bool _isDetalhesMovimentoAberto;

    // Campos do formulário partilhado "Nova Entrada" / "Nova Saída"
    [ObservableProperty] private string _novoMovimentoDescricao = string.Empty;
    [ObservableProperty] private string _novoMovimentoCategoria = string.Empty;
    [ObservableProperty] private string _novoMovimentoValor = string.Empty;

    [RelayCommand]
    private void AbrirNovaEntrada()
    {
        MovimentoTipoModal = "Entrada";
        LimparFormularioMovimento();
        IsNovoMovimentoAberto = true;
    }

    [RelayCommand]
    private void AbrirNovaSaida()
    {
        MovimentoTipoModal = "Saída";
        LimparFormularioMovimento();
        IsNovoMovimentoAberto = true;
    }

    private void LimparFormularioMovimento()
    {
        NovoMovimentoDescricao = string.Empty;
        NovoMovimentoCategoria = string.Empty;
        NovoMovimentoValor = string.Empty;
    }

    [RelayCommand]
    private void AbrirDetalhesMovimento(MovimentoItem? movimento)
    {
        if (movimento is null) return;
        MovimentoSelecionado = movimento;
        MovimentoTipoModal = Entradas.Contains(movimento) ? "Entrada" : "Saída";
        IsDetalhesMovimentoAberto = true;
    }

    [RelayCommand]
    private void AbrirEditarMovimento()
    {
        if (MovimentoSelecionado is null) return;

        // Reaproveita os mesmos campos do formulário "Novo Movimento", já
        // pré-preenchidos com os dados atuais do item selecionado.
        NovoMovimentoDescricao = MovimentoSelecionado.Descricao;
        NovoMovimentoCategoria = MovimentoSelecionado.Categoria;
        NovoMovimentoValor = MovimentoSelecionado.Valor;

        IsDetalhesMovimentoAberto = false;
        IsEditarMovimentoAberto = true;
    }

    [RelayCommand]
    private void ConfirmarNovoMovimento()
    {
        if (!string.IsNullOrWhiteSpace(NovoMovimentoDescricao))
        {
            var novo = new MovimentoItem(
                NovoMovimentoDescricao,
                string.IsNullOrWhiteSpace(NovoMovimentoCategoria) ? "Outro" : NovoMovimentoCategoria,
                string.IsNullOrWhiteSpace(NovoMovimentoValor) ? "0 Kz" : NovoMovimentoValor,
                DateTime.Now.ToString("dd/MM/yyyy"));

            var destino = MovimentoTipoModal == "Entrada" ? Entradas : Saidas;
            destino.Insert(0, novo);
            AtualizarIndicadoresDashboard();
        }

        FecharModal();
    }

    [RelayCommand]
    private void ConfirmarEditarMovimento()
    {
        if (MovimentoSelecionado is not null && !string.IsNullOrWhiteSpace(NovoMovimentoDescricao))
        {
            var colecao = MovimentoTipoModal == "Entrada" ? Entradas : Saidas;
            var indice = colecao.IndexOf(MovimentoSelecionado);
            if (indice >= 0)
            {
                colecao[indice] = new MovimentoItem(
                    NovoMovimentoDescricao,
                    string.IsNullOrWhiteSpace(NovoMovimentoCategoria) ? "Outro" : NovoMovimentoCategoria,
                    string.IsNullOrWhiteSpace(NovoMovimentoValor) ? "0 Kz" : NovoMovimentoValor,
                    MovimentoSelecionado.Data);
            }
            AtualizarIndicadoresDashboard();
        }

        FecharModal();
    }

    // ================================================================
    // Aba "Caixa"
    // ================================================================
    [ObservableProperty] private bool _caixaAberto;
    [ObservableProperty] private string _saldoInicialLabel = "0,00 Kz";
    [ObservableProperty] private string _saldoAtualLabel = "0,00 Kz";

    public ObservableCollection<SessaoCaixaItem> HistoricoCaixa { get; } = new();

    [ObservableProperty] private bool _isAbrirCaixaAberto;
    [ObservableProperty] private bool _isFecharCaixaAberto;
    [ObservableProperty] private bool _isReabrirCaixaAberto;

    // Campo do formulário "Abrir Caixa"
    [ObservableProperty] private string _novoSaldoInicialCaixa = string.Empty;

    [RelayCommand]
    private void AbrirAbrirCaixaModal()
    {
        NovoSaldoInicialCaixa = string.Empty;
        IsAbrirCaixaAberto = true;
    }

    [RelayCommand] private void AbrirFecharCaixaModal() => IsFecharCaixaAberto = true;
    [RelayCommand] private void AbrirReabrirCaixaModal() => IsReabrirCaixaAberto = true;

    [RelayCommand]
    private void ConfirmarAbrirCaixa()
    {
        CaixaAberto = true;
        SaldoInicialLabel = string.IsNullOrWhiteSpace(NovoSaldoInicialCaixa) ? "0,00 Kz" : $"{NovoSaldoInicialCaixa} Kz";
        SaldoAtualLabel = SaldoInicialLabel;
        HistoricoCaixa.Insert(0, new SessaoCaixaItem(
            DateTime.Now.ToString("dd/MM/yyyy HH:mm"), null, SaldoInicialLabel, null, "Aberto"));
        FecharModal();
    }

    [RelayCommand]
    private void ConfirmarFecharCaixa()
    {
        CaixaAberto = false;
        if (HistoricoCaixa.Count > 0)
        {
            var atual = HistoricoCaixa[0];
            HistoricoCaixa[0] = atual with { DataFecho = DateTime.Now.ToString("dd/MM/yyyy HH:mm"), SaldoFinal = SaldoAtualLabel, Estado = "Fechado" };
        }
        FecharModal();
    }

    [RelayCommand]
    private void ConfirmarReabrirCaixa()
    {
        CaixaAberto = true;
        if (HistoricoCaixa.Count > 0)
        {
            var atual = HistoricoCaixa[0];
            HistoricoCaixa[0] = atual with { Estado = "Reaberto" };
        }
        FecharModal();
    }

    // ================================================================
    // Fecho unificado de todos os modais desta view
    // ================================================================
    public bool AlgumModalAberto =>
        IsDetalhesPagamentoAberto || IsAnularPagamentoAberto ||
        IsNovoMovimentoAberto || IsEditarMovimentoAberto || IsDetalhesMovimentoAberto ||
        IsAbrirCaixaAberto || IsFecharCaixaAberto || IsReabrirCaixaAberto;

    partial void OnIsDetalhesPagamentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsAnularPagamentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsNovoMovimentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsEditarMovimentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsDetalhesMovimentoAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsAbrirCaixaAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsFecharCaixaAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsReabrirCaixaAbertoChanged(bool v) => OnPropertyChanged(nameof(AlgumModalAberto));

    [RelayCommand]
    private void FecharModal()
    {
        IsDetalhesPagamentoAberto = false;
        MostrandoRecibo = false;
        IsAnularPagamentoAberto = false;
        MotivoAnulacao = string.Empty;
        IsNovoMovimentoAberto = false;
        IsEditarMovimentoAberto = false;
        IsDetalhesMovimentoAberto = false;
        IsAbrirCaixaAberto = false;
        IsFecharCaixaAberto = false;
        IsReabrirCaixaAberto = false;
    }

    // ================================================================
    // Dashboard Financeiro (indicadores resumidos — secção 5 do relatório)
    // ================================================================
    [ObservableProperty] private string _recebimentosMesAtualLabel = "0 Kz";
    [ObservableProperty] private string _recebimentosMesAnteriorLabel = "0 Kz";
    [ObservableProperty] private string _totalEntradasMesLabel = "0 Kz";
    [ObservableProperty] private string _totalSaidasMesLabel = "0 Kz";
    // Saldo atual do caixa reutiliza SaldoAtualLabel (aba "Caixa").

    private void AtualizarIndicadoresDashboard()
    {
        var hoje = DateTime.Now;
        var mesAnterior = hoje.AddMonths(-1);

        var recebimentosValidos = _todosPagamentos.Where(p => !p.Anulado).Select(p => (p.Data, p.Valor));

        RecebimentosMesAtualLabel = FormatarKz(SomaValoresDoMes(recebimentosValidos, hoje.Year, hoje.Month));
        RecebimentosMesAnteriorLabel = FormatarKz(SomaValoresDoMes(recebimentosValidos, mesAnterior.Year, mesAnterior.Month));
        TotalEntradasMesLabel = FormatarKz(SomaValoresDoMes(Entradas.Select(e => (e.Data, e.Valor)), hoje.Year, hoje.Month));
        TotalSaidasMesLabel = FormatarKz(SomaValoresDoMes(Saidas.Select(s => (s.Data, s.Valor)), hoje.Year, hoje.Month));
    }

    private static decimal SomaValoresDoMes(IEnumerable<(string Data, string Valor)> itens, int ano, int mes)
    {
        decimal total = 0m;
        foreach (var (dataTexto, valorTexto) in itens)
        {
            if (DateTime.TryParseExact(dataTexto, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out var data) &&
                data.Year == ano && data.Month == mes)
            {
                total += ParseValorKz(valorTexto);
            }
        }
        return total;
    }

    private static decimal ParseValorKz(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor)) return 0m;
        var limpo = valor.Replace("Kz", string.Empty).Trim()
            .Replace(".", string.Empty)   // remove separador de milhar
            .Replace(",", ".");           // vírgula decimal -> ponto
        return decimal.TryParse(limpo, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : 0m;
    }

    private static string FormatarKz(decimal valor) =>
        $"{valor.ToString("N0", System.Globalization.CultureInfo.InvariantCulture).Replace(",", ".")} Kz";

    // ================================================================
    // Dados mock
    // ================================================================
    public FinanceiroViewModel()
    {
        _todosPagamentos = new List<PagamentoItem>
        {
            new("João Pedro da Silva",  "PAG-2026-0451", "25.000 Kz", "02/04/2026", "Dinheiro",              GerarNumeroRecibo(), "Propina"),
            new("Maria Luísa Alberto",  "PAG-2026-0452", "25.000 Kz", "05/03/2026", "Transferência Bancária", GerarNumeroRecibo(), "Propina"),
            new("Ana Paula Domingos",   "PAG-2026-0453", "25.000 Kz", "01/02/2026", "TPA / Multicaixa",       GerarNumeroRecibo(), "Matrícula"),
            new("Carlos Manuel",        "PAG-2026-0454", "25.000 Kz", "28/01/2026", "Dinheiro",               GerarNumeroRecibo(), "Uniforme"),
        };
        AplicarFiltroPagamentos();

        Entradas.Add(new MovimentoItem("Subsídio do Ministério da Educação", "Subsídio", "500.000 Kz", "05/07/2026"));
        Entradas.Add(new MovimentoItem("Doação - Associação de Pais", "Doação", "80.000 Kz", "18/06/2026"));

        Saidas.Add(new MovimentoItem("Salários - Corpo Docente", "Salários", "800.000 Kz", "30/06/2026"));
        Saidas.Add(new MovimentoItem("Manutenção do gerador", "Manutenção", "45.000 Kz", "22/06/2026"));
        Saidas.Add(new MovimentoItem("Material de limpeza", "Consumíveis", "12.500 Kz", "15/06/2026"));

        CaixaAberto = true;
        SaldoInicialLabel = "50.000 Kz";
        SaldoAtualLabel = "312.500 Kz";
        HistoricoCaixa.Add(new SessaoCaixaItem("20/07/2026 07:32", null, "50.000 Kz", null, "Aberto"));
        HistoricoCaixa.Add(new SessaoCaixaItem("17/07/2026 07:28", "17/07/2026 17:05", "40.000 Kz", "298.400 Kz", "Fechado"));
        HistoricoCaixa.Add(new SessaoCaixaItem("16/07/2026 07:30", "16/07/2026 17:10", "35.000 Kz", "271.200 Kz", "Fechado"));

        AtualizarIndicadoresDashboard();
    }
}

/// <summary>Linha da aba "Recebimentos" (consulta de pagamentos vindos do módulo Alunos).</summary>
public sealed record PagamentoItem(
    string Aluno,
    string Referencia,
    string Valor,
    string Data,
    string Metodo,
    string NumeroRecibo,
    string TipoCobranca,
    string Estado = "Confirmado")
{
    public bool Anulado => Estado == "Anulado";
}

/// <summary>Linha partilhada pelas abas "Entradas" e "Saídas".</summary>
public sealed class MovimentoItem
{
    public string Descricao { get; }
    public string Categoria { get; }
    public string Valor { get; }
    public string Data { get; }

    public MovimentoItem(string descricao, string categoria, string valor, string data)
    {
        Descricao = descricao;
        Categoria = categoria;
        Valor = valor;
        Data = data;
    }
}

/// <summary>Sessão de caixa exibida no histórico da aba "Caixa".</summary>
public sealed record SessaoCaixaItem(string DataAbertura, string? DataFecho, string SaldoInicial, string? SaldoFinal, string Estado);

} // fim namespace ScoolManager.Desktop.ViewModels.Pages
