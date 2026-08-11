using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

/// <summary>
/// Repositório EF de Turmas.
/// As navegações (AnoLectivo, Classe, Curso, Sala) são necessárias para
/// propriedades calculadas como Nome / EstaCheia — por isso o Include.
/// AsNoTracking nas leituras evita o conflito de tracking na edição.
/// </summary>
public class EfTurmaRepository : ITurmaRepository
{
    private readonly IDbContextFactory<ScoolManagerDbContext> _factory;

    public EfTurmaRepository(IDbContextFactory<ScoolManagerDbContext> factory)
        => _factory = factory;

    /// <summary>
    /// Query base com Include das navegações + AsNoTracking.
    /// </summary>
    private static IQueryable<Turma> ComNavegacoes(ScoolManagerDbContext db) =>
        db.Turmas
            .AsNoTracking()
            .Include(t => t.AnoLectivo)
            .Include(t => t.Classe)
            .Include(t => t.Curso)
            .Include(t => t.Sala);

    public async Task<IReadOnlyList<Turma>> ObterTodasAsync(CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await ComNavegacoes(db).ToListAsync(ct);
    }

    public async Task<Turma?> ObterPorIdAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await ComNavegacoes(db)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<Turma> AdicionarAsync(Turma turma, CancellationToken ct = default)
    {
        // A entidade chega só com FKs (sem navegações preenchidas) —
        // é o padrão correcto para Create.
        await using var db = await _factory.CreateDbContextAsync(ct);
        db.Turmas.Add(turma);
        await db.SaveChangesAsync(ct);
        return turma;
    }

    public async Task AtualizarAsync(Turma turma, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);

        var existente = await db.Turmas.FindAsync([turma.Id], ct)
            ?? throw new InvalidOperationException($"Turma {turma.Id} não encontrada.");

        // Só os campos que o modal de edição permite alterar.
        // AnoLectivo / Classe / Curso / Letra / Matriculados não são editáveis.
        existente.SalaId     = turma.SalaId;
        existente.Turno      = turma.Turno;
        existente.Capacidade = turma.Capacidade;

        await db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        var turma = await db.Turmas.FindAsync([id], ct);
        if (turma is null) return;

        db.Turmas.Remove(turma);
        await db.SaveChangesAsync(ct);
    }
}