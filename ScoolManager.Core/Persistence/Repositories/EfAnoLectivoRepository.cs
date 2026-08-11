using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Persistence.Repositories;

/// <summary>
/// Repositório EF de Anos Lectivos.
/// </summary>
public class EfAnoLectivoRepository : IAnoLectivoRepository
{
    private readonly IDbContextFactory<ScoolManagerDbContext> _factory;

    public EfAnoLectivoRepository(IDbContextFactory<ScoolManagerDbContext> factory)
        => _factory = factory;

    public async Task<IReadOnlyList<AnoLectivo>> ObterTodosAsync(CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await db.AnosLectivos
            .AsNoTracking()
            .OrderByDescending(a => a.DataInicio)
            .ToListAsync(ct);
    }

    public async Task<AnoLectivo?> ObterPorIdAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await db.AnosLectivos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<AnoLectivo?> ObterAnoAbertoAsync(CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await db.AnosLectivos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Estado == EstadoAnoLectivo.Aberto, ct);
    }

    public async Task<AnoLectivo> AdicionarAsync(AnoLectivo anoLectivo, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        db.AnosLectivos.Add(anoLectivo);
        await db.SaveChangesAsync(ct);
        return anoLectivo;
    }

    public async Task AtualizarAsync(AnoLectivo anoLectivo, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);

        var existente = await db.AnosLectivos.FindAsync([anoLectivo.Id], ct)
            ?? throw new InvalidOperationException($"AnoLectivo {anoLectivo.Id} não encontrado.");

        existente.Nome        = anoLectivo.Nome;
        existente.DataInicio  = anoLectivo.DataInicio;
        existente.DataTermino = anoLectivo.DataTermino;
        existente.Estado      = anoLectivo.Estado;

        await db.SaveChangesAsync(ct);
    }
}