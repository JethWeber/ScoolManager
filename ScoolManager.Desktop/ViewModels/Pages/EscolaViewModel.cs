using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using ScoolManager.Desktop.Models;
using ScoolManager.Desktop.Services;

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
public partial class EscolaViewModel : ViewModelBase
{
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

    /// <summary>Título mostrado no placeholder de qualquer aba ainda não implementada.</summary>
    public string TituloAbaEmDesenvolvimento => AbaItemSelecionada?.Valor switch
    {
        AbaEscola.Salas => "Salas",
        AbaEscola.Cursos => "Cursos",
        AbaEscola.AnosLectivos => "Anos Lectivos",
        _ => string.Empty
    };

    partial void OnAbaItemSelecionadaChanged(AbaEscolaItem? value)
    {
        OnPropertyChanged(nameof(AbaTurmasAtiva));
        OnPropertyChanged(nameof(AbaSalasAtiva));
        OnPropertyChanged(nameof(AbaCursosAtiva));
        OnPropertyChanged(nameof(AbaAnosLectivosAtiva));
        OnPropertyChanged(nameof(TituloAbaEmDesenvolvimento));
    }

    public EscolaViewModel()
    {
        _abaItemSelecionada = Abas[0]; // Turmas
        AtualizarListagemTurmas();
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
        TurmasListadas.Clear();

        var turmas = EscolaRepository.Turmas.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(TermoPesquisaTurmas))
        {
            var termo = TermoPesquisaTurmas.Trim();
            turmas = turmas.Where(t =>
                t.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                t.Sala.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                (t.Curso != null && t.Curso.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase)));
        }

        foreach (var turma in turmas.OrderBy(t => t.Classe.Numero).ThenBy(t => t.Curso?.Sigla).ThenBy(t => t.Letra))
            TurmasListadas.Add(turma);

        SemResultadosTurmas = TurmasListadas.Count == 0;

        var totalAlunos = EscolaRepository.Turmas.Sum(t => t.Matriculados);
        var capacidadeTotal = EscolaRepository.Turmas.Sum(t => t.Capacidade);

        TotalAlunosTurmasTexto = totalAlunos.ToString();
        CapacidadeTotalTurmasTexto = capacidadeTotal.ToString();
        VagasDisponiveisTurmasTexto = (capacidadeTotal - totalAlunos).ToString();

        var turmasComVaga = EscolaRepository.Turmas.Count(t => !t.EstaCheia);
        VagasEmTurmasLabel = $"Em {turmasComVaga} turma(s)";
    }

    // -----------------------------------------------------------------
    // Opções para os ComboBox do modal
    // -----------------------------------------------------------------

    public ObservableCollection<AnoLectivoModel> AnosLectivosOpcoes => EscolaRepository.AnosLectivos;
    public ObservableCollection<ClasseModel> ClassesOpcoes => EscolaRepository.Classes;
    public ObservableCollection<CursoModel> CursosOpcoes => EscolaRepository.Cursos;
    public ObservableCollection<SalaModel> SalasOpcoes => EscolaRepository.Salas;

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
    // Aba "Salas" (vazia por agora)
    // TODO: CRUD simples sobre EscolaRepository.Salas (Nome, Capacidade,
    // Bloco opcional, Observações opcional).
    // =================================================================

    // =================================================================
    // Aba "Cursos" (vazia por agora)
    // TODO: CRUD simples sobre EscolaRepository.Cursos (Nome, Sigla).
    // =================================================================

    // =================================================================
    // Aba "Anos Lectivos" (vazia por agora)
    // TODO: CRUD + fluxo "Encerrar Ano Lectivo" sobre
    // EscolaRepository.AnosLectivos (Designação, Data Início, Data Término,
    // Estado Aberto/Encerrado).
    // =================================================================
}
