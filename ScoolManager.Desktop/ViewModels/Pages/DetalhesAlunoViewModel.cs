using System;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.ViewModels.Pages.Pagamentos;

namespace ScoolManager.Desktop.ViewModels.Pages
{

/// <summary>
/// ViewModel da view "Detalhes do Aluno" (View 3 da Secretaria Escolar).
///
/// Centraliza as informações do aluno em 3 abas (Dados Pessoais, Documentação,
/// Histórico Financeiro) e expõe as 3 ações do cabeçalho (Editar Perfil,
/// Efetuar Pagamento, Renovar Matrícula) + Excluir Aluno via menu "⋮".
///
/// O fluxo de "Efetuar Pagamento" (legenda de categorias + 5 formulários) vive
/// inteiramente em <see cref="AlunoPagamentosViewModel"/> / AlunoPagamentosView,
/// para não poluir esta classe. Esta ViewModel apenas abre esse fluxo e escuta
/// o evento PagamentoConfirmado para atualizar o histórico/saldo.
///
/// IMPORTANTE: esta view não depende do ScoolManager.Core (ainda vazio).
/// Os dados são locais/mock, assim como em AlunosViewModel. Quando o Core
/// tiver as entidades reais, trocar estes campos por bindings ao serviço.
/// </summary>
public partial class DetalhesAlunoViewModel : ViewModelBase, IAsyncInitializable
{
    private readonly IAlunoService _alunoService;
    private readonly int? _alunoId;
    public AlunoPagamentosViewModel Pagamentos { get; }
    public enum Aba
    {
        DadosPessoais,
        Documentacao,
        HistoricoFinanceiro
    }

    // ===== Fluxo "Efetuar Pagamento" (separado, ver AlunoPagamentosViewModel) =====
    public AlunoPagamentosViewModel PagamentosViewModel { get; } = new();

    // ===== Cabeçalho =====
    [ObservableProperty] private string _nomeCompleto = string.Empty;
    [ObservableProperty] private string _codigoMatricula = string.Empty;
    [ObservableProperty] private string _classe = string.Empty;
    [ObservableProperty] private string _turma = string.Empty;
    [ObservableProperty] private bool _ativo = true;
    [ObservableProperty] private string? _fotografiaCaminho;

    public bool TemFotografia => !string.IsNullOrEmpty(FotografiaCaminho);

    partial void OnFotografiaCaminhoChanged(string? value) => OnPropertyChanged(nameof(TemFotografia));

    public string SituacaoTexto => Ativo ? "Ativo" : "Inativo";
    public IBrush SituacaoTextoBrush => new SolidColorBrush(Color.Parse(Ativo ? "#34D399" : "#FFB4AB"));
    public IBrush SituacaoFundoBrush => new SolidColorBrush(Color.Parse(Ativo ? "#1A34D399" : "#1AFFB4AB"));

    public string Iniciais
    {
        get
        {
            if (string.IsNullOrWhiteSpace(NomeCompleto)) return string.Empty;
            var partes = NomeCompleto.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var iniciais = string.Empty;
            if (partes.Length > 0) iniciais += char.ToUpperInvariant(partes[0][0]);
            if (partes.Length > 1) iniciais += char.ToUpperInvariant(partes[^1][0]);
            return iniciais;
        }
    }

    partial void OnAtivoChanged(bool value)
    {
        OnPropertyChanged(nameof(SituacaoTexto));
        OnPropertyChanged(nameof(SituacaoTextoBrush));
        OnPropertyChanged(nameof(SituacaoFundoBrush));
    }

    partial void OnNomeCompletoChanged(string value)
    {
        OnPropertyChanged(nameof(Iniciais));
        PagamentosViewModel.SetAluno(value, CodigoMatricula);
    }

    partial void OnCodigoMatriculaChanged(string value) => PagamentosViewModel.SetAluno(NomeCompleto, value);

    // ===== Abas =====
    [ObservableProperty] private Aba _abaSelecionada = Aba.DadosPessoais;

    public bool AbaDadosPessoaisAtiva => AbaSelecionada == Aba.DadosPessoais;
    public bool AbaDocumentacaoAtiva => AbaSelecionada == Aba.Documentacao;
    public bool AbaHistoricoFinanceiroAtiva => AbaSelecionada == Aba.HistoricoFinanceiro;

    partial void OnAbaSelecionadaChanged(Aba value)
    {
        OnPropertyChanged(nameof(AbaDadosPessoaisAtiva));
        OnPropertyChanged(nameof(AbaDocumentacaoAtiva));
        OnPropertyChanged(nameof(AbaHistoricoFinanceiroAtiva));
    }

    [RelayCommand] private void AbrirAbaDadosPessoais() => AbaSelecionada = Aba.DadosPessoais;
    [RelayCommand] private void AbrirAbaDocumentacao() => AbaSelecionada = Aba.Documentacao;
    [RelayCommand] private void AbrirAbaHistoricoFinanceiro() => AbaSelecionada = Aba.HistoricoFinanceiro;

    // ===== Aba "Dados Pessoais" =====
    [ObservableProperty] private DateTimeOffset? _dataNascimento;
    [ObservableProperty] private string _genero = string.Empty;
    [ObservableProperty] private string _nacionalidade = string.Empty;
    [ObservableProperty] private string _numeroBiCedula = string.Empty;
    [ObservableProperty] private string _endereco = string.Empty;
    [ObservableProperty] private string _telefone = string.Empty;
    [ObservableProperty] private string? _email;

    // Encarregados de educação (a spec atual não tem aba própria para isto,
    // por isso ficam dentro de "Dados Pessoais" - mesma info do Passo 2
    // do wizard "Novo Aluno" em AlunosViewModel).
    [ObservableProperty] private string _nomePai = string.Empty;
    [ObservableProperty] private string _contactoPai = string.Empty;
    [ObservableProperty] private string _nomeMae = string.Empty;
    [ObservableProperty] private string _contactoMae = string.Empty;

    // ===== Matrícula / dados académicos (usados na aba e no widget lateral) =====
    [ObservableProperty] private string _curso = string.Empty;
    [ObservableProperty] private string _anoLectivo = string.Empty;
    [ObservableProperty] private DateTimeOffset? _dataMatricula;

    // ===== Aba "Documentação" =====
    public ObservableCollection<DocumentoAlunoItem> Documentos { get; } = new();

    // ===== Aba "Histórico Financeiro" + widget "Situação Financeira" =====
    public ObservableCollection<PagamentoHistoricoItem> HistoricoPagamentos { get; } = new();

    [ObservableProperty] private string _saldoDevedorLabel = "0,00 Kz";
    [ObservableProperty] private int _propinasPagas;
    [ObservableProperty] private int _propinasTotais = 10;

    public double ProgressoPropinas => PropinasTotais == 0 ? 0 : (double)PropinasPagas / PropinasTotais;
    public string ProgressoPropinasLabel => $"{PropinasPagas} / {PropinasTotais} Meses";

    partial void OnPropinasPagasChanged(int value)
    {
        OnPropertyChanged(nameof(ProgressoPropinas));
        OnPropertyChanged(nameof(ProgressoPropinasLabel));
    }

    // ===== Os 3 modais restantes (Editar Aluno, Renovar Matrícula, Confirmar Exclusão).
    //       "Efetuar Pagamento" passou a viver em PagamentosViewModel. =====
    [ObservableProperty] private bool _isEditarPerfilAberto;
    [ObservableProperty] private bool _isRenovarMatriculaAberto;
    [ObservableProperty] private bool _isConfirmarExclusaoAberto;

    public bool AlgumModalAberto =>
        IsEditarPerfilAberto || IsRenovarMatriculaAberto || IsConfirmarExclusaoAberto;

    partial void OnIsEditarPerfilAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsRenovarMatriculaAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsConfirmarExclusaoAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));

    [RelayCommand] private void AbrirEditarPerfil() => IsEditarPerfilAberto = true;
    [RelayCommand] private void AbrirRenovarMatricula() => IsRenovarMatriculaAberto = true;
    [RelayCommand] private void AbrirConfirmarExclusao() => IsConfirmarExclusaoAberto = true;

    /// <summary>Abre o fluxo "Efetuar Pagamento" (Legenda -> formulário), agora isolado em PagamentosViewModel.</summary>
    [RelayCommand] private void AbrirEfetuarPagamento() => PagamentosViewModel.AbrirCommand.Execute(null);

    [RelayCommand]
    private void FecharModal()
    {
        IsEditarPerfilAberto = false;
        IsRenovarMatriculaAberto = false;
        IsConfirmarExclusaoAberto = false;
    }

    // TODO: ligar aos serviços reais quando existirem (persistência, etc.)
    [RelayCommand] private void ConfirmarEditarPerfil() => FecharModal();
    [RelayCommand] private void ConfirmarRenovarMatricula() => FecharModal();

    /// <summary>Atualiza histórico/saldo quando PagamentosViewModel confirma um pagamento (qualquer categoria).</summary>
    private void OnPagamentoConfirmado(object? sender, PagamentoRealizadoEventArgs e)
    {
        // TODO: substituir por chamada real ao módulo Financeiro.
        HistoricoPagamentos.Insert(0, new PagamentoHistoricoItem(
            mesReferencia: e.Descricao,
            numeroRecibo: e.NumeroRecibo,
            valor: e.Valor.ToString("N2") + " Kz",
            data: e.Data.ToString("dd/MM/yyyy"),
            pago: true));

        if (e.Categoria == CategoriaPagamento.Propina)
        {
            PropinasPagas = Math.Min(PropinasTotais, PropinasPagas + e.QuantidadeReferencias);
        }
    }

    /// <summary>Disparado ao confirmar a exclusão — a View decide como voltar para Alunos.</summary>
    public event EventHandler? ExclusaoConfirmada;

    [RelayCommand]
    private async Task ConfirmarExclusao()
    {
        if (_alunoId is null or <= 0) return;

        try
        {
            await _alunoService.RemoverAsync(_alunoId.Value);
            FecharModal();
            ExclusaoConfirmada?.Invoke(this, EventArgs.Empty);
        }
        catch
        {
            FecharModal();
        }
    }

    /// <summary>Disparado pelo botão discreto "Voltar para Alunos" do cabeçalho.</summary>
    public event EventHandler? VoltarParaAlunosSolicitado;

    [RelayCommand]
    private void VoltarParaAlunos() => VoltarParaAlunosSolicitado?.Invoke(this, EventArgs.Empty);

    // ================================================================
    // Construtores
    // ================================================================
    public DetalhesAlunoViewModel(AlunoListItemModel aluno, IAlunoService alunoService)
    {
        _alunoService = alunoService;
        _alunoId = aluno.Id;

        PagamentosViewModel.PagamentoConfirmado += OnPagamentoConfirmado;

        // Pré-preenche o cabeçalho (evita ecrã vazio enquanto carrega)
        NomeCompleto = aluno.Nome;
        CodigoMatricula = aluno.Codigo;
        Classe = aluno.Classe;
        Turma = aluno.Sala;
        Curso = aluno.Curso;
        Ativo = aluno.Ativo;
        Telefone = aluno.Telefone;
        NomePai = aluno.Encarregado;
        ContactoPai = aluno.Telefone;
        // NÃO chamar PreencherDadosMock()
    }

    public DetalhesAlunoViewModel(int alunoId, IAlunoService alunoService, IEscolaService escolaService)
    {
        _alunoService = alunoService;
        _alunoId = alunoId;

        Pagamentos = new AlunoPagamentosViewModel(escolaService);
        Pagamentos.PagamentoConfirmado += OnPagamentoConfirmado;
    }

    public DetalhesAlunoViewModel() : this(
        new AlunoListItemModel(0, "2026/0000", "Aluno Exemplo", "", "", "", "", "", true),
        null!)
    {
    }

    public async Task InitializeAsync()
    {
        if (_alunoId is null or <= 0 || _alunoService is null)
            return;

        try
        {
            var aluno = await _alunoService.ObterDetalhesAsync(_alunoId.Value);

            NomeCompleto      = aluno.Nome;
            CodigoMatricula   = aluno.Codigo;
            Classe            = aluno.Turma?.Nome ?? string.Empty;
            Turma             = aluno.Turma?.Sala?.Nome ?? string.Empty;
            Curso             = aluno.Turma?.Curso?.Nome ?? string.Empty;
            Ativo             = aluno.Ativo;
            Telefone          = aluno.Telefone ?? string.Empty;
            DataNascimento    = aluno.DataNascimento;
            Genero            = aluno.Genero ?? string.Empty;
            Nacionalidade     = aluno.Nacionalidade ?? string.Empty;
            NumeroBiCedula    = aluno.NumeroBiCedula ?? string.Empty;
            Endereco          = aluno.Endereco ?? string.Empty;
            Email             = aluno.Email;
            FotografiaCaminho = aluno.FotografiaCaminho;
            AnoLectivo        = aluno.AnoLectivo?.Nome ?? string.Empty;
            DataMatricula     = aluno.DataMatricula;

            var pai = aluno.Encarregados.FirstOrDefault(e => e.Tipo == ScoolManager.Core.Enums.TipoEncarregado.Pai);
            var mae = aluno.Encarregados.FirstOrDefault(e => e.Tipo == ScoolManager.Core.Enums.TipoEncarregado.Mae);

            NomePai     = pai?.Nome ?? string.Empty;
            ContactoPai = pai?.Contacto ?? string.Empty;
            NomeMae     = mae?.Nome ?? string.Empty;
            ContactoMae = mae?.Contacto ?? string.Empty;

            Documentos.Clear();
            foreach (var doc in aluno.Documentos)
            {
                Documentos.Add(new DocumentoAlunoItem(
                    TipoDocumentoLabel(doc.Tipo),
                    doc.NomeArquivo,
                    doc.DataUpload));
            }
        }
        catch
        {
            // Mantém o que veio da lista; sem mock
        }
    }

    private static string TipoDocumentoLabel(ScoolManager.Core.Enums.TipoDocumentoAluno tipo) => tipo switch
    {
        ScoolManager.Core.Enums.TipoDocumentoAluno.BiCedula       => "BI / Cédula",
        ScoolManager.Core.Enums.TipoDocumentoAluno.Certificado    => "Certificado / Declaração",
        ScoolManager.Core.Enums.TipoDocumentoAluno.FotoTipoPasse  => "Foto Tipo Passe",
        ScoolManager.Core.Enums.TipoDocumentoAluno.AtestadoMedico => "Atestado Médico",
        _ => tipo.ToString()
    };
}

/// <summary>Item exibido na aba "Documentação". Somente leitura nesta view.</summary>
public sealed class DocumentoAlunoItem
{
    public string Tipo { get; }
    public string? NomeArquivo { get; }
    public DateTime? DataUpload { get; }

    public bool TemArquivo => !string.IsNullOrEmpty(NomeArquivo);
    public string NomeArquivoOuPlaceholder => NomeArquivo ?? "Nenhum ficheiro enviado";
    public string DataUploadLabel => DataUpload?.ToString("dd/MM/yyyy") ?? "—";

    public DocumentoAlunoItem(string tipo, string? nomeArquivo, DateTime? dataUpload)
    {
        Tipo = tipo;
        NomeArquivo = nomeArquivo;
        DataUpload = dataUpload;
    }
}

/// <summary>Linha da tabela exibida na aba "Histórico Financeiro".</summary>
public sealed class PagamentoHistoricoItem
{
    public string MesReferencia { get; }
    public string NumeroRecibo { get; }
    public string Valor { get; }
    public string Data { get; }
    public bool Pago { get; }

    public string StatusTexto => Pago ? "Pago" : "Em atraso";
    public IBrush StatusBrush => new SolidColorBrush(Color.Parse(Pago ? "#34D399" : "#FFB4AB"));

    public PagamentoHistoricoItem(string mesReferencia, string numeroRecibo, string valor, string data, bool pago)
    {
        MesReferencia = mesReferencia;
        NumeroRecibo = numeroRecibo;
        Valor = valor;
        Data = data;
        Pago = pago;
    }
}

} // fim namespace ScoolManager.Desktop.ViewModels.Pages
