
using System.Threading.Tasks;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Alunos;
using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Desktop.ViewModels.Pages
{

/// <summary>
/// ViewModel da view "Alunos" (Secretaria Escolar).
/// Responsável por: pesquisa, filtros, listagem e disparo dos modais
/// (Novo Aluno em passos, Importar Alunos, Exportar PDF, Exportar Excel, Filtros Avançados).
///
/// IMPORTANTE (conforme especificação): cada linha da tabela é clicável e
/// navega para "Detalhes do Aluno" — não existe botão "Ver Detalhes".
///
/// A tabela principal mostra apenas: Código, Nome, Classe, Curso e Sala.
/// Os restantes dados (encarregado, telefone, documentos, etc.) ficam
/// disponíveis no modelo para a futura view de Detalhes do Aluno.
/// </summary>
public partial class AlunosViewModel : ViewModelBase, IAsyncInitializable
{
    private readonly IAlunoService _alunoService;
    private readonly IEscolaService _escolaService;
    private readonly List<AlunoListItemModel> _todosAlunos = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string? _classeSelecionada;

    [ObservableProperty]
    private string? _statusSelecionado;

    [ObservableProperty]
    private string? _anoLetivoSelecionado;

    // ===== Estado dos modais da view =====
    [ObservableProperty] private bool _isNovoAlunoAberto;
    [ObservableProperty] private bool _isImportarAlunosAberta;
    [ObservableProperty] private bool _isExportarPdfAberta;
    [ObservableProperty] private bool _isExportarExcelAberta;
    [ObservableProperty] private bool _isFiltrosAvancadosAberta;

    /// <summary>Usado pelo overlay/backdrop no XAML: verdadeiro se qualquer modal estiver aberto.</summary>
    public bool AlgumModalAberto =>
        IsNovoAlunoAberto || IsImportarAlunosAberta || IsExportarPdfAberta ||
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

    // ================================================================
    // ===== WIZARD "NOVO ALUNO" (formulário de matrícula, por passos) ==
    // ================================================================

    public const int TotalDePassos = 4;

    [ObservableProperty]
    private int _passoAtual = 1;

    // --- Opções para os ComboBox do formulário ---
    public ObservableCollection<string> ClassesDisponiveis { get; }
    public ObservableCollection<string> OpcoesSexo { get; } = new() { "Masculino", "Feminino" };
    public ObservableCollection<string> OpcoesTurno { get; } = new() { "Manhã", "Tarde", "Noite" };
    public ObservableCollection<string> OpcoesPeriodo { get; } = new() { "Integral", "Meio Período" };

    // --- Passo 1: Dados do Aluno ---
    [ObservableProperty] private string _nomeCompleto = string.Empty;
    [ObservableProperty] private DateTimeOffset? _dataNascimento;
    [ObservableProperty] private string? _sexo;
    [ObservableProperty] private string _naturalidade = string.Empty;
    [ObservableProperty] private string _provincia = string.Empty;
    [ObservableProperty] private string _pais = string.Empty;
    [ObservableProperty] private string _numeroBiCedulaAluno = string.Empty;
    [ObservableProperty] private string _morada = string.Empty;
    [ObservableProperty] private bool _sofreDoencaSim;
    [ObservableProperty] private bool _sofreDoencaNao = true;
    [ObservableProperty] private string _qualDoenca = string.Empty;

    // --- Passo 2: Dados dos Encarregados ---
    [ObservableProperty] private string _nomePai = string.Empty;
    [ObservableProperty] private string _profissaoPai = string.Empty;
    [ObservableProperty] private string _contactoPai = string.Empty;
    [ObservableProperty] private string _nomeMae = string.Empty;
    [ObservableProperty] private string _profissaoMae = string.Empty;
    [ObservableProperty] private string _contactoMae = string.Empty;

    // --- Passo 3: Enquadramento na Instituição ---
    [ObservableProperty] private string? _classeMatricula;
    [ObservableProperty] private string _cursoMatricula = string.Empty;
    [ObservableProperty] private string? _turno;
    [ObservableProperty] private string? _periodo;
    [ObservableProperty] private string _salaMatricula = string.Empty;

    // --- Passo 4: Documentos. Cada um é uma zona de arrastar-e-soltar própria
    //     (ver AlunosView.axaml, Passo 4). Guardamos apenas o nome do ficheiro
    //     escolhido; a leitura/armazenamento real do ficheiro fica para quando
    //     existir um serviço de documentos. BI/Cédula é o único obrigatório.
    public DocumentoRequeridoItem CertificadoDocumento { get; } = new("Certificado / Declaração", obrigatorio: false);
    public DocumentoRequeridoItem FotoDocumento { get; } = new("Foto Tipo Passe", obrigatorio: false);
    public DocumentoRequeridoItem BiCedulaDocumento { get; } = new("BI / Cédula", obrigatorio: true);
    public DocumentoRequeridoItem AtestadoDocumento { get; } = new("Atestado Médico", obrigatorio: false);

    // --- Propriedades computadas do wizard ---
    public bool EhPasso1 => PassoAtual == 1;
    public bool EhPasso2 => PassoAtual == 2;
    public bool EhPasso3 => PassoAtual == 3;
    public bool EhPasso4 => PassoAtual == 4;

    public bool PodeVoltar => PassoAtual > 1;

    public string TituloPassoAtual => PassoAtual switch
    {
        1 => "Dados do Aluno",
        2 => "Dados dos Encarregados",
        3 => "Enquadramento na Instituição",
        4 => "Documentos",
        _ => string.Empty
    };

    public string TextoBotaoAvancar => PassoAtual < TotalDePassos ? "Avançar" : "Concluir Matrícula";

    public bool PodeAvancar => PassoAtual switch
    {
        1 => !string.IsNullOrWhiteSpace(NomeCompleto),
        3 => !string.IsNullOrWhiteSpace(ClasseMatricula),
        4 => BiCedulaDocumento.TemArquivo,
        _ => true
    };

    public AlunosViewModel(IAlunoService alunoService, IEscolaService escolaService)
    {
        _alunoService = alunoService;
        _escolaService = escolaService;

        Classes = new ObservableCollection<string>
        {
            "Todas as Classes", "6ª Classe B", "7ª Classe A", "9ª Classe B", "10ª Classe A"
        };
        StatusOptions = new ObservableCollection<string> { "Status: Todos", "Ativos", "Inativos" };
        AnosLetivos = new ObservableCollection<string> { "Ano: 2025/2026", "Ano: 2024/2025" };

        ClassesDisponiveis = new ObservableCollection<string>(Classes.Skip(1));

        foreach (var documento in new[] { CertificadoDocumento, FotoDocumento, BiCedulaDocumento, AtestadoDocumento })
            documento.PropertyChanged += (_, _) => OnPropertyChanged(nameof(PodeAvancar));

        _classeSelecionada = Classes[0];
        _statusSelecionado = StatusOptions[0];
        _anoLetivoSelecionado = AnosLetivos[0];
    }

    public async Task InitializeAsync()
    {
        try
        {
            var filtro = new FiltroAlunoDto
            {
                TextoBusca = SearchText,
                Situacao = StatusSelecionado == StatusOptions[0] ? null : StatusSelecionado,
                Classe = ClasseSelecionada == Classes[0] ? null : ClasseSelecionada,
                ApenasAtivos = StatusSelecionado == "Ativos"
            };

            var alunos = await _alunoService.ObterListaAsync(filtro);
            _todosAlunos.Clear();

            foreach (var aluno in alunos)
            {
                var turmaNome = aluno.Turma?.Nome ?? "Sem turma";
                var primeiroEncarregado = aluno.Encarregados.FirstOrDefault();
                _todosAlunos.Add(new AlunoListItemModel(
                    Codigo: aluno.Codigo,
                    Nome: aluno.Nome,
                    Classe: turmaNome,
                    Curso: aluno.Turma?.Curso?.Nome ?? string.Empty,
                    Sala: aluno.Turma?.Sala?.Nome ?? string.Empty,
                    Encarregado: primeiroEncarregado?.Nome ?? string.Empty,
                    Telefone: primeiroEncarregado?.Contacto ?? aluno.Telefone,
                    Ativo: aluno.Ativo));
            }

            AplicarFiltros();
        }
        catch (Exception)
        {
            _todosAlunos.Clear();
            AplicarFiltros();
        }
    }

    partial void OnSearchTextChanged(string value) => AplicarFiltros();
    partial void OnClasseSelecionadaChanged(string? value) => AplicarFiltros();
    partial void OnStatusSelecionadoChanged(string? value) => AplicarFiltros();
    partial void OnAnoLetivoSelecionadoChanged(string? value) => AplicarFiltros();

    partial void OnIsNovoAlunoAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsImportarAlunosAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsExportarPdfAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsExportarExcelAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsFiltrosAvancadosAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));

    // --- Notificações para as propriedades computadas do wizard ---
    partial void OnPassoAtualChanged(int value)
    {
        OnPropertyChanged(nameof(EhPasso1));
        OnPropertyChanged(nameof(EhPasso2));
        OnPropertyChanged(nameof(EhPasso3));
        OnPropertyChanged(nameof(EhPasso4));
        OnPropertyChanged(nameof(PodeVoltar));
        OnPropertyChanged(nameof(TituloPassoAtual));
        OnPropertyChanged(nameof(TextoBotaoAvancar));
        OnPropertyChanged(nameof(PodeAvancar));
    }

    partial void OnNomeCompletoChanged(string value) => OnPropertyChanged(nameof(PodeAvancar));
    partial void OnClasseMatriculaChanged(string? value) => OnPropertyChanged(nameof(PodeAvancar));

    private void AplicarFiltros()
    {
        IEnumerable<AlunoListItemModel> query = _todosAlunos;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var termo = SearchText.Trim();
            query = query.Where(a =>
                a.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                a.Codigo.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                a.Curso.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
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
        // do MainWindowViewModel por uma DetalhesAlunoViewModel(aluno.Codigo).
        DetalhesAlunoSolicitado?.Invoke(this, aluno);
    }

    // ===== Novo Aluno (wizard por passos) =====
    [RelayCommand]
    private void AbrirNovoAluno()
    {
        LimparFormularioNovoAluno();
        PassoAtual = 1;
        IsNovoAlunoAberto = true;
    }

    [RelayCommand]
    private void PassoAnterior()
    {
        if (PassoAtual > 1)
            PassoAtual--;
    }

    [RelayCommand]
    private async Task AvancarOuConcluir()
    {
        if (!PodeAvancar) return;

        if (PassoAtual < TotalDePassos)
        {
            PassoAtual++;
            return;
        }

        await ConcluirNovoAluno();
    }

    private async Task ConcluirNovoAluno()
    {
        try
        {
            var turma = await ResolverTurmaDestinoAsync();
            var aluno = new Aluno
            {
                Codigo = GerarNovoCodigo(),
                Nome = NomeCompleto.Trim(),
                DataNascimento = DataNascimento?.DateTime,
                Genero = Sexo,
                Naturalidade = Naturalidade,
                Provincia = Provincia,
                Pais = Pais,
                NumeroBiCedula = NumeroBiCedulaAluno,
                Endereco = Morada,
                Telefone = !string.IsNullOrWhiteSpace(ContactoPai) ? ContactoPai : ContactoMae,
                Email = null,
                Ativo = true,
                TurmaId = turma.Id,
                AnoLectivoId = turma.AnoLectivoId,
                DataMatricula = DateTime.Today,
                TemCondicaoMedica = SofreDoencaSim,
                DescricaoCondicaoMedica = QualDoenca,
                Encarregados = new List<Encarregado>(),
                Documentos = new List<DocumentoAluno>()
            };

            if (!string.IsNullOrWhiteSpace(NomePai))
                aluno.Encarregados.Add(new Encarregado { Tipo = TipoEncarregado.Pai, Nome = NomePai, Contacto = ContactoPai, Profissao = ProfissaoPai });

            if (!string.IsNullOrWhiteSpace(NomeMae))
                aluno.Encarregados.Add(new Encarregado { Tipo = TipoEncarregado.Mae, Nome = NomeMae, Contacto = ContactoMae, Profissao = ProfissaoMae });

            if (BiCedulaDocumento.TemArquivo)
                aluno.Documentos.Add(new DocumentoAluno { Tipo = TipoDocumentoAluno.BiCedula, NomeArquivo = BiCedulaDocumento.NomeArquivo, DataUpload = DateTime.Now });

            await _alunoService.CriarAsync(aluno, aluno.Encarregados);

            LimparFormularioNovoAluno();
            await InitializeAsync();
            FecharModal();
        }
        catch (Exception)
        {
            FecharModal();
        }
    }

    private async Task<ScoolManager.Core.Entities.Escola.Turma> ResolverTurmaDestinoAsync()
    {
        var turmas = (await _escolaService.ObterTurmasAsync()).ToList();
        if (turmas.Count == 0)
            throw new InvalidOperationException("Não existe nenhuma turma criada para associar o aluno.");

        var numeroClasse = ExtrairNumeroClasse(ClasseMatricula);
        var letraTurma = ExtrairUltimaLetra(ClasseMatricula);

        var turmaMatch = turmas.FirstOrDefault(t =>
            t.Classe?.Numero == numeroClasse &&
            (!letraTurma.HasValue || t.Letra == letraTurma.Value) &&
            (string.IsNullOrWhiteSpace(CursoMatricula) ||
                t.Curso is null ||
                t.Curso.Nome.Contains(CursoMatricula, StringComparison.OrdinalIgnoreCase) ||
                t.Curso.Sigla.Contains(CursoMatricula, StringComparison.OrdinalIgnoreCase)));

        if (turmaMatch is not null) return turmaMatch;

        return turmas.FirstOrDefault(t => t.Classe?.Numero == numeroClasse) ?? turmas.First();
    }

    private static int? ExtrairNumeroClasse(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return null;

        var digits = new string(texto.Where(char.IsDigit).ToArray());
        return int.TryParse(digits, out var numero) ? numero : null;
    }

    private static char? ExtrairUltimaLetra(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return null;

        for (var i = texto.Length - 1; i >= 0; i--)
        {
            if (char.IsLetter(texto[i]))
                return char.ToUpperInvariant(texto[i]);
        }

        return null;
    }

    private string GerarNovoCodigo()
    {
        var ano = DateTime.Now.Year;
        var sequencial = _todosAlunos.Count + 1;
        return $"{ano}/{sequencial:0000}";
    }

    private void LimparFormularioNovoAluno()
    {
        NomeCompleto = string.Empty;
        DataNascimento = null;
        Sexo = null;
        Naturalidade = string.Empty;
        Provincia = string.Empty;
        Pais = string.Empty;
        NumeroBiCedulaAluno = string.Empty;
        Morada = string.Empty;
        SofreDoencaNao = true;
        SofreDoencaSim = false;
        QualDoenca = string.Empty;

        NomePai = string.Empty;
        ProfissaoPai = string.Empty;
        ContactoPai = string.Empty;
        NomeMae = string.Empty;
        ProfissaoMae = string.Empty;
        ContactoMae = string.Empty;

        ClasseMatricula = null;
        CursoMatricula = string.Empty;
        Turno = null;
        Periodo = null;
        SalaMatricula = string.Empty;

        foreach (var documento in new[] { CertificadoDocumento, FotoDocumento, BiCedulaDocumento, AtestadoDocumento })
            documento.NomeArquivo = DocumentoRequeridoItem.SemFicheiroPlaceholder;
    }

    // ===== Os demais modais da view Alunos =====
    [RelayCommand] private void AbrirImportarAlunos() => IsImportarAlunosAberta = true;
    [RelayCommand] private void AbrirExportarPdf() => IsExportarPdfAberta = true;
    [RelayCommand] private void AbrirExportarExcel() => IsExportarExcelAberta = true;
    [RelayCommand] private void AbrirFiltrosAvancados() => IsFiltrosAvancadosAberta = true;

    [RelayCommand]
    private void FecharModal()
    {
        IsNovoAlunoAberto = false;
        IsImportarAlunosAberta = false;
        IsExportarPdfAberta = false;
        IsExportarExcelAberta = false;
        IsFiltrosAvancadosAberta = false;
    }

    // TODO: substituir os placeholders abaixo pela ligação real aos serviços
    // (importar CSV/Excel, gerar PDF/Excel) quando existirem.
    [RelayCommand] private void ConfirmarImportarAlunos() => FecharModal();
    [RelayCommand] private void ConfirmarExportarPdf() => FecharModal();
    [RelayCommand] private void ConfirmarExportarExcel() => FecharModal();
    [RelayCommand] private void ConfirmarFiltrosAvancados() => FecharModal();

}

/// <summary>
/// Representa um dos documentos que podem ser exigidos no Passo 4 do
/// wizard "Novo Aluno" (Certificado/Declaração, Foto Tipo Passe, BI/Cédula,
/// Atestado Médico). É selecionado através do dropdown e o nome do
/// ficheiro escolhido é atribuído a <see cref="NomeArquivo"/> pelo
/// code-behind (após o utilizador escolher um ficheiro no seletor).
/// </summary>
public partial class DocumentoRequeridoItem : ObservableObject
{
    public const string SemFicheiroPlaceholder = "Nenhum ficheiro selecionado";

    public string Tipo { get; }
    public bool Obrigatorio { get; }

    [ObservableProperty]
    private string _nomeArquivo = SemFicheiroPlaceholder;

    public bool TemArquivo => NomeArquivo != SemFicheiroPlaceholder;

    public DocumentoRequeridoItem(string tipo, bool obrigatorio)
    {
        Tipo = tipo;
        Obrigatorio = obrigatorio;
    }

    partial void OnNomeArquivoChanged(string value) => OnPropertyChanged(nameof(TemArquivo));
}

} // fim namespace ScoolManager.Desktop.ViewModels.Pages
