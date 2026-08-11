using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

/// <summary>
/// Repositório EF de Cursos.
/// Usa IDbContextFactory para criar um DbContext de curta duração por operação —
/// essencial em desktop, onde o root ServiceProvider faria um Scoped viver para sempre.
/// </summary>
public class EfCursoRepository : ICursoRepository
{
    private readonly IDbContextFactory<ScoolManagerDbContext> _factory;

    public EfCursoRepository(IDbContextFactory<ScoolManagerDbContext> factory)
        => _factory = factory;

    public async Task<IReadOnlyList<Curso>> ObterTodosAsync(CancellationToken ct = default)
    {
        // AsNoTracking: a UI só precisa de leitura; evita entidades ficarem tracked.
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await db.Cursos
            .AsNoTracking()
            .OrderBy(c => c.Nome)
            .ToListAsync(ct);
    }

    public async Task<Curso?> ObterPorIdAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await db.Cursos
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<Curso> AdicionarAsync(Curso curso, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        db.Cursos.Add(curso);
        await db.SaveChangesAsync(ct);
        return curso;
    }

    public async Task AtualizarAsync(Curso curso, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);

        // Carrega a instância tracked e copia só os campos editáveis.
        // Nunca usar db.Update(entidadeDetached) — causa conflito de tracking.
        var existente = await db.Cursos.FindAsync([curso.Id], ct)
            ?? throw new InvalidOperationException($"Curso {curso.Id} não encontrado.");

        existente.Nome  = curso.Nome;
        existente.Sigla = curso.Sigla;

        await db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        var curso = await db.Cursos.FindAsync([id], ct);
        if (curso is null) return;

        db.Cursos.Remove(curso);
        await db.SaveChangesAsync(ct);
    }
}