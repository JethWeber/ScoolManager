using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfAnoLectivoRepository : IAnoLectivoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfAnoLectivoRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<AnoLectivo>> ObterTodosAsync(CancellationToken ct = default) =>
        await _db.AnosLectivos.OrderByDescending(a => a.DataInicio).ToListAsync(ct);

    public async Task<AnoLectivo?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.AnosLectivos.FindAsync([id], ct);

    public async Task<AnoLectivo?> ObterAnoAbertoAsync(CancellationToken ct = default) =>
        await _db.AnosLectivos.FirstOrDefaultAsync(a => a.Estado == EstadoAnoLectivo.Aberto, ct);

    public async Task<AnoLectivo> AdicionarAsync(AnoLectivo anoLectivo, CancellationToken ct = default)
    {
        _db.AnosLectivos.Add(anoLectivo);
        await _db.SaveChangesAsync(ct);
        return anoLectivo;
    }

    public async Task AtualizarAsync(AnoLectivo anoLectivo, CancellationToken ct = default)
    {
        _db.AnosLectivos.Update(anoLectivo);
        await _db.SaveChangesAsync(ct);
    }
}
