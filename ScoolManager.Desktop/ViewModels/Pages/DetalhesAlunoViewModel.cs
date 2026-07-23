using System;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
public partial class DetalhesAlunoViewModel : ViewModelBase
{
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
    private void ConfirmarExclusao()
    {
        // TODO: remover o aluno de facto quando existir um serviço/persistência real.
        FecharModal();
        ExclusaoConfirmada?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Disparado pelo botão discreto "Voltar para Alunos" do cabeçalho.</summary>
    public event EventHandler? VoltarParaAlunosSolicitado;

    [RelayCommand]
    private void VoltarParaAlunos() => VoltarParaAlunosSolicitado?.Invoke(this, EventArgs.Empty);

    // ================================================================
    // Construtores
    // ================================================================

    /// <summary>
    /// Constrói os detalhes a partir de uma linha da lista de Alunos.
    /// Os campos que a lista não tem (data de nascimento, documentos, histórico
    /// de pagamentos, etc.) ficam com dados mock até existir um serviço real.
    /// </summary>
    public DetalhesAlunoViewModel(AlunoListItemModel aluno)
    {
        NomeCompleto = aluno.Nome;
        CodigoMatricula = aluno.Codigo;
        Classe = aluno.Classe;
        Turma = aluno.Sala;
        Curso = aluno.Curso;
        Ativo = aluno.Ativo;
        Telefone = aluno.Telefone;

        NomePai = aluno.Encarregado;
        ContactoPai = aluno.Telefone;

        PreencherDadosMock();
    }

    /// <summary>Usado pelo designer (Design.DataContext) e como fallback.</summary>
    public DetalhesAlunoViewModel() : this(CriarAlunoMock())
    {
    }

    private void PreencherDadosMock()
    {
        DataNascimento = new DateTimeOffset(2009, 5, 12, 0, 0, 0, TimeSpan.Zero);
        Genero = "Masculino";
        Nacionalidade = "Angolana";
        NumeroBiCedula = "007654321LA045";
        Endereco = "Bairro Talatona, Rua 12, Vivenda 45A, Luanda";
        Email = $"{NomeCompleto.Split(' ')[0].ToLowerInvariant()}@estudante.com";

        AnoLectivo = "2025/2026";
        DataMatricula = new DateTimeOffset(2025, 1, 15, 0, 0, 0, TimeSpan.Zero);

        PropinasPagas = 4;
        PropinasTotais = 10;
        SaldoDevedorLabel = "0,00 Kz";

        Documentos.Clear();
        Documentos.Add(new DocumentoAlunoItem("Certificado / Declaração", "certificado_9classe.pdf", DateTime.Now.AddMonths(-6)));
        Documentos.Add(new DocumentoAlunoItem("Foto Tipo Passe", "foto_perfil.jpg", DateTime.Now.AddMonths(-6)));
        Documentos.Add(new DocumentoAlunoItem("BI / Cédula", "bi_cedula.pdf", DateTime.Now.AddMonths(-6)));
        Documentos.Add(new DocumentoAlunoItem("Atestado Médico", null, null));

        HistoricoPagamentos.Clear();
        HistoricoPagamentos.Add(new PagamentoHistoricoItem("Abril 2026", "#REC-4560", "25.000 Kz", "02/04/2026", true));
        HistoricoPagamentos.Add(new PagamentoHistoricoItem("Março 2026", "#REC-4412", "25.000 Kz", "05/03/2026", true));
        HistoricoPagamentos.Add(new PagamentoHistoricoItem("Fevereiro 2026", "#REC-4288", "25.000 Kz", "01/02/2026", true));
        HistoricoPagamentos.Add(new PagamentoHistoricoItem("Janeiro 2026", "#REC-4133", "25.000 Kz", "—", false));
    }

    private static AlunoListItemModel CriarAlunoMock() => new(
        Codigo: "2026/0842",
        Nome: "João Pedro da Silva",
        Classe: "10ª Classe A",
        Curso: "Ciências",
        Sala: "Sala 12",
        Encarregado: "Ricardo da Silva",
        Telefone: "+244 923 000 111",
        Ativo: true);
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
