using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfTurmaRepository : ITurmaRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfTurmaRepository(ScoolManagerDbContext db) => _db = db;

    // Nome/OcupacaoPercentual/EstaCheia dependem de Classe/Curso já
    // carregados — sem Include, viriam null e o cálculo ficava errado.
    private IQueryable<Turma> ComNavegacoes() => _db.Turmas
        .Include(t => t.AnoLectivo)
        .Include(t => t.Classe)
        .Include(t => t.Curso)
        .Include(t => t.Sala);

    public async Task<IReadOnlyList<Turma>> ObterTodasAsync(CancellationToken ct = default) =>
        await ComNavegacoes().ToListAsync(ct);

    public async Task<Turma?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await ComNavegacoes().FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<Turma> AdicionarAsync(Turma turma, CancellationToken ct = default)
    {
        _db.Turmas.Add(turma);
        await _db.SaveChangesAsync(ct);
        return turma;
    }

    public async Task AtualizarAsync(Turma turma, CancellationToken ct = default)
    {
        _db.Turmas.Update(turma);
        await _db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        var turma = await _db.Turmas.FindAsync([id], ct);
        if (turma is null) return;
        _db.Turmas.Remove(turma);
        await _db.SaveChangesAsync(ct);
    }
}
