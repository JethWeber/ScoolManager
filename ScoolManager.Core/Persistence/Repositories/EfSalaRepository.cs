using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfSalaRepository : ISalaRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfSalaRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Sala>> ObterTodasAsync(CancellationToken ct = default) =>
        await _db.Salas.OrderBy(s => s.Nome).ToListAsync(ct);

    public async Task<Sala?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.Salas.FindAsync([id], ct);

    public async Task<Sala> AdicionarAsync(Sala sala, CancellationToken ct = default)
    {
        _db.Salas.Add(sala);
        await _db.SaveChangesAsync(ct);
        return sala;
    }

    public async Task AtualizarAsync(Sala sala, CancellationToken ct = default)
    {
        _db.Salas.Update(sala);
        await _db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        var sala = await _db.Salas.FindAsync([id], ct);
        if (sala is null) return;
        _db.Salas.Remove(sala);
        await _db.SaveChangesAsync(ct);
    }
}
