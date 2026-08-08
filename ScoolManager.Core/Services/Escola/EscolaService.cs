using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Escola;

public class EscolaService : IEscolaService
{
    private readonly IClasseRepository _classes;
    private readonly ICursoRepository _cursos;
    private readonly ISalaRepository _salas;
    private readonly IAnoLectivoRepository _anosLectivos;
    private readonly ITurmaRepository _turmas;

    public EscolaService(
        IClasseRepository classes,
        ICursoRepository cursos,
        ISalaRepository salas,
        IAnoLectivoRepository anosLectivos,
        ITurmaRepository turmas)
    {
        _classes = classes;
        _cursos = cursos;
        _salas = salas;
        _anosLectivos = anosLectivos;
        _turmas = turmas;
    }

    public Task<IReadOnlyList<Classe>> ObterClassesAsync(CancellationToken ct = default) => _classes.ObterTodasAsync(ct);

    public Task<IReadOnlyList<Curso>> ObterCursosAsync(CancellationToken ct = default) => _cursos.ObterTodosAsync(ct);
    public Task<Curso> CriarCursoAsync(Curso curso, CancellationToken ct = default) => _cursos.AdicionarAsync(curso, ct);
    public Task AtualizarCursoAsync(Curso curso, CancellationToken ct = default) => _cursos.AtualizarAsync(curso, ct);
    public Task RemoverCursoAsync(int id, CancellationToken ct = default) => _cursos.RemoverAsync(id, ct);

    public Task<IReadOnlyList<Sala>> ObterSalasAsync(CancellationToken ct = default) => _salas.ObterTodasAsync(ct);
    public Task<Sala> CriarSalaAsync(Sala sala, CancellationToken ct = default) => _salas.AdicionarAsync(sala, ct);
    public Task AtualizarSalaAsync(Sala sala, CancellationToken ct = default) => _salas.AtualizarAsync(sala, ct);
    public Task RemoverSalaAsync(int id, CancellationToken ct = default) => _salas.RemoverAsync(id, ct);

    public Task<IReadOnlyList<AnoLectivo>> ObterAnosLectivosAsync(CancellationToken ct = default) => _anosLectivos.ObterTodosAsync(ct);
    public Task<AnoLectivo> CriarAnoLectivoAsync(AnoLectivo anoLectivo, CancellationToken ct = default) => _anosLectivos.AdicionarAsync(anoLectivo, ct);

    public async Task EncerrarAnoLectivoAsync(int id, CancellationToken ct = default)
    {
        var ano = await _anosLectivos.ObterPorIdAsync(id, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(AnoLectivo), id);

        ano.Estado = EstadoAnoLectivo.Encerrado;
        await _anosLectivos.AtualizarAsync(ano, ct);
    }

    public Task<IReadOnlyList<Turma>> ObterTurmasAsync(CancellationToken ct = default) => _turmas.ObterTodasAsync(ct);

    public async Task<Turma> CriarTurmaAsync(int anoLectivoId, int classeId, int? cursoId, int salaId, TurnoLetivo turno, int capacidade, CancellationToken ct = default)
    {
        var (anoLectivo, classe, curso) = await ResolverCombinacaoAsync(anoLectivoId, classeId, cursoId, ct);

        var sala = await _salas.ObterPorIdAsync(salaId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Sala), salaId);

        var turmasExistentes = await _turmas.ObterTodasAsync(ct);

        // Valida a regra de abertura (a letra "A" pode sempre ser criada;
        // "B", "C", ... só quando a anterior estiver cheia) e só depois
        // calcula qual é a próxima letra livre.
        TurmaNamingService.ValidarAberturaOuLancar(turmasExistentes, anoLectivo, classe, curso);
        var letra = TurmaNamingService.ProximaLetraDisponivel(turmasExistentes, anoLectivo, classe, curso);

        var turma = new Turma
        {
            AnoLectivoId = anoLectivoId,
            ClasseId = classeId,
            CursoId = cursoId,
            Letra = letra,
            SalaId = sala.Id,
            Turno = turno,
            Capacidade = capacidade,
            Matriculados = 0
        };

        return await _turmas.AdicionarAsync(turma, ct);
    }

    public Task AtualizarTurmaAsync(Turma turma, CancellationToken ct = default) => _turmas.AtualizarAsync(turma, ct);
    public Task RemoverTurmaAsync(int id, CancellationToken ct = default) => _turmas.RemoverAsync(id, ct);

    public async Task<char> ProximaLetraDisponivelAsync(int anoLectivoId, int classeId, int? cursoId, CancellationToken ct = default)
    {
        var (anoLectivo, classe, curso) = await ResolverCombinacaoAsync(anoLectivoId, classeId, cursoId, ct);
        var turmas = await _turmas.ObterTodasAsync(ct);
        return TurmaNamingService.ProximaLetraDisponivel(turmas, anoLectivo, classe, curso);
    }

    public async Task<bool> PodeAbrirNovaTurmaAsync(int anoLectivoId, int classeId, int? cursoId, CancellationToken ct = default)
    {
        var (anoLectivo, classe, curso) = await ResolverCombinacaoAsync(anoLectivoId, classeId, cursoId, ct);
        var turmas = await _turmas.ObterTodasAsync(ct);
        return TurmaNamingService.PodeAbrirNovaTurma(turmas, anoLectivo, classe, curso);
    }

    private async Task<(AnoLectivo AnoLectivo, Classe Classe, Curso? Curso)> ResolverCombinacaoAsync(int anoLectivoId, int classeId, int? cursoId, CancellationToken ct)
    {
        var anoLectivo = await _anosLectivos.ObterPorIdAsync(anoLectivoId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(AnoLectivo), anoLectivoId);

        var classe = await _classes.ObterPorIdAsync(classeId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Classe), classeId);

        Curso? curso = null;
        if (cursoId is not null)
        {
            curso = await _cursos.ObterPorIdAsync(cursoId.Value, ct)
                ?? throw new EntidadeNaoEncontradaException(nameof(Curso), cursoId.Value);
        }

        return (anoLectivo, classe, curso);
    }
}
