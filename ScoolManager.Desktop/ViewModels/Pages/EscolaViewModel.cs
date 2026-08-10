using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Desktop.Models;
using CoreEnums = ScoolManager.Core.Enums;

namespace ScoolManager.Desktop.ViewModels.Pages;

/// <summary>Identifica cada aba do módulo Escola.</summary>
public enum AbaEscola
{
    Turmas,
    Salas,
    Cursos,
    AnosLectivos
}

/// <summary>Item da faixa de abas (ícone + título + o valor do enum correspondente).</summary>
public class AbaEscolaItem
{
    public required MaterialIconKind Icon { get; init; }
    public required string Titulo { get; init; }
    public required AbaEscola Valor { get; init; }
}

/// <summary>Item do ComboBox de Turno (par Valor+Label, para não vincular o enum diretamente no ComboBox).</summary>
public class TurnoOpcao
{
    public required TurnoLetivo Valor { get; init; }
    public required string Label { get; init; }
}

/// <summary>
/// ViewModel única do módulo Escola. Sustenta TODAS as abas (Turmas, Salas,
/// Cursos, Anos Lectivos) - não existe uma ViewModel por aba. Cada aba tem a
/// sua secção claramente demarcada abaixo; para dar vida a uma aba ainda
/// vazia, acrescente as propriedades/comandos dela na secção correspondente
/// (já com um comentário "TODO") e ligue-os na EscolaView.axaml, no bloco
/// dessa aba.
///
/// IMPORTANTE: a Classe (1ª à 13ª) NÃO é uma aba nem tem CRUD - é um catálogo
/// interno (<see cref="EscolaRepository.Classes"/>) usado apenas como campo
/// de seleção no modal "Nova Turma"/"Editar Turma".
///
/// Por agora só a aba "Turmas" está pronta; as restantes mostram apenas um
/// estado "em desenvolvimento" (ver <see cref="TituloAbaEmDesenvolvimento"/>).
/// </summary>
public partial class EscolaViewModel : ViewModelBase, IAsyncInitializable
{
    private readonly IEscolaService? _escolaService;
    private readonly List<TurmaModel> _turmasFonte = new();

    // =================================================================
    // Faixa de abas (comum a todas)
    // =================================================================

    public ObservableCollection<AbaEscolaItem> Abas { get; } = new()
    {
        new() { Icon = MaterialIconKind.AccountMultiple, Titulo = "Turmas",        Valor = AbaEscola.Turmas },
        new() { Icon = MaterialIconKind.Door,            Titulo = "Salas",         Valor = AbaEscola.Salas },
        new() { Icon = MaterialIconKind.BookOpenVariant, Titulo = "Cursos",        Valor = AbaEscola.Cursos },
        new() { Icon = MaterialIconKind.CalendarMonth,   Titulo = "Anos Lectivos", Valor = AbaEscola.AnosLectivos },
    };

    [ObservableProperty]
    private AbaEscolaItem? _abaItemSelecionada;

    // Flags de visibilidade por aba (usadas na View para trocar o conteúdo
    // visível sem sair desta ViewModel nem desta View).
    public bool AbaTurmasAtiva => AbaItemSelecionada?.Valor == AbaEscola.Turmas;
    public bool AbaSalasAtiva => AbaItemSelecionada?.Valor == AbaEscola.Salas;
    public bool AbaCursosAtiva => AbaItemSelecionada?.Valor == AbaEscola.Cursos;
    public bool AbaAnosLectivosAtiva => AbaItemSelecionada?.Valor == AbaEscola.AnosLectivos;

    partial void OnAbaItemSelecionadaChanged(AbaEscolaItem? value)
    {
        OnPropertyChanged(nameof(AbaTurmasAtiva));
        OnPropertyChanged(nameof(AbaSalasAtiva));
        OnPropertyChanged(nameof(AbaCursosAtiva));
        OnPropertyChanged(nameof(AbaAnosLectivosAtiva));
    }

    public EscolaViewModel() : this(null) { }

    public EscolaViewModel(IEscolaService? escolaService)
    {
        _escolaService = escolaService;
        _abaItemSelecionada = Abas[0]; // Turmas

        if (escolaService is null)
        {
            AtualizarListagemTurmas();
            AtualizarListagemSalas();
            AtualizarListagemCursos();
            AtualizarListagemAnosLectivos();
            return;
        }

        _ = InitializeAsync();
    }

    public async Task InitializeAsync()
    {
        if (_escolaService is null)
            return;

        try
        {
            var classes = await _escolaService.ObterClassesAsync();
            var cursos = await _escolaService.ObterCursosAsync();
            var salas = await _escolaService.ObterSalasAsync();
            var anosLectivos = await _escolaService.ObterAnosLectivosAsync();
            var turmas = await _escolaService.ObterTurmasAsync();

            ClassesOpcoes.Clear();
            foreach (var classe in classes.OrderBy(c => c.Numero))
                ClassesOpcoes.Add(Mapear(classe));

            CursosOpcoes.Clear();
            foreach (var curso in cursos.OrderBy(c => c.Nome))
                CursosOpcoes.Add(Mapear(curso));

            SalasOpcoes.Clear();
            foreach (var sala in salas.OrderBy(s => s.Nome))
                SalasOpcoes.Add(Mapear(sala));

            AnosLectivosOpcoes.Clear();
            foreach (var ano in anosLectivos.OrderByDescending(a => a.DataInicio))
                AnosLectivosOpcoes.Add(Mapear(ano));

            _turmasFonte.Clear();
            foreach (var turma in turmas.OrderBy(t => t.Classe?.Numero ?? 0).ThenBy(t => t.Curso?.Nome).ThenBy(t => t.Letra))
                _turmasFonte.Add(Mapear(turma));

            TurmasListadas.Clear();
            foreach (var turma in _turmasFonte)
                TurmasListadas.Add(turma);

            AtualizarListagemTurmas();
            AtualizarListagemSalas();
            AtualizarListagemCursos();
            AtualizarListagemAnosLectivos();
        }
        catch
        {
            // Mantém o estado atual em caso de falha de leitura.
        }
    }

    // =================================================================
    // Aba "Turmas" (única já funcional)
    //
    // Cada cartão representa uma Turma concreta (ex.: "10ª GRSI A"). A letra
    // é sempre atribuída automaticamente pelo TurmaNamingService - por isso
    // o modal não tem um campo de "Letra": ele mostra o nome já calculado
    // como pré-visualização, e bloqueia "Guardar" se ainda não for permitido
    // abrir a próxima turma da combinação.
    // =================================================================

    [ObservableProperty] private string _termoPesquisaTurmas = string.Empty;
    [ObservableProperty] private bool _semResultadosTurmas;

    public ObservableCollection<TurmaModel> TurmasListadas { get; } = new();

    [ObservableProperty] private string _totalAlunosTurmasTexto = "0";
    [ObservableProperty] private string _capacidadeTotalTurmasTexto = "0";
    [ObservableProperty] private string _vagasDisponiveisTurmasTexto = "0";
    [ObservableProperty] private string _vagasEmTurmasLabel = string.Empty;

    partial void OnTermoPesquisaTurmasChanged(string value) => AtualizarListagemTurmas();

    private void AtualizarListagemTurmas()
    {
        var turmas = _turmasFonte.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(TermoPesquisaTurmas))
        {
            var termo = TermoPesquisaTurmas.Trim();
            turmas = turmas.Where(t =>
                t.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                t.Sala.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                (t.Curso != null && t.Curso.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase)));
        }

        TurmasListadas.Clear();
        foreach (var turma in turmas.OrderBy(t => t.Classe.Numero).ThenBy(t => t.Curso?.Sigla).ThenBy(t => t.Letra))
            TurmasListadas.Add(turma);

        SemResultadosTurmas = TurmasListadas.Count == 0;

        var totalAlunos = _turmasFonte.Sum(t => t.Matriculados);
        var capacidadeTotal = _turmasFonte.Sum(t => t.Capacidade);

        TotalAlunosTurmasTexto = totalAlunos.ToString();
        CapacidadeTotalTurmasTexto = capacidadeTotal.ToString();
        VagasDisponiveisTurmasTexto = (capacidadeTotal - totalAlunos).ToString();

        var turmasComVaga = _turmasFonte.Count(t => !t.EstaCheia);
        VagasEmTurmasLabel = $"Em {turmasComVaga} turma(s)";
    }

    // -----------------------------------------------------------------
    // Opções para os ComboBox do modal
    // -----------------------------------------------------------------

    public ObservableCollection<AnoLectivoModel> AnosLectivosOpcoes { get; } = new();
    public ObservableCollection<ClasseModel> ClassesOpcoes { get; } = new();
    public ObservableCollection<CursoModel> CursosOpcoes { get; } = new();
    public ObservableCollection<SalaModel> SalasOpcoes { get; } = new();

    public ObservableCollection<TurnoOpcao> TurnoOpcoes { get; } = new()
    {
        new() { Valor = TurnoLetivo.Manha, Label = TurnoLetivo.Manha.ParaLabel() },
        new() { Valor = TurnoLetivo.Tarde, Label = TurnoLetivo.Tarde.ParaLabel() },
        new() { Valor = TurnoLetivo.Noite, Label = TurnoLetivo.Noite.ParaLabel() },
    };

    // -----------------------------------------------------------------
    // Modal "Nova Turma" / "Editar Turma"
    //
    // Overlay simples dentro da própria EscolaView (padrão já validado):
    // um Border a cheio ecrã, ligado a ModalTurmaVisivel, com o formulário
    // lá dentro. Em modo de edição, Ano Lectivo/Classe/Curso ficam
    // bloqueados (a letra já foi atribuída e não faz sentido "mudar de
    // identidade" uma turma existente) - só Sala, Turno e Capacidade são
    // editáveis.
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalTurmaVisivel;
    [ObservableProperty] private string _modalTurmaTitulo = "Nova Turma";
    [ObservableProperty] private string _modalTurmaErro = string.Empty;
    [ObservableProperty] private bool _modalTurmaModoEdicao;

    [ObservableProperty] private AnoLectivoModel? _formAnoLectivoSelecionado;
    [ObservableProperty] private ClasseModel? _formClasseSelecionada;
    [ObservableProperty] private CursoModel? _formCursoSelecionado;
    [ObservableProperty] private SalaModel? _formSalaSelecionada;
    [ObservableProperty] private TurnoOpcao? _formTurnoSelecionado;
    [ObservableProperty] private string _formCapacidadeTurma = string.Empty;
    [ObservableProperty] private string _formNomePreview = string.Empty;

    /// <summary>Verdadeiro quando a Classe escolhida é do Ensino Médio (única em que o Curso se aplica).</summary>
    public bool CursoAplicavel => FormClasseSelecionada?.Nivel == NivelEnsino.Medio;

    private TurmaModel? _turmaEmEdicao;

    partial void OnFormClasseSelecionadaChanged(ClasseModel? value)
    {
        if (value?.Nivel != NivelEnsino.Medio)
            FormCursoSelecionado = null;

        OnPropertyChanged(nameof(CursoAplicavel));
        AtualizarPreviewNomeTurma();
    }

    partial void OnFormCursoSelecionadoChanged(CursoModel? value) => AtualizarPreviewNomeTurma();
    partial void OnFormAnoLectivoSelecionadoChanged(AnoLectivoModel? value) => AtualizarPreviewNomeTurma();

    private void AtualizarPreviewNomeTurma()
    {
        ModalTurmaErro = string.Empty;

        if (_modalTurmaModoEdicao && _turmaEmEdicao is not null)
        {
            FormNomePreview = _turmaEmEdicao.Nome;
            return;
        }

        if (FormAnoLectivoSelecionado is null || FormClasseSelecionada is null)
        {
            FormNomePreview = string.Empty;
            return;
        }

        if (!TurmaNamingService.PodeAbrirNovaTurma(EscolaRepository.Turmas, FormAnoLectivoSelecionado, FormClasseSelecionada, FormCursoSelecionado))
        {
            FormNomePreview = string.Empty;
            ModalTurmaErro = TurmaNamingService.MotivoBloqueio(EscolaRepository.Turmas, FormAnoLectivoSelecionado, FormClasseSelecionada, FormCursoSelecionado);
            return;
        }

        var letra = TurmaNamingService.ProximaLetraDisponivel(EscolaRepository.Turmas, FormAnoLectivoSelecionado, FormClasseSelecionada, FormCursoSelecionado);
        FormNomePreview = FormCursoSelecionado is null
            ? $"{FormClasseSelecionada.Numero}ª {letra}"
            : $"{FormClasseSelecionada.Numero}ª {FormCursoSelecionado.Sigla} {letra}";
    }

    [RelayCommand]
    private void NovaTurma()
    {
        _turmaEmEdicao = null;
        ModalTurmaModoEdicao = false;
        ModalTurmaTitulo = "Nova Turma";
        ModalTurmaErro = string.Empty;
        FormAnoLectivoSelecionado = EscolaRepository.AnosLectivos.FirstOrDefault(a => a.Estado == EstadoAnoLectivo.Aberto);
        FormClasseSelecionada = null;
        FormCursoSelecionado = null;
        FormSalaSelecionada = null;
        FormTurnoSelecionado = null;
        FormCapacidadeTurma = string.Empty;
        FormNomePreview = string.Empty;
        ModalTurmaVisivel = true;
    }

    [RelayCommand]
    private void EditarTurma(TurmaModel turma)
    {
        _turmaEmEdicao = turma;
        ModalTurmaModoEdicao = true;
        ModalTurmaTitulo = "Editar Turma";
        ModalTurmaErro = string.Empty;
        FormAnoLectivoSelecionado = turma.AnoLectivo;
        FormClasseSelecionada = turma.Classe;
        FormCursoSelecionado = turma.Curso;
        FormSalaSelecionada = turma.Sala;
        FormTurnoSelecionado = TurnoOpcoes.FirstOrDefault(t => t.Valor == turma.Turno);
        FormCapacidadeTurma = turma.Capacidade.ToString();
        FormNomePreview = turma.Nome;
        ModalTurmaVisivel = true;
    }

    [RelayCommand]
    private void CancelarModalTurma()
    {
        ModalTurmaVisivel = false;
    }

    [RelayCommand]
    private void SalvarTurma()
    {
        if (FormAnoLectivoSelecionado is not { } anoLectivo)
        {
            ModalTurmaErro = "Selecione o ano lectivo.";
            return;
        }

        if (FormClasseSelecionada is not { } classe)
        {
            ModalTurmaErro = "Selecione a classe.";
            return;
        }

        if (CursoAplicavel && FormCursoSelecionado is null)
        {
            ModalTurmaErro = "Selecione o curso.";
            return;
        }

        if (FormSalaSelecionada is not { } sala)
        {
            ModalTurmaErro = "Selecione a sala.";
            return;
        }

        if (FormTurnoSelecionado is not { } turno)
        {
            ModalTurmaErro = "Selecione o turno.";
            return;
        }

        if (!int.TryParse(FormCapacidadeTurma, out var capacidade) || capacidade <= 0)
        {
            ModalTurmaErro = "Indique uma capacidade válida.";
            return;
        }

        var curso = FormCursoSelecionado;

        if (_turmaEmEdicao is null)
        {
            if (!TurmaNamingService.PodeAbrirNovaTurma(EscolaRepository.Turmas, anoLectivo, classe, curso))
            {
                ModalTurmaErro = TurmaNamingService.MotivoBloqueio(EscolaRepository.Turmas, anoLectivo, classe, curso);
                return;
            }

            var letra = TurmaNamingService.ProximaLetraDisponivel(EscolaRepository.Turmas, anoLectivo, classe, curso);

            EscolaRepository.Turmas.Add(new TurmaModel
            {
                Id = EscolaRepository.ProximoIdTurma(),
                AnoLectivo = anoLectivo,
                Classe = classe,
                Curso = curso,
                Letra = letra,
                Sala = sala,
                Turno = turno.Valor,
                Capacidade = capacidade,
                Matriculados = 0
            });
        }
        else
        {
            // Identidade (Ano Lectivo/Classe/Curso/Letra) não muda em edição - só Sala/Turno/Capacidade.
            _turmaEmEdicao.Sala = sala;
            _turmaEmEdicao.Turno = turno.Valor;
            _turmaEmEdicao.Capacidade = capacidade;
        }

        ModalTurmaVisivel = false;
        AtualizarListagemTurmas();
    }

    // -----------------------------------------------------------------
    // Modal "Eliminar Turma"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalEliminarTurmaVisivel;
    [ObservableProperty] private string _modalEliminarTurmaErro = string.Empty;
    [ObservableProperty] private string _modalEliminarTurmaNome = string.Empty;

    private TurmaModel? _turmaParaEliminar;

    [RelayCommand]
    private void EliminarTurma(TurmaModel turma)
    {
        _turmaParaEliminar = turma;
        ModalEliminarTurmaNome = turma.Nome;
        ModalEliminarTurmaErro = string.Empty;
        ModalEliminarTurmaVisivel = true;
    }

    [RelayCommand]
    private void CancelarEliminarTurma()
    {
        ModalEliminarTurmaVisivel = false;
    }

    [RelayCommand]
    private void ConfirmarEliminarTurma()
    {
        if (_turmaParaEliminar is null)
        {
            ModalEliminarTurmaVisivel = false;
            return;
        }

        if (_turmaParaEliminar.Matriculados > 0)
        {
            ModalEliminarTurmaErro = "Não é possível eliminar: existem alunos matriculados nesta turma.";
            return;
        }

        EscolaRepository.Turmas.Remove(_turmaParaEliminar);
        ModalEliminarTurmaVisivel = false;
        AtualizarListagemTurmas();
    }

    // =================================================================
    // Aba "Salas"
    // =================================================================

    [ObservableProperty] private string _termoPesquisaSalas = string.Empty;
    [ObservableProperty] private bool _semResultadosSalas;

    public ObservableCollection<SalaModel> SalasListadas { get; } = new();

    [ObservableProperty] private string _totalSalasTexto = "0";
    [ObservableProperty] private string _capacidadeTotalSalasTexto = "0";

    partial void OnTermoPesquisaSalasChanged(string value) => AtualizarListagemSalas();

    private void AtualizarListagemSalas()
    {
        SalasListadas.Clear();

        var salas = SalasOpcoes.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(TermoPesquisaSalas))
        {
            var termo = TermoPesquisaSalas.Trim();
            salas = salas.Where(s =>
                s.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                (s.Bloco != null && s.Bloco.Contains(termo, StringComparison.OrdinalIgnoreCase)));
        }

        foreach (var sala in salas.OrderBy(s => s.Nome))
            SalasListadas.Add(sala);

        SemResultadosSalas = SalasListadas.Count == 0;

        TotalSalasTexto = SalasOpcoes.Count.ToString();
        CapacidadeTotalSalasTexto = SalasOpcoes.Sum(s => s.Capacidade).ToString();
    }

    // -----------------------------------------------------------------
    // Modal "Nova Sala" / "Editar Sala"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalSalaVisivel;
    [ObservableProperty] private string _modalSalaTitulo = "Nova Sala";
    [ObservableProperty] private string _modalSalaErro = string.Empty;

    [ObservableProperty] private string _formNomeSala = string.Empty;
    [ObservableProperty] private string _formCapacidadeSala = string.Empty;
    [ObservableProperty] private string _formBlocoSala = string.Empty;
    [ObservableProperty] private string _formObservacoesSala = string.Empty;

    private SalaModel? _salaEmEdicao;

    [RelayCommand]
    private void NovaSala()
    {
        _salaEmEdicao = null;
        ModalSalaTitulo = "Nova Sala";
        ModalSalaErro = string.Empty;
        FormNomeSala = string.Empty;
        FormCapacidadeSala = string.Empty;
        FormBlocoSala = string.Empty;
        FormObservacoesSala = string.Empty;
        ModalSalaVisivel = true;
    }

    [RelayCommand]
    private void EditarSala(SalaModel sala)
    {
        _salaEmEdicao = sala;
        ModalSalaTitulo = "Editar Sala";
        ModalSalaErro = string.Empty;
        FormNomeSala = sala.Nome;
        FormCapacidadeSala = sala.Capacidade.ToString();
        FormBlocoSala = sala.Bloco ?? string.Empty;
        FormObservacoesSala = sala.Observacoes ?? string.Empty;
        ModalSalaVisivel = true;
    }

    [RelayCommand]
    private void CancelarModalSala() => ModalSalaVisivel = false;

    [RelayCommand]
    private void SalvarSala()
    {
        var nome = FormNomeSala.Trim();
        if (string.IsNullOrWhiteSpace(nome))
        {
            ModalSalaErro = "Indique o nome da sala.";
            return;
        }

        if (!int.TryParse(FormCapacidadeSala, out var capacidade) || capacidade <= 0)
        {
            ModalSalaErro = "Indique uma capacidade válida.";
            return;
        }

        var duplicada = EscolaRepository.Salas.Any(s =>
            s.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && s != _salaEmEdicao);
        if (duplicada)
        {
            ModalSalaErro = "Já existe uma sala com esse nome.";
            return;
        }

        var bloco = string.IsNullOrWhiteSpace(FormBlocoSala) ? null : FormBlocoSala.Trim();
        var observacoes = string.IsNullOrWhiteSpace(FormObservacoesSala) ? null : FormObservacoesSala.Trim();

        if (_salaEmEdicao is null)
        {
            EscolaRepository.Salas.Add(new SalaModel
            {
                Id = EscolaRepository.ProximoIdSala(),
                Nome = nome,
                Capacidade = capacidade,
                Bloco = bloco,
                Observacoes = observacoes
            });
        }
        else
        {
            _salaEmEdicao.Nome = nome;
            _salaEmEdicao.Capacidade = capacidade;
            _salaEmEdicao.Bloco = bloco;
            _salaEmEdicao.Observacoes = observacoes;
        }

        ModalSalaVisivel = false;
        AtualizarListagemSalas();
        AtualizarListagemTurmas(); // os cartões de Turma mostram o nome da Sala
    }

    // -----------------------------------------------------------------
    // Modal "Eliminar Sala"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalEliminarSalaVisivel;
    [ObservableProperty] private string _modalEliminarSalaErro = string.Empty;
    [ObservableProperty] private string _modalEliminarSalaNome = string.Empty;

    private SalaModel? _salaParaEliminar;

    [RelayCommand]
    private void EliminarSala(SalaModel sala)
    {
        _salaParaEliminar = sala;
        ModalEliminarSalaNome = sala.Nome;
        ModalEliminarSalaErro = string.Empty;
        ModalEliminarSalaVisivel = true;
    }

    [RelayCommand]
    private void CancelarEliminarSala() => ModalEliminarSalaVisivel = false;

    [RelayCommand]
    private void ConfirmarEliminarSala()
    {
        if (_salaParaEliminar is null)
        {
            ModalEliminarSalaVisivel = false;
            return;
        }

        var emUso = EscolaRepository.Turmas.Any(t => t.Sala.Id == _salaParaEliminar.Id);
        if (emUso)
        {
            ModalEliminarSalaErro = "Não é possível eliminar: existem turmas a usar esta sala.";
            return;
        }

        EscolaRepository.Salas.Remove(_salaParaEliminar);
        ModalEliminarSalaVisivel = false;
        AtualizarListagemSalas();
    }

    // =================================================================
    // Aba "Cursos"
    // =================================================================

    [ObservableProperty] private string _termoPesquisaCursos = string.Empty;
    [ObservableProperty] private bool _semResultadosCursos;

    public ObservableCollection<CursoModel> CursosListados { get; } = new();

    [ObservableProperty] private string _totalCursosTexto = "0";

    partial void OnTermoPesquisaCursosChanged(string value) => AtualizarListagemCursos();

    private void AtualizarListagemCursos()
    {
        CursosListados.Clear();

        var cursos = CursosOpcoes.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(TermoPesquisaCursos))
        {
            var termo = TermoPesquisaCursos.Trim();
            cursos = cursos.Where(c =>
                c.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                c.Sigla.Contains(termo, StringComparison.OrdinalIgnoreCase));
        }

        foreach (var curso in cursos.OrderBy(c => c.Nome))
            CursosListados.Add(curso);

        SemResultadosCursos = CursosListados.Count == 0;
        TotalCursosTexto = CursosOpcoes.Count.ToString();
    }

    // -----------------------------------------------------------------
    // Modal "Novo Curso" / "Editar Curso"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalCursoVisivel;
    [ObservableProperty] private string _modalCursoTitulo = "Novo Curso";
    [ObservableProperty] private string _modalCursoErro = string.Empty;

    [ObservableProperty] private string _formNomeCurso = string.Empty;
    [ObservableProperty] private string _formSiglaCurso = string.Empty;

    private CursoModel? _cursoEmEdicao;

    [RelayCommand]
    private void NovoCurso()
    {
        _cursoEmEdicao = null;
        ModalCursoTitulo = "Novo Curso";
        ModalCursoErro = string.Empty;
        FormNomeCurso = string.Empty;
        FormSiglaCurso = string.Empty;
        ModalCursoVisivel = true;
    }

    [RelayCommand]
    private void EditarCurso(CursoModel curso)
    {
        _cursoEmEdicao = curso;
        ModalCursoTitulo = "Editar Curso";
        ModalCursoErro = string.Empty;
        FormNomeCurso = curso.Nome;
        FormSiglaCurso = curso.Sigla;
        ModalCursoVisivel = true;
    }

    [RelayCommand]
    private void CancelarModalCurso() => ModalCursoVisivel = false;

    [RelayCommand]
    private void SalvarCurso()
    {
        var nome = FormNomeCurso.Trim();
        var sigla = FormSiglaCurso.Trim().ToUpperInvariant();

        if (string.IsNullOrWhiteSpace(nome))
        {
            ModalCursoErro = "Indique o nome do curso.";
            return;
        }

        if (string.IsNullOrWhiteSpace(sigla))
        {
            ModalCursoErro = "Indique a sigla do curso.";
            return;
        }

        var duplicado = EscolaRepository.Cursos.Any(c =>
            c.Sigla.Equals(sigla, StringComparison.OrdinalIgnoreCase) && c != _cursoEmEdicao);
        if (duplicado)
        {
            ModalCursoErro = "Já existe um curso com essa sigla.";
            return;
        }

        if (_cursoEmEdicao is null)
        {
            EscolaRepository.Cursos.Add(new CursoModel
            {
                Id = EscolaRepository.ProximoIdCurso(),
                Nome = nome,
                Sigla = sigla
            });
        }
        else
        {
            _cursoEmEdicao.Nome = nome;
            _cursoEmEdicao.Sigla = sigla;
        }

        ModalCursoVisivel = false;
        AtualizarListagemCursos();
        AtualizarListagemTurmas(); // os nomes das Turmas ("10ª GRSI A") dependem da sigla do Curso
    }

    // -----------------------------------------------------------------
    // Modal "Eliminar Curso"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalEliminarCursoVisivel;
    [ObservableProperty] private string _modalEliminarCursoErro = string.Empty;
    [ObservableProperty] private string _modalEliminarCursoNome = string.Empty;

    private CursoModel? _cursoParaEliminar;

    [RelayCommand]
    private void EliminarCurso(CursoModel curso)
    {
        _cursoParaEliminar = curso;
        ModalEliminarCursoNome = $"{curso.Nome} ({curso.Sigla})";
        ModalEliminarCursoErro = string.Empty;
        ModalEliminarCursoVisivel = true;
    }

    [RelayCommand]
    private void CancelarEliminarCurso() => ModalEliminarCursoVisivel = false;

    [RelayCommand]
    private void ConfirmarEliminarCurso()
    {
        if (_cursoParaEliminar is null)
        {
            ModalEliminarCursoVisivel = false;
            return;
        }

        var emUso = EscolaRepository.Turmas.Any(t => t.Curso?.Id == _cursoParaEliminar.Id);
        if (emUso)
        {
            ModalEliminarCursoErro = "Não é possível eliminar: existem turmas associadas a este curso.";
            return;
        }

        EscolaRepository.Cursos.Remove(_cursoParaEliminar);
        ModalEliminarCursoVisivel = false;
        AtualizarListagemCursos();
    }

    // =================================================================
    // Aba "Anos Lectivos"
    //
    // Não existe "Eliminar Ano Lectivo" (só Novo, Editar e Encerrar - ver
    // documentação do módulo). Uma vez Encerrado, o ano deixa de poder ser
    // editado (fica só histórico); "Encerrar" também só é possível enquanto
    // o ano estiver Aberto.
    // =================================================================

    [ObservableProperty] private string _termoPesquisaAnosLectivos = string.Empty;
    [ObservableProperty] private bool _semResultadosAnosLectivos;

    public ObservableCollection<AnoLectivoModel> AnosLectivosListados { get; } = new();

    partial void OnTermoPesquisaAnosLectivosChanged(string value) => AtualizarListagemAnosLectivos();

    private void AtualizarListagemAnosLectivos()
    {
        AnosLectivosListados.Clear();

        var anos = AnosLectivosOpcoes.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(TermoPesquisaAnosLectivos))
        {
            var termo = TermoPesquisaAnosLectivos.Trim();
            anos = anos.Where(a => a.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase));
        }

        foreach (var ano in anos.OrderByDescending(a => a.DataInicio))
            AnosLectivosListados.Add(ano);

        SemResultadosAnosLectivos = AnosLectivosListados.Count == 0;
    }

    // -----------------------------------------------------------------
    // Modal "Novo Ano Lectivo" / "Editar Ano Lectivo"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalAnoLectivoVisivel;
    [ObservableProperty] private string _modalAnoLectivoTitulo = "Novo Ano Lectivo";
    [ObservableProperty] private string _modalAnoLectivoErro = string.Empty;

    [ObservableProperty] private string _formNomeAnoLectivo = string.Empty;
    [ObservableProperty] private DateTime? _formDataInicioAnoLectivo;
    [ObservableProperty] private DateTime? _formDataTerminoAnoLectivo;

    private AnoLectivoModel? _anoLectivoEmEdicao;

    [RelayCommand]
    private void NovoAnoLectivo()
    {
        _anoLectivoEmEdicao = null;
        ModalAnoLectivoTitulo = "Novo Ano Lectivo";
        ModalAnoLectivoErro = string.Empty;
        FormNomeAnoLectivo = string.Empty;
        FormDataInicioAnoLectivo = null;
        FormDataTerminoAnoLectivo = null;
        ModalAnoLectivoVisivel = true;
    }

    [RelayCommand]
    private void EditarAnoLectivo(AnoLectivoModel ano)
    {
        if (ano.Estado == EstadoAnoLectivo.Encerrado)
            return; // ano encerrado é só histórico - não é editável.

        _anoLectivoEmEdicao = ano;
        ModalAnoLectivoTitulo = "Editar Ano Lectivo";
        ModalAnoLectivoErro = string.Empty;
        FormNomeAnoLectivo = ano.Nome;
        FormDataInicioAnoLectivo = ano.DataInicio;
        FormDataTerminoAnoLectivo = ano.DataTermino;
        ModalAnoLectivoVisivel = true;
    }

    [RelayCommand]
    private void CancelarModalAnoLectivo() => ModalAnoLectivoVisivel = false;

    [RelayCommand]
    private void SalvarAnoLectivo()
    {
        var nome = FormNomeAnoLectivo.Trim();
        if (string.IsNullOrWhiteSpace(nome))
        {
            ModalAnoLectivoErro = "Indique a designação do ano lectivo.";
            return;
        }

        if (FormDataInicioAnoLectivo is not { } dataInicio || FormDataTerminoAnoLectivo is not { } dataTermino)
        {
            ModalAnoLectivoErro = "Indique a data de início e a data de término.";
            return;
        }

        if (dataTermino <= dataInicio)
        {
            ModalAnoLectivoErro = "A data de término tem de ser posterior à data de início.";
            return;
        }

        var duplicado = EscolaRepository.AnosLectivos.Any(a =>
            a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && a != _anoLectivoEmEdicao);
        if (duplicado)
        {
            ModalAnoLectivoErro = "Já existe um ano lectivo com essa designação.";
            return;
        }

        if (_anoLectivoEmEdicao is null)
        {
            EscolaRepository.AnosLectivos.Add(new AnoLectivoModel
            {
                Id = EscolaRepository.ProximoIdAnoLectivo(),
                Nome = nome,
                DataInicio = dataInicio,
                DataTermino = dataTermino,
                Estado = EstadoAnoLectivo.Aberto
            });
        }
        else
        {
            _anoLectivoEmEdicao.Nome = nome;
            _anoLectivoEmEdicao.DataInicio = dataInicio;
            _anoLectivoEmEdicao.DataTermino = dataTermino;
        }

        ModalAnoLectivoVisivel = false;
        AtualizarListagemAnosLectivos();
    }

    // -----------------------------------------------------------------
    // Modal "Encerrar Ano Lectivo"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalEncerrarAnoLectivoVisivel;
    [ObservableProperty] private string _modalEncerrarAnoLectivoErro = string.Empty;
    [ObservableProperty] private string _modalEncerrarAnoLectivoNome = string.Empty;

    private AnoLectivoModel? _anoLectivoParaEncerrar;

    [RelayCommand]
    private void EncerrarAnoLectivo(AnoLectivoModel ano)
    {
        _anoLectivoParaEncerrar = ano;
        ModalEncerrarAnoLectivoNome = ano.Nome;
        ModalEncerrarAnoLectivoErro = string.Empty;
        ModalEncerrarAnoLectivoVisivel = true;
    }

    [RelayCommand]
    private void CancelarEncerrarAnoLectivo() => ModalEncerrarAnoLectivoVisivel = false;

    [RelayCommand]
    private void ConfirmarEncerrarAnoLectivo()
    {
        if (_anoLectivoParaEncerrar is null)
        {
            ModalEncerrarAnoLectivoVisivel = false;
            return;
        }

        _anoLectivoParaEncerrar.Estado = EstadoAnoLectivo.Encerrado;
        ModalEncerrarAnoLectivoVisivel = false;
        AtualizarListagemAnosLectivos();
    }
}
