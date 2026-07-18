
namespace ScoolManager.Desktop.ViewModels.Pages
{

/// <summary>
/// ViewModel da view "Alunos" (Secretaria Escolar).
/// Responsável por: pesquisa, filtros, listagem e disparo dos 5 modais
/// (Nova Matrícula, Importar Alunos, Exportar PDF, Exportar Excel, Filtros Avançados).
///
/// IMPORTANTE (conforme especificação): cada linha da tabela é clicável e
/// navega para "Detalhes do Aluno" — não existe botão "Ver Detalhes".
/// </summary>
public partial class AlunosViewModel : ViewModelBase
{
    // Fonte completa dos dados (mock). A pesquisa/filtros trabalham sobre esta lista
    // e só o resultado é publicado em "Alunos". TODO: substituir por um IAlunoService real.
    private readonly List<AlunoListItemModel> _todosAlunos;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string? _classeSelecionada;

    [ObservableProperty]
    private string? _statusSelecionado;

    [ObservableProperty]
    private string? _anoLetivoSelecionado;

    // ===== Estado dos 5 modais da view =====
    [ObservableProperty] private bool _isNovaMatriculaAberta;
    [ObservableProperty] private bool _isImportarAlunosAberta;
    [ObservableProperty] private bool _isExportarPdfAberta;
    [ObservableProperty] private bool _isExportarExcelAberta;
    [ObservableProperty] private bool _isFiltrosAvancadosAberta;

    /// <summary>Usado pelo overlay/backdrop no XAML: verdadeiro se qualquer modal estiver aberto.</summary>
    public bool AlgumModalAberto =>
        IsNovaMatriculaAberta || IsImportarAlunosAberta || IsExportarPdfAberta ||
        IsExportarExcelAberta || IsFiltrosAvancadosAberta;

    public ObservableCollection<AlunoListItemModel> Alunos { get; } = new();

    public ObservableCollection<string> Classes { get; }
    public ObservableCollection<string> StatusOptions { get; }
    public ObservableCollection<string> AnosLetivos { get; }

    public string ResumoExibicaoLabel => Alunos.Count == 0
        ? "Nenhum aluno encontrado"
        : $"A exibir 1-{Alunos.Count} de {Alunos.Count} alunos";

    /// <summary>
    /// Disparado quando o utilizador clica numa linha. O code-behind da View
    /// subscreve isto para, no futuro, navegar até "Detalhes do Aluno".
    /// </summary>
    public event EventHandler<AlunoListItemModel>? DetalhesAlunoSolicitado;

    public AlunosViewModel()
    {
        _todosAlunos = CriarDadosMock();

        Classes = new ObservableCollection<string>
        {
            "Todas as Classes", "6ª Classe B", "7ª Classe A", "9ª Classe B", "10ª Classe A"
        };
        StatusOptions = new ObservableCollection<string> { "Status: Todos", "Ativos", "Inativos" };
        AnosLetivos = new ObservableCollection<string> { "Ano: 2025/2026", "Ano: 2024/2025" };

        _classeSelecionada = Classes[0];
        _statusSelecionado = StatusOptions[0];
        _anoLetivoSelecionado = AnosLetivos[0];

        AplicarFiltros();
    }

    partial void OnSearchTextChanged(string value) => AplicarFiltros();
    partial void OnClasseSelecionadaChanged(string? value) => AplicarFiltros();
    partial void OnStatusSelecionadoChanged(string? value) => AplicarFiltros();
    partial void OnAnoLetivoSelecionadoChanged(string? value) => AplicarFiltros();

    partial void OnIsNovaMatriculaAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsImportarAlunosAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsExportarPdfAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsExportarExcelAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsFiltrosAvancadosAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));

    private void AplicarFiltros()
    {
        IEnumerable<AlunoListItemModel> query = _todosAlunos;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var termo = SearchText.Trim();
            query = query.Where(a =>
                a.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                a.NumeroEstudante.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                a.Encarregado.Contains(termo, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(ClasseSelecionada) && ClasseSelecionada != Classes[0])
            query = query.Where(a => a.Classe == ClasseSelecionada);

        if (!string.IsNullOrEmpty(StatusSelecionado) && StatusSelecionado != StatusOptions[0])
        {
            var ativo = StatusSelecionado == "Ativos";
            query = query.Where(a => a.Ativo == ativo);
        }

        Alunos.Clear();
        foreach (var aluno in query)
            Alunos.Add(aluno);

        OnPropertyChanged(nameof(ResumoExibicaoLabel));
    }

    // ===== Fluxo principal: clicar na linha -> Detalhes do Aluno =====
    [RelayCommand]
    private void AbrirDetalhes(AlunoListItemModel? aluno)
    {
        if (aluno is null) return;

        // TODO: quando a view "Detalhes do Aluno" existir, trocar o CurrentPage
        // do MainWindowViewModel por uma DetalhesAlunoViewModel(aluno.NumeroEstudante).
        DetalhesAlunoSolicitado?.Invoke(this, aluno);
    }

    // ===== Os 5 modais da view Alunos =====
    [RelayCommand] private void AbrirNovaMatricula() => IsNovaMatriculaAberta = true;
    [RelayCommand] private void AbrirImportarAlunos() => IsImportarAlunosAberta = true;
    [RelayCommand] private void AbrirExportarPdf() => IsExportarPdfAberta = true;
    [RelayCommand] private void AbrirExportarExcel() => IsExportarExcelAberta = true;
    [RelayCommand] private void AbrirFiltrosAvancados() => IsFiltrosAvancadosAberta = true;

    [RelayCommand]
    private void FecharModal()
    {
        IsNovaMatriculaAberta = false;
        IsImportarAlunosAberta = false;
        IsExportarPdfAberta = false;
        IsExportarExcelAberta = false;
        IsFiltrosAvancadosAberta = false;
    }

    // TODO: substituir os placeholders abaixo pela ligação real aos serviços
    // (matricular aluno, importar CSV/Excel, gerar PDF/Excel) quando existirem.
    [RelayCommand] private void ConfirmarNovaMatricula() => FecharModal();
    [RelayCommand] private void ConfirmarImportarAlunos() => FecharModal();
    [RelayCommand] private void ConfirmarExportarPdf() => FecharModal();
    [RelayCommand] private void ConfirmarExportarExcel() => FecharModal();
    [RelayCommand] private void ConfirmarFiltrosAvancados() => FecharModal();

    private static List<AlunoListItemModel> CriarDadosMock() => new()
    {
        new("2026/0842", "João Pedro da Silva",   "10ª Classe A", "Ricardo da Silva", "+244 923 000 111", true),
        new("2026/1205", "Maria Luísa Alberto",    "9ª Classe B",  "Isabel Alberto",   "+244 931 444 222", true),
        new("2026/0591", "Manuel Francisco",       "7ª Classe A",  "Ana Francisco",    "+244 944 888 777", false),
        new("2026/0998", "Ana Paula Domingos",     "10ª Classe A", "José Domingos",    "+244 923 111 222", true),
        new("2026/1423", "Carlos Manuel",          "6ª Classe B",  "Isabel Manuel",    "+244 928 555 333", true),
    };
}

} // fim namespace ScoolManager.Desktop.ViewModels.Pages
