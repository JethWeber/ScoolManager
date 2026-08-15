using System;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.ViewModels.Pages
{

/// <summary>
/// ViewModel da view "Detalhes do Aluno" (View 3 da Secretaria Escolar).
///
/// Centraliza as informações do aluno em 3 abas (Dados Pessoais, Documentação,
/// Histórico Financeiro) e expõe as 3 ações do cabeçalho (Editar Perfil,
/// Efetuar Pagamento, Renovar Matrícula) + Excluir Aluno via menu "⋮".
///
/// IMPORTANTE: esta view não depende do ScoolManager.Core (ainda vazio).
/// Os dados são locais/mock, assim como em AlunosViewModel. Quando o Core
/// tiver as entidades reais, trocar estes campos por bindings ao serviço.
/// </summary>
public partial class DetalhesAlunoViewModel : ViewModelBase, IAsyncInitializable
{
    private readonly IAlunoService _alunoService;
    private readonly int? _alunoId;
    public enum Aba
    {
        DadosPessoais,
        Documentacao,
        HistoricoFinanceiro
    }

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

    partial void OnNomeCompletoChanged(string value) => OnPropertyChanged(nameof(Iniciais));

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

    // ===== Os 4 modais da spec (Editar Aluno, Efetuar Pagamento, Renovar Matrícula, Confirmar Exclusão) =====
    [ObservableProperty] private bool _isEditarPerfilAberto;
    [ObservableProperty] private bool _isEfetuarPagamentoAberto;
    [ObservableProperty] private bool _isRenovarMatriculaAberto;
    [ObservableProperty] private bool _isConfirmarExclusaoAberto;

    public bool AlgumModalAberto =>
        IsEditarPerfilAberto || IsEfetuarPagamentoAberto || IsRenovarMatriculaAberto || IsConfirmarExclusaoAberto;

    partial void OnIsEditarPerfilAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsEfetuarPagamentoAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsRenovarMatriculaAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsConfirmarExclusaoAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));

    [RelayCommand] private void AbrirEditarPerfil() => IsEditarPerfilAberto = true;
    [RelayCommand] private void AbrirEfetuarPagamento() => IsEfetuarPagamentoAberto = true;
    [RelayCommand] private void AbrirRenovarMatricula() => IsRenovarMatriculaAberto = true;
    [RelayCommand] private void AbrirConfirmarExclusao() => IsConfirmarExclusaoAberto = true;

    [RelayCommand]
    private void FecharModal()
    {
        IsEditarPerfilAberto = false;
        IsEfetuarPagamentoAberto = false;
        IsRenovarMatriculaAberto = false;
        IsConfirmarExclusaoAberto = false;
    }

    // TODO: ligar aos serviços reais quando existirem (persistência, geração de recibo, etc.)
    [RelayCommand] private void ConfirmarEditarPerfil() => FecharModal();
    [RelayCommand] private void ConfirmarRenovarMatricula() => FecharModal();

    [RelayCommand]
    private void ConfirmarEfetuarPagamento()
    {
        // TODO: substituir por chamada real ao módulo Financeiro.
        HistoricoPagamentos.Insert(0, new PagamentoHistoricoItem(
            mesReferencia: DateTime.Now.ToString("MMMM yyyy"),
            numeroRecibo: $"#REC-{Random.Shared.Next(1000, 9999)}",
            valor: "25.000 Kz",
            data: DateTime.Now.ToString("dd/MM/yyyy"),
            pago: true));

        if (PropinasPagas < PropinasTotais)
            PropinasPagas++;

        FecharModal();
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

    public DetalhesAlunoViewModel(int alunoId, IAlunoService alunoService)
    {
        _alunoService = alunoService;
        _alunoId = alunoId;
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
