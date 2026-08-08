using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfClasseRepository : IClasseRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfClasseRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Classe>> ObterTodasAsync(CancellationToken ct = default) =>
        await _db.Classes.OrderBy(c => c.Numero).ToListAsync(ct);

    public async Task<Classe?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.Classes.FindAsync([id], ct);
}
