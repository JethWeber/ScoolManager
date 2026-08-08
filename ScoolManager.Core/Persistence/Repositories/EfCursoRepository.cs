using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfCursoRepository : ICursoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfCursoRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Curso>> ObterTodosAsync(CancellationToken ct = default) =>
        await _db.Cursos.OrderBy(c => c.Nome).ToListAsync(ct);

    public async Task<Curso?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.Cursos.FindAsync([id], ct);

    public async Task<Curso> AdicionarAsync(Curso curso, CancellationToken ct = default)
    {
        _db.Cursos.Add(curso);
        await _db.SaveChangesAsync(ct);
        return curso;
    }

    public async Task AtualizarAsync(Curso curso, CancellationToken ct = default)
    {
        _db.Cursos.Update(curso);
        await _db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        var curso = await _db.Cursos.FindAsync([id], ct);
        if (curso is null) return;
        _db.Cursos.Remove(curso);
        await _db.SaveChangesAsync(ct);
    }
}
