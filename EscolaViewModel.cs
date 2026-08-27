using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    AnosLectivos,
    Servicos
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

/// <summary>Item do ComboBox de Categoria de Serviço (mesmo motivo do TurnoOpcao acima).</summary>
public class CategoriaServicoOpcao
{
    public required CategoriaServico Valor { get; init; }
    public required string Label { get; init; }
}

/// <summary>
/// ViewModel única do módulo Escola. Sustenta TODAS as abas (Turmas, Salas,
/// Cursos, Anos Lectivos, Serviços) - não existe uma ViewModel por aba. Cada
/// aba tem a sua secção claramente demarcada abaixo; para dar vida a uma aba
/// ainda vazia, acrescente as propriedades/comandos dela na secção
/// correspondente (já com um comentário "TODO") e ligue-os na
/// EscolaView.axaml, no bloco dessa aba.
///
/// IMPORTANTE: a Classe (1ª à 13ª) NÃO é uma aba nem tem CRUD - é um catálogo
/// interno (<see cref="EscolaRepository.Classes"/>) usado apenas como campo
/// de seleção no modal "Nova Turma"/"Editar Turma".
///
/// Por agora só as abas "Turmas", "Salas", "Cursos", "Anos Lectivos" e
/// "Serviços" estão prontas.
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
        new() { Icon = MaterialIconKind.CashMultiple,    Titulo = "Serviços",      Valor = AbaEscola.Servicos },
    };

    [ObservableProperty]
    private AbaEscolaItem? _abaItemSelecionada;

    // Flags de visibilidade por aba (usadas na View para trocar o conteúdo
    // visível sem sair desta ViewModel nem desta View).
    public bool AbaTurmasAtiva => AbaItemSelecionada?.Valor == AbaEscola.Turmas;
    public bool AbaSalasAtiva => AbaItemSelecionada?.Valor == AbaEscola.Salas;
    public bool AbaCursosAtiva => AbaItemSelecionada?.Valor == AbaEscola.Cursos;
    public bool AbaAnosLectivosAtiva => AbaItemSelecionada?.Valor == AbaEscola.AnosLectivos;
    public bool AbaServicosAtiva => AbaItemSelecionada?.Valor == AbaEscola.Servicos;

    partial void OnAbaItemSelecionadaChanged(AbaEscolaItem? value)
    {
        OnPropertyChanged(nameof(AbaTurmasAtiva));
        OnPropertyChanged(nameof(AbaSalasAtiva));
        OnPropertyChanged(nameof(AbaCursosAtiva));
        OnPropertyChanged(nameof(AbaAnosLectivosAtiva));
        OnPropertyChanged(nameof(AbaServicosAtiva));
    }

    public EscolaViewModel() : this(null) { }

    public EscolaViewModel(IEscolaService? escolaService)
    {
        _escolaService = escolaService;
        _abaItemSelecionada = Abas[0]; // Turmas

        // Sem serviço, a ViewModel permanece vazia. Não existem dados Mock
        // no módulo Escola: os dados reais vêm sempre do ScoolManager.Core.
        if (escolaService is not null)
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
            var servicos = await _escolaService.ObterServicosAsync();

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

            TurmasOpcoes.Clear();
            foreach (var turma in _turmasFonte.OrderBy(t => t.Nome))
                TurmasOpcoes.Add(turma);

            _servicosFonte.Clear();
            foreach (var servico in servicos.OrderBy(s => s.Categoria).ThenBy(s => s.Nome))
                _servicosFonte.Add(Mapear(servico));

            AtualizarListagemTurmas();
            AtualizarListagemSalas();
            AtualizarListagemCursos();
            AtualizarListagemAnosLectivos();
            AtualizarListagemServicos();
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

    /// <summary>
    /// Todas as turmas da BD, sem filtro de pesquisa - usada no ComboBox
    /// "Turma" do modal de Serviço (categoria Propina). Diferente de
    /// TurmasListadas, que reflete a pesquisa da aba "Turmas".
    /// </summary>
    public ObservableCollection<TurmaModel> TurmasOpcoes { get; } = new();

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
        _ = AtualizarPreviewNomeTurmaAsync();
    }

    private async Task AtualizarPreviewNomeTurmaAsync()
    {
        ModalTurmaErro = string.Empty;

        if (ModalTurmaModoEdicao && _turmaEmEdicao is not null)
        {
            FormNomePreview = _turmaEmEdicao.Nome;
            return;
        }

        if (_escolaService is null || FormAnoLectivoSelecionado is null || FormClasseSelecionada is null)
        {
            FormNomePreview = string.Empty;
            return;
        }

        try
        {
            var podeAbrir = await _escolaService.PodeAbrirNovaTurmaAsync(
                FormAnoLectivoSelecionado.Id,
                FormClasseSelecionada.Id,
                FormCursoSelecionado?.Id);

            if (!podeAbrir)
            {
                FormNomePreview = string.Empty;
                ModalTurmaErro = "Não é possível abrir uma nova turma para esta combinação. Verifique se a turma anterior já atingiu a capacidade.";
                return;
            }

            var letra = await _escolaService.ProximaLetraDisponivelAsync(
                FormAnoLectivoSelecionado.Id,
                FormClasseSelecionada.Id,
                FormCursoSelecionado?.Id);

            FormNomePreview = FormCursoSelecionado is null
                ? $"{FormClasseSelecionada.Numero}ª {letra}"
                : $"{FormClasseSelecionada.Numero}ª {FormCursoSelecionado.Sigla} {letra}";
        }
        catch (Exception ex)
        {
            FormNomePreview = string.Empty;
            ModalTurmaErro = ex.Message;
        }
    }

    [RelayCommand]
    private void NovaTurma()
    {
        _turmaEmEdicao = null;
        ModalTurmaModoEdicao = false;
        ModalTurmaTitulo = "Nova Turma";
        ModalTurmaErro = string.Empty;
        FormAnoLectivoSelecionado = AnosLectivosOpcoes.FirstOrDefault(a => a.Estado == EstadoAnoLectivo.Aberto);
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
    private async Task SalvarTurma()
    {
        if (_escolaService is null)
        {
            ModalTurmaErro = "O serviço da Escola não está disponível.";
            return;
        }

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

        try
        {
            if (_turmaEmEdicao is null)
            {
                var turma = await _escolaService.CriarTurmaAsync(
                    anoLectivo.Id,
                    classe.Id,
                    FormCursoSelecionado?.Id,
                    sala.Id,
                    (CoreEnums.TurnoLetivo)turno.Valor,
                    capacidade);

                ModalTurmaVisivel = false;
                await InitializeAsync();
                return;
            }

            var turmaAtualizada = new Turma
            {
                Id = _turmaEmEdicao.Id,
                AnoLectivoId = _turmaEmEdicao.AnoLectivo.Id,
                ClasseId = _turmaEmEdicao.Classe.Id,
                CursoId = _turmaEmEdicao.Curso?.Id,
                Letra = _turmaEmEdicao.Letra,
                SalaId = sala.Id,
                Turno = (CoreEnums.TurnoLetivo)turno.Valor,
                Capacidade = capacidade,
                Matriculados = _turmaEmEdicao.Matriculados
            };

            await _escolaService.AtualizarTurmaAsync(turmaAtualizada);

            ModalTurmaVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalTurmaErro = ex.Message;
        }
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
    private async Task ConfirmarEliminarTurma()
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

        if (_escolaService is null)
        {
            ModalEliminarTurmaErro = "O serviço da Escola não está disponível.";
            return;
        }

        try
        {
            await _escolaService.RemoverTurmaAsync(_turmaParaEliminar.Id);
            ModalEliminarTurmaVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalEliminarTurmaErro = ex.Message;
        }
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
    private async Task SalvarSala()
    {
        if (_escolaService is null)
        {
            ModalSalaErro = "O serviço da Escola não está disponível.";
            return;
        }

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

        try
        {
            var bloco = string.IsNullOrWhiteSpace(FormBlocoSala) ? null : FormBlocoSala.Trim();
            var observacoes = string.IsNullOrWhiteSpace(FormObservacoesSala) ? null : FormObservacoesSala.Trim();

            if (_salaEmEdicao is null)
            {
                await _escolaService.CriarSalaAsync(new Sala
                {
                    Nome = nome,
                    Capacidade = capacidade,
                    Bloco = bloco,
                    Observacoes = observacoes
                });
            }
            else
            {
                await _escolaService.AtualizarSalaAsync(new Sala
                {
                    Id = _salaEmEdicao.Id,
                    Nome = nome,
                    Capacidade = capacidade,
                    Bloco = bloco,
                    Observacoes = observacoes
                });
            }

            ModalSalaVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalSalaErro = ex.Message;
        }
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
    private async Task ConfirmarEliminarSala()
    {
        if (_salaParaEliminar is null)
        {
            ModalEliminarSalaVisivel = false;
            return;
        }

        if (_turmasFonte.Any(t => t.Sala.Id == _salaParaEliminar.Id))
        {
            ModalEliminarSalaErro = "Não é possível eliminar: existem turmas a usar esta sala.";
            return;
        }

        if (_escolaService is null)
        {
            ModalEliminarSalaErro = "O serviço da Escola não está disponível.";
            return;
        }

        try
        {
            await _escolaService.RemoverSalaAsync(_salaParaEliminar.Id);
            ModalEliminarSalaVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalEliminarSalaErro = ex.Message;
        }
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
    private async Task SalvarCurso()
    {
        if (_escolaService is null)
        {
            ModalCursoErro = "O serviço da Escola não está disponível.";
            return;
        }

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

        try
        {
            if (_cursoEmEdicao is null)
            {
                await _escolaService.CriarCursoAsync(new Curso
                {
                    Nome = nome,
                    Sigla = sigla
                });
            }
            else
            {
                await _escolaService.AtualizarCursoAsync(new Curso
                {
                    Id = _cursoEmEdicao.Id,
                    Nome = nome,
                    Sigla = sigla
                });
            }

            ModalCursoVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalCursoErro = ex.Message;
        }
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
    private async Task ConfirmarEliminarCurso()
    {
        if (_cursoParaEliminar is null)
        {
            ModalEliminarCursoVisivel = false;
            return;
        }

        if (_turmasFonte.Any(t => t.Curso?.Id == _cursoParaEliminar.Id))
        {
            ModalEliminarCursoErro = "Não é possível eliminar: existem turmas associadas a este curso.";
            return;
        }

        if (_escolaService is null)
        {
            ModalEliminarCursoErro = "O serviço da Escola não está disponível.";
            return;
        }

        try
        {
            await _escolaService.RemoverCursoAsync(_cursoParaEliminar.Id);
            ModalEliminarCursoVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalEliminarCursoErro = ex.Message;
        }
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
    private async Task SalvarAnoLectivo()
    {
        if (_escolaService is null)
        {
            ModalAnoLectivoErro = "O serviço da Escola não está disponível.";
            return;
        }

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

        try
        {
            if (_anoLectivoEmEdicao is null)
            {
                await _escolaService.CriarAnoLectivoAsync(new AnoLectivo
                {
                    Nome = nome,
                    DataInicio = dataInicio,
                    DataTermino = dataTermino,
                    Estado = CoreEnums.EstadoAnoLectivo.Aberto
                });
            }
            else
            {
                await _escolaService.AtualizarAnoLectivoAsync(new AnoLectivo
                {
                    Id = _anoLectivoEmEdicao.Id,
                    Nome = nome,
                    DataInicio = dataInicio,
                    DataTermino = dataTermino,
                    Estado = (CoreEnums.EstadoAnoLectivo)_anoLectivoEmEdicao.Estado
                });
            }

            ModalAnoLectivoVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalAnoLectivoErro = ex.Message;
        }
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
    private async Task ConfirmarEncerrarAnoLectivo()
    {
        if (_anoLectivoParaEncerrar is null)
        {
            ModalEncerrarAnoLectivoVisivel = false;
            return;
        }

        if (_escolaService is null)
        {
            ModalEncerrarAnoLectivoErro = "O serviço da Escola não está disponível.";
            return;
        }

        try
        {
            await _escolaService.EncerrarAnoLectivoAsync(_anoLectivoParaEncerrar.Id);
            ModalEncerrarAnoLectivoVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalEncerrarAnoLectivoErro = ex.Message;
        }
    }

    // =================================================================
    // Aba "Serviços"
    //
    // Catálogo de tudo o que a escola cobra/vende ao aluno na secção de
    // Pagamentos (propinas, cartões, provas, uniformes, outros). Cada
    // cartão é um ServicoEscolarModel concreto (ex.: "Cartão de Estudante -
    // 1ª Via", 2000 Kz). Ao contrário de Sala/Curso, aqui NÃO existe
    // eliminação "livre" pensada como principal: o botão de ação normal é
    // Desativar/Reativar (Ativo=false/true), para não quebrar o histórico
    // de pagamentos já feitos com aquele serviço - "Eliminar" fica como
    // opção secundária, tal como hoje ainda não valida uso (ver TODO no
    // EscolaService.RemoverServicoAsync, no Core).
    // =================================================================

    [ObservableProperty] private string _termoPesquisaServicos = string.Empty;
    [ObservableProperty] private bool _semResultadosServicos;
    [ObservableProperty] private bool _mostrarServicosDesativados;

    private readonly List<ServicoEscolarModel> _servicosFonte = new();

    public ObservableCollection<ServicoEscolarModel> ServicosListados { get; } = new();

    [ObservableProperty] private string _totalServicosAtivosTexto = "0";

    public ObservableCollection<CategoriaServicoOpcao> CategoriaServicoOpcoes { get; } = new()
    {
        new() { Valor = CategoriaServico.Propina,  Label = CategoriaServico.Propina.ParaLabel() },
        new() { Valor = CategoriaServico.Cartao,   Label = CategoriaServico.Cartao.ParaLabel() },
        new() { Valor = CategoriaServico.Prova,    Label = CategoriaServico.Prova.ParaLabel() },
        new() { Valor = CategoriaServico.Uniforme, Label = CategoriaServico.Uniforme.ParaLabel() },
        new() { Valor = CategoriaServico.Outro,    Label = CategoriaServico.Outro.ParaLabel() },
    };

    partial void OnTermoPesquisaServicosChanged(string value) => AtualizarListagemServicos();
    partial void OnMostrarServicosDesativadosChanged(bool value) => AtualizarListagemServicos();

    private void AtualizarListagemServicos()
    {
        var servicos = _servicosFonte.AsEnumerable();

        if (!MostrarServicosDesativados)
            servicos = servicos.Where(s => s.Ativo);

        if (!string.IsNullOrWhiteSpace(TermoPesquisaServicos))
        {
            var termo = TermoPesquisaServicos.Trim();
            servicos = servicos.Where(s => s.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase));
        }

        ServicosListados.Clear();
        foreach (var servico in servicos.OrderBy(s => s.Categoria).ThenBy(s => s.Nome))
            ServicosListados.Add(servico);

        SemResultadosServicos = ServicosListados.Count == 0;
        TotalServicosAtivosTexto = _servicosFonte.Count(s => s.Ativo).ToString();
    }

    // -----------------------------------------------------------------
    // Modal "Novo Serviço" / "Editar Serviço"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalServicoVisivel;
    [ObservableProperty] private string _modalServicoTitulo = "Novo Serviço";
    [ObservableProperty] private string _modalServicoErro = string.Empty;

    [ObservableProperty] private string _formNomeServico = string.Empty;
    [ObservableProperty] private CategoriaServicoOpcao? _formCategoriaServicoSelecionada;
    [ObservableProperty] private string _formPrecoServico = string.Empty;
    [ObservableProperty] private string _formDescricaoServico = string.Empty;
    [ObservableProperty] private TurmaModel? _formTurmaSelecionadaServico;

    /// <summary>
    /// Verdadeiro quando a categoria escolhida é Propina - só aí o campo
    /// "Turma" aparece no modal (ver comentário em ServicoEscolar.TurmaId,
    /// no Core: a propina varia por turma, não por classe isolada).
    /// </summary>
    public bool PropinaAplicavel => FormCategoriaServicoSelecionada?.Valor == CategoriaServico.Propina;

    private ServicoEscolarModel? _servicoEmEdicao;

    partial void OnFormCategoriaServicoSelecionadaChanged(CategoriaServicoOpcao? value)
    {
        if (!PropinaAplicavel)
            FormTurmaSelecionadaServico = null;

        OnPropertyChanged(nameof(PropinaAplicavel));
    }

    [RelayCommand]
    private void NovoServico()
    {
        _servicoEmEdicao = null;
        ModalServicoTitulo = "Novo Serviço";
        ModalServicoErro = string.Empty;
        FormNomeServico = string.Empty;
        FormCategoriaServicoSelecionada = null;
        FormPrecoServico = string.Empty;
        FormDescricaoServico = string.Empty;
        FormTurmaSelecionadaServico = null;
        ModalServicoVisivel = true;
    }

    [RelayCommand]
    private void EditarServico(ServicoEscolarModel servico)
    {
        _servicoEmEdicao = servico;
        ModalServicoTitulo = "Editar Serviço";
        ModalServicoErro = string.Empty;
        FormNomeServico = servico.Nome;
        FormCategoriaServicoSelecionada = CategoriaServicoOpcoes.FirstOrDefault(c => c.Valor == servico.Categoria);
        FormPrecoServico = servico.Preco.ToString("0.##", CultureInfo.InvariantCulture);
        FormDescricaoServico = servico.Descricao ?? string.Empty;
        FormTurmaSelecionadaServico = servico.TurmaId is { } turmaId
            ? TurmasOpcoes.FirstOrDefault(t => t.Id == turmaId)
            : null;
        ModalServicoVisivel = true;
    }

    [RelayCommand]
    private void CancelarModalServico() => ModalServicoVisivel = false;

    [RelayCommand]
    private async Task SalvarServico()
    {
        if (_escolaService is null)
        {
            ModalServicoErro = "O serviço da Escola não está disponível.";
            return;
        }

        var nome = FormNomeServico.Trim();
        if (string.IsNullOrWhiteSpace(nome))
        {
            ModalServicoErro = "Indique o nome do serviço.";
            return;
        }

        if (FormCategoriaServicoSelecionada is not { } categoria)
        {
            ModalServicoErro = "Selecione a categoria.";
            return;
        }

        if (PropinaAplicavel && FormTurmaSelecionadaServico is null)
        {
            ModalServicoErro = "Selecione a turma a que esta propina se aplica.";
            return;
        }

        if (!decimal.TryParse(FormPrecoServico, NumberStyles.Number, CultureInfo.CurrentCulture, out var preco) &&
            !decimal.TryParse(FormPrecoServico, NumberStyles.Number, CultureInfo.InvariantCulture, out preco))
        {
            ModalServicoErro = "Indique um preço válido.";
            return;
        }

        if (preco <= 0)
        {
            ModalServicoErro = "O preço tem de ser maior que zero.";
            return;
        }

        try
        {
            var descricao = string.IsNullOrWhiteSpace(FormDescricaoServico) ? null : FormDescricaoServico.Trim();
            var turmaId = PropinaAplicavel ? FormTurmaSelecionadaServico!.Id : (int?)null;

            if (_servicoEmEdicao is null)
            {
                await _escolaService.CriarServicoAsync(new ServicoEscolar
                {
                    Nome = nome,
                    Categoria = (CoreEnums.CategoriaServico)categoria.Valor,
                    Preco = preco,
                    Descricao = descricao,
                    Ativo = true,
                    TurmaId = turmaId
                });
            }
            else
            {
                await _escolaService.AtualizarServicoAsync(new ServicoEscolar
                {
                    Id = _servicoEmEdicao.Id,
                    Nome = nome,
                    Categoria = (CoreEnums.CategoriaServico)categoria.Valor,
                    Preco = preco,
                    Descricao = descricao,
                    Ativo = _servicoEmEdicao.Ativo,
                    TurmaId = turmaId
                });
            }

            ModalServicoVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalServicoErro = ex.Message;
        }
    }

    // -----------------------------------------------------------------
    // Desativar / Reativar Serviço
    //
    // Preferível a eliminar sempre que o serviço já foi vendido - ver nota
    // no topo desta secção e em ServicoEscolar.Ativo (Core).
    // -----------------------------------------------------------------

    [RelayCommand]
    private async Task DesativarServico(ServicoEscolarModel servico) => await DefinirAtivoServicoAsync(servico, false);

    [RelayCommand]
    private async Task ReativarServico(ServicoEscolarModel servico) => await DefinirAtivoServicoAsync(servico, true);

    private async Task DefinirAtivoServicoAsync(ServicoEscolarModel servico, bool ativo)
    {
        if (_escolaService is null)
            return;

        try
        {
            await _escolaService.DefinirAtivoServicoAsync(servico.Id, ativo);
            await InitializeAsync();
        }
        catch
        {
            // TODO: expor este erro na UI (ex.: um Snackbar/toast global)
            // quando o módulo tiver esse mecanismo - por agora, mantém o
            // estado atual em caso de falha, tal como o resto do módulo.
        }
    }

    // -----------------------------------------------------------------
    // Modal "Eliminar Serviço"
    // -----------------------------------------------------------------

    [ObservableProperty] private bool _modalEliminarServicoVisivel;
    [ObservableProperty] private string _modalEliminarServicoErro = string.Empty;
    [ObservableProperty] private string _modalEliminarServicoNome = string.Empty;

    private ServicoEscolarModel? _servicoParaEliminar;

    [RelayCommand]
    private void EliminarServico(ServicoEscolarModel servico)
    {
        _servicoParaEliminar = servico;
        ModalEliminarServicoNome = servico.Nome;
        ModalEliminarServicoErro = string.Empty;
        ModalEliminarServicoVisivel = true;
    }

    [RelayCommand]
    private void CancelarEliminarServico() => ModalEliminarServicoVisivel = false;

    [RelayCommand]
    private async Task ConfirmarEliminarServico()
    {
        if (_servicoParaEliminar is null)
        {
            ModalEliminarServicoVisivel = false;
            return;
        }

        if (_escolaService is null)
        {
            ModalEliminarServicoErro = "O serviço da Escola não está disponível.";
            return;
        }

        try
        {
            await _escolaService.RemoverServicoAsync(_servicoParaEliminar.Id);
            ModalEliminarServicoVisivel = false;
            await InitializeAsync();
        }
        catch (Exception ex)
        {
            ModalEliminarServicoErro = ex.Message;
        }
    }

    // =================================================================
    // Mapeamento Core -> Desktop Models
    // =================================================================

    private static ClasseModel Mapear(Classe classe) => new()
    {
        Id = classe.Id,
        Numero = classe.Numero,
        Nivel = (ScoolManager.Desktop.Models.NivelEnsino)classe.Nivel
    };

    private static CursoModel Mapear(Curso curso) => new()
    {
        Id = curso.Id,
        Nome = curso.Nome,
        Sigla = curso.Sigla
    };

    private static SalaModel Mapear(Sala sala) => new()
    {
        Id = sala.Id,
        Nome = sala.Nome,
        Capacidade = sala.Capacidade,
        Bloco = sala.Bloco,
        Observacoes = sala.Observacoes
    };

    private static AnoLectivoModel Mapear(AnoLectivo ano) => new()
    {
        Id = ano.Id,
        Nome = ano.Nome,
        DataInicio = ano.DataInicio,
        DataTermino = ano.DataTermino,
        Estado = (ScoolManager.Desktop.Models.EstadoAnoLectivo)ano.Estado
    };

    private static TurmaModel Mapear(Turma turma) => new()
    {
        Id = turma.Id,
        AnoLectivo = Mapear(turma.AnoLectivo!),
        Classe = Mapear(turma.Classe!),
        Curso = turma.Curso is null ? null : Mapear(turma.Curso),
        Letra = turma.Letra,
        Sala = Mapear(turma.Sala!),
        Turno = (ScoolManager.Desktop.Models.TurnoLetivo)turma.Turno,
        Capacidade = turma.Capacidade,
        Matriculados = turma.Matriculados
    };

    private static ServicoEscolarModel Mapear(ServicoEscolar servico) => new()
    {
        Id = servico.Id,
        Nome = servico.Nome,
        Categoria = (ScoolManager.Desktop.Models.CategoriaServico)servico.Categoria,
        Preco = servico.Preco,
        Descricao = servico.Descricao,
        Ativo = servico.Ativo,
        TurmaId = servico.TurmaId,
        TurmaNome = servico.Turma?.Nome
    };

}
