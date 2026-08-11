using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Alunos;
using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>
/// ViewModel da view "Alunos" (Secretaria Escolar).
/// Todos os dados de listagem, filtros e wizard vêm do ScoolManager.Core
/// (IAlunoService + IEscolaService). Não há mocks.
/// </summary>
public partial class AlunosViewModel : ViewModelBase, IAsyncInitializable
{
    private readonly IAlunoService _alunoService;
    private readonly IEscolaService _escolaService;

    /// <summary>Fonte completa (sem filtro de UI) para reaplicar pesquisa/filtros localmente.</summary>
    private readonly List<AlunoListItemModel> _todosAlunos = new();

    // =================================================================
    // Filtros da listagem
    // =================================================================

    [ObservableProperty] private string _searchText = string.Empty;

    /// <summary>"Todas as Classes" ou o nome da classe (ex.: "10ª").</summary>
    [ObservableProperty] private string? _classeSelecionada;

    /// <summary>"Status: Todos" | "Ativos" | "Inativos".</summary>
    [ObservableProperty] private string? _statusSelecionado;

    /// <summary>"Ano: Todos" ou o nome do ano lectivo (ex.: "2025/2026").</summary>
    [ObservableProperty] private string? _anoLetivoSelecionado;

    // =================================================================
    // Estado dos modais
    // =================================================================

    [ObservableProperty] private bool _isNovoAlunoAberto;
    [ObservableProperty] private bool _isImportarAlunosAberta;
    [ObservableProperty] private bool _isExportarPdfAberta;
    [ObservableProperty] private bool _isExportarExcelAberta;
    [ObservableProperty] private bool _isFiltrosAvancadosAberta;

    public bool AlgumModalAberto =>
        IsNovoAlunoAberto || IsImportarAlunosAberta || IsExportarPdfAberta ||
        IsExportarExcelAberta || IsFiltrosAvancadosAberta;

    // =================================================================
    // Listagens e opções de filtro (populadas a partir do Core)
    // =================================================================

    public ObservableCollection<AlunoListItemModel> Alunos { get; } = new();

    /// <summary>Inclui sempre "Todas as Classes" no índice 0.</summary>
    public ObservableCollection<string> Classes { get; } = new();

    public ObservableCollection<string> StatusOptions { get; } = new()
    {
        "Status: Todos", "Ativos", "Inativos"
    };

    /// <summary>Inclui sempre "Ano: Todos" no índice 0.</summary>
    public ObservableCollection<string> AnosLetivos { get; } = new();

    public string ResumoExibicaoLabel => Alunos.Count == 0
        ? "Nenhum aluno encontrado"
        : $"A exibir 1-{Alunos.Count} de {Alunos.Count} alunos";

    /// <summary>Clique na linha → navegação para Detalhes do Aluno (code-behind).</summary>
    public event EventHandler<AlunoListItemModel>? DetalhesAlunoSolicitado;

    // =================================================================
    // Wizard "Novo Aluno"
    // =================================================================

    public const int TotalDePassos = 4;

    [ObservableProperty] private int _passoAtual = 1;

    // --- Opções dinâmicas do formulário (vindas do Core) ---

    /// <summary>Classes do catálogo (1ª … 13ª) para o passo 3.</summary>
    public ObservableCollection<Classe> ClassesDisponiveis { get; } = new();

    /// <summary>Cursos disponíveis (só relevantes para Ensino Médio).</summary>
    public ObservableCollection<Curso> CursosDisponiveis { get; } = new();

    /// <summary>Turmas abertas (filtradas pela classe/curso escolhidos).</summary>
    public ObservableCollection<Turma> TurmasDisponiveis { get; } = new();

    /// <summary>Salas (só leitura / pré-visualização a partir da turma).</summary>
    public ObservableCollection<Sala> SalasDisponiveis { get; } = new();

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

    // --- Passo 2: Encarregados ---
    [ObservableProperty] private string _nomePai = string.Empty;
    [ObservableProperty] private string _profissaoPai = string.Empty;
    [ObservableProperty] private string _contactoPai = string.Empty;
    [ObservableProperty] private string _nomeMae = string.Empty;
    [ObservableProperty] private string _profissaoMae = string.Empty;
    [ObservableProperty] private string _contactoMae = string.Empty;

    // --- Passo 3: Enquadramento (agora com entidades reais) ---
    [ObservableProperty] private Classe? _classeMatricula;
    [ObservableProperty] private Curso? _cursoMatricula;
    [ObservableProperty] private Turma? _turmaMatricula;
    [ObservableProperty] private string? _turno;          // opcional (já vem da turma)
    [ObservableProperty] private string? _periodo;
    [ObservableProperty] private string _salaMatricula = string.Empty; // só visualização

    /// <summary>Curso só se aplica no Ensino Médio.</summary>
    public bool CursoAplicavel => ClasseMatricula?.Nivel == ScoolManager.Core.Enums.NivelEnsino.Medio;

    // --- Passo 4: Documentos ---
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
        3 => TurmaMatricula is not null,
        4 => BiCedulaDocumento.TemArquivo,
        _ => true
    };

    // Cache interno das turmas (para filtrar no wizard)
    private List<Turma> _turmasCache = new();

    // =================================================================
    // Construtor + Initialize
    // =================================================================

    public AlunosViewModel(IAlunoService alunoService, IEscolaService escolaService)
    {
        _alunoService = alunoService;
        _escolaService = escolaService;

        // Valores iniciais dos filtros (serão reescritos no InitializeAsync)
        Classes.Add("Todas as Classes");
        AnosLetivos.Add("Ano: Todos");
        _classeSelecionada = Classes[0];
        _statusSelecionado = StatusOptions[0];
        _anoLetivoSelecionado = AnosLetivos[0];

        foreach (var documento in new[] { CertificadoDocumento, FotoDocumento, BiCedulaDocumento, AtestadoDocumento })
            documento.PropertyChanged += (_, _) => OnPropertyChanged(nameof(PodeAvancar));
    }

    public async Task InitializeAsync()
    {
        try
        {
            // 1) Carregar catálogos da Escola (dados reais)
            var classes = await _escolaService.ObterClassesAsync();
            var anos = await _escolaService.ObterAnosLectivosAsync();
            var cursos = await _escolaService.ObterCursosAsync();
            var salas = await _escolaService.ObterSalasAsync();
            _turmasCache = (await _escolaService.ObterTurmasAsync()).ToList();

            // Filtro "Classes"
            Classes.Clear();
            Classes.Add("Todas as Classes");
            foreach (var c in classes.OrderBy(c => c.Numero))
                Classes.Add($"{c.Numero}ª");

            // Filtro "Anos Lectivos"
            AnosLetivos.Clear();
            AnosLetivos.Add("Ano: Todos");
            foreach (var a in anos.OrderByDescending(a => a.DataInicio))
                AnosLetivos.Add($"Ano: {a.Nome}");

            // Opções do wizard
            ClassesDisponiveis.Clear();
            foreach (var c in classes.OrderBy(c => c.Numero))
                ClassesDisponiveis.Add(c);

            CursosDisponiveis.Clear();
            foreach (var c in cursos.OrderBy(c => c.Nome))
                CursosDisponiveis.Add(c);

            SalasDisponiveis.Clear();
            foreach (var s in salas.OrderBy(s => s.Nome))
                SalasDisponiveis.Add(s);

            // Restaurar seleções de filtro se ainda existirem
            if (string.IsNullOrEmpty(ClasseSelecionada) || !Classes.Contains(ClasseSelecionada))
                ClasseSelecionada = Classes[0];
            if (string.IsNullOrEmpty(AnoLetivoSelecionado) || !AnosLetivos.Contains(AnoLetivoSelecionado))
                AnoLetivoSelecionado = AnosLetivos[0];
            if (string.IsNullOrEmpty(StatusSelecionado))
                StatusSelecionado = StatusOptions[0];

            // 2) Carregar alunos
            await CarregarAlunosAsync();
        }
        catch
        {
            _todosAlunos.Clear();
            AplicarFiltros();
        }
    }

    private async Task CarregarAlunosAsync()
    {
        var filtro = new FiltroAlunoDto
        {
            TextoBusca = string.IsNullOrWhiteSpace(SearchText) ? null : SearchText.Trim(),
            Situacao = StatusSelecionado is null or "Status: Todos" ? null : StatusSelecionado,
            Classe = ClasseSelecionada is null or "Todas as Classes"
                ? null
                : ClasseSelecionada.TrimEnd('ª'), // "10ª" → "10" (o repositório parseia int)
            ApenasAtivos = StatusSelecionado == "Ativos" ? true : null
        };

        var alunos = await _alunoService.ObterListaAsync(filtro);

        _todosAlunos.Clear();
        foreach (var aluno in alunos)
        {
            var primeiroEncarregado = aluno.Encarregados.FirstOrDefault();

            _todosAlunos.Add(new AlunoListItemModel(
                Codigo: aluno.Codigo,
                Nome: aluno.Nome,
                Classe: aluno.Turma?.Nome ?? "Sem turma",
                Curso: aluno.Turma?.Curso?.Nome ?? string.Empty,
                Sala: aluno.Turma?.Sala?.Nome ?? string.Empty,
                Encarregado: primeiroEncarregado?.Nome ?? string.Empty,
                Telefone: primeiroEncarregado?.Contacto ?? aluno.Telefone ?? string.Empty,
                Ativo: aluno.Ativo));
        }

        AplicarFiltros();
    }

    // =================================================================
    // Filtros locais (sobre a lista já carregada)
    // =================================================================

    partial void OnSearchTextChanged(string value) => AplicarFiltros();
    partial void OnClasseSelecionadaChanged(string? value) => _ = RecarregarComFiltroServidorAsync();
    partial void OnStatusSelecionadoChanged(string? value) => _ = RecarregarComFiltroServidorAsync();
    partial void OnAnoLetivoSelecionadoChanged(string? value) => AplicarFiltros(); // filtro local por enquanto

    private async Task RecarregarComFiltroServidorAsync()
    {
        try { await CarregarAlunosAsync(); }
        catch { /* mantém lista actual */ }
    }

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

        // Filtro de ano lectivo (local, pelo texto do nome da turma / se no futuro o DTO trouxer o ano)
        if (!string.IsNullOrEmpty(AnoLetivoSelecionado) && AnoLetivoSelecionado != "Ano: Todos")
        {
            var nomeAno = AnoLetivoSelecionado.Replace("Ano: ", "").Trim();
            // Se o AlunoListItemModel passar a ter AnoLectivo, filtrar aqui.
            // Por agora deixa passar (o servidor já pode filtrar no futuro).
        }

        Alunos.Clear();
        foreach (var aluno in query)
            Alunos.Add(aluno);

        OnPropertyChanged(nameof(ResumoExibicaoLabel));
    }

    // =================================================================
    // Wizard – selecção de Classe / Curso / Turma
    // =================================================================

    partial void OnClasseMatriculaChanged(Classe? value)
    {
        if (value?.Nivel != ScoolManager.Core.Enums.NivelEnsino.Medio)
            CursoMatricula = null;

        OnPropertyChanged(nameof(CursoAplicavel));
        AtualizarTurmasDisponiveis();
        OnPropertyChanged(nameof(PodeAvancar));
    }

    partial void OnCursoMatriculaChanged(Curso? value)
    {
        AtualizarTurmasDisponiveis();
        OnPropertyChanged(nameof(PodeAvancar));
    }

    partial void OnTurmaMatriculaChanged(Turma? value)
    {
        // Pré-preenche sala e turno a partir da turma escolhida
        SalaMatricula = value?.Sala?.Nome ?? string.Empty;
        Turno = value?.Turno switch
        {
            ScoolManager.Core.Enums.TurnoLetivo.Manha => "Manhã",
            ScoolManager.Core.Enums.TurnoLetivo.Tarde => "Tarde",
            ScoolManager.Core.Enums.TurnoLetivo.Noite => "Noite",
            _ => null
        };
        OnPropertyChanged(nameof(PodeAvancar));
    }

    private void AtualizarTurmasDisponiveis()
    {
        TurmasDisponiveis.Clear();
        TurmaMatricula = null;

        if (ClasseMatricula is null) return;

        var query = _turmasCache.Where(t => t.ClasseId == ClasseMatricula.Id);

        if (CursoAplicavel && CursoMatricula is not null)
            query = query.Where(t => t.CursoId == CursoMatricula.Id);
        else if (!CursoAplicavel)
            query = query.Where(t => t.CursoId == null);

        foreach (var t in query.OrderBy(t => t.Letra))
            TurmasDisponiveis.Add(t);
    }

    // =================================================================
    // Notificações de UI do wizard
    // =================================================================

    partial void OnIsNovoAlunoAbertoChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsImportarAlunosAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsExportarPdfAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsExportarExcelAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));
    partial void OnIsFiltrosAvancadosAbertaChanged(bool value) => OnPropertyChanged(nameof(AlgumModalAberto));

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

    // =================================================================
    // Comandos
    // =================================================================

    [RelayCommand]
    private void AbrirDetalhes(AlunoListItemModel? aluno)
    {
        if (aluno is null) return;
        DetalhesAlunoSolicitado?.Invoke(this, aluno);
    }

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
        if (TurmaMatricula is null) return;

        try
        {
            var aluno = new Aluno
            {
                Codigo = await GerarNovoCodigoAsync(),
                Nome = NomeCompleto.Trim(),
                DataNascimento = DataNascimento?.DateTime,
                Genero = Sexo,
                Naturalidade = string.IsNullOrWhiteSpace(Naturalidade) ? null : Naturalidade.Trim(),
                Provincia = string.IsNullOrWhiteSpace(Provincia) ? null : Provincia.Trim(),
                Pais = string.IsNullOrWhiteSpace(Pais) ? null : Pais.Trim(),
                NumeroBiCedula = string.IsNullOrWhiteSpace(NumeroBiCedulaAluno) ? null : NumeroBiCedulaAluno.Trim(),
                Endereco = string.IsNullOrWhiteSpace(Morada) ? null : Morada.Trim(),
                Telefone = !string.IsNullOrWhiteSpace(ContactoPai) ? ContactoPai.Trim()
                         : !string.IsNullOrWhiteSpace(ContactoMae) ? ContactoMae.Trim()
                         : null,
                Email = null,
                Ativo = true,
                TurmaId = TurmaMatricula.Id,
                AnoLectivoId = TurmaMatricula.AnoLectivoId,
                DataMatricula = DateTime.Today,
                TemCondicaoMedica = SofreDoencaSim,
                DescricaoCondicaoMedica = SofreDoencaSim && !string.IsNullOrWhiteSpace(QualDoenca)
                    ? QualDoenca.Trim()
                    : null,
                Encarregados = new List<Encarregado>(),
                Documentos = new List<DocumentoAluno>()
            };

            if (!string.IsNullOrWhiteSpace(NomePai))
            {
                aluno.Encarregados.Add(new Encarregado
                {
                    Tipo = TipoEncarregado.Pai,
                    Nome = NomePai.Trim(),
                    Contacto = string.IsNullOrWhiteSpace(ContactoPai) ? null : ContactoPai.Trim(),
                    Profissao = string.IsNullOrWhiteSpace(ProfissaoPai) ? null : ProfissaoPai.Trim()
                });
            }

            if (!string.IsNullOrWhiteSpace(NomeMae))
            {
                aluno.Encarregados.Add(new Encarregado
                {
                    Tipo = TipoEncarregado.Mae,
                    Nome = NomeMae.Trim(),
                    Contacto = string.IsNullOrWhiteSpace(ContactoMae) ? null : ContactoMae.Trim(),
                    Profissao = string.IsNullOrWhiteSpace(ProfissaoMae) ? null : ProfissaoMae.Trim()
                });
            }

            if (BiCedulaDocumento.TemArquivo)
            {
                aluno.Documentos.Add(new DocumentoAluno
                {
                    Tipo = TipoDocumentoAluno.BiCedula,
                    NomeArquivo = BiCedulaDocumento.NomeArquivo,
                    DataUpload = DateTime.Now
                });
            }

            // Outros documentos opcionais
            AdicionarDocumentoSeExistir(aluno, CertificadoDocumento, TipoDocumentoAluno.Certificado);
            AdicionarDocumentoSeExistir(aluno, FotoDocumento, TipoDocumentoAluno.FotoTipoPasse);
            AdicionarDocumentoSeExistir(aluno, AtestadoDocumento, TipoDocumentoAluno.AtestadoMedico);

            await _alunoService.CriarAsync(aluno, aluno.Encarregados);

            LimparFormularioNovoAluno();
            await InitializeAsync();
            FecharModal();
        }
        catch
        {
            // Em produção: mostrar mensagem de erro no UI
            FecharModal();
        }
    }

    private static void AdicionarDocumentoSeExistir(Aluno aluno, DocumentoRequeridoItem item, TipoDocumentoAluno tipo)
    {
        if (!item.TemArquivo) return;
        aluno.Documentos.Add(new DocumentoAluno
        {
            Tipo = tipo,
            NomeArquivo = item.NomeArquivo,
            DataUpload = DateTime.Now
        });
    }

    /// <summary>
    /// Gera código no formato AAAA/NNNN (ex.: 2026/0003).
    /// Usa a contagem real de alunos do serviço para evitar colisões.
    /// </summary>
    private async Task<string> GerarNovoCodigoAsync()
    {
        var ano = DateTime.Now.Year;
        try
        {
            var existentes = await _alunoService.ObterListaAsync(new FiltroAlunoDto());
            var sequencial = existentes.Count + 1;
            return $"{ano}/{sequencial:0000}";
        }
        catch
        {
            return $"{ano}/{_todosAlunos.Count + 1:0000}";
        }
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
        CursoMatricula = null;
        TurmaMatricula = null;
        Turno = null;
        Periodo = null;
        SalaMatricula = string.Empty;
        TurmasDisponiveis.Clear();

        foreach (var documento in new[] { CertificadoDocumento, FotoDocumento, BiCedulaDocumento, AtestadoDocumento })
            documento.NomeArquivo = DocumentoRequeridoItem.SemFicheiroPlaceholder;
    }

    // ===== Outros modais =====
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

    [RelayCommand] private void ConfirmarImportarAlunos() => FecharModal();
    [RelayCommand] private void ConfirmarExportarPdf() => FecharModal();
    [RelayCommand] private void ConfirmarExportarExcel() => FecharModal();
    [RelayCommand] private void ConfirmarFiltrosAvancados() => FecharModal();
}

/// <summary>
/// Documento do passo 4 do wizard "Novo Aluno".
/// </summary>
public partial class DocumentoRequeridoItem : ObservableObject
{
    public const string SemFicheiroPlaceholder = "Nenhum ficheiro selecionado";

    public string Tipo { get; }
    public bool Obrigatorio { get; }

    [ObservableProperty] private string _nomeArquivo = SemFicheiroPlaceholder;

    public bool TemArquivo => NomeArquivo != SemFicheiroPlaceholder;

    public DocumentoRequeridoItem(string tipo, bool obrigatorio)
    {
        Tipo = tipo;
        Obrigatorio = obrigatorio;
    }

    partial void OnNomeArquivoChanged(string value) => OnPropertyChanged(nameof(TemArquivo));
}