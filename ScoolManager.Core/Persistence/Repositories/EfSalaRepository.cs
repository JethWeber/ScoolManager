using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Persistence.Repositories;

/// <summary>
/// Repositório EF de Salas.
/// Mesmo padrão: factory + AsNoTracking nas leituras + update por Find.
/// </summary>
public class EfSalaRepository : ISalaRepository
{
    private readonly IDbContextFactory<ScoolManagerDbContext> _factory;

    public EfSalaRepository(IDbContextFactory<ScoolManagerDbContext> factory)
        => _factory = factory;

    public async Task<IReadOnlyList<Sala>> ObterTodasAsync(CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await db.Salas
            .AsNoTracking()
            .OrderBy(s => s.Nome)
            .ToListAsync(ct);
    }

    public async Task<Sala?> ObterPorIdAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        return await db.Salas
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<Sala> AdicionarAsync(Sala sala, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        db.Salas.Add(sala);
        await db.SaveChangesAsync(ct);
        return sala;
    }

    public async Task AtualizarAsync(Sala sala, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);

        var existente = await db.Salas.FindAsync([sala.Id], ct)
            ?? throw new InvalidOperationException($"Sala {sala.Id} não encontrada.");

        existente.Nome         = sala.Nome;
        existente.Capacidade   = sala.Capacidade;
        existente.Bloco        = sala.Bloco;
        existente.Observacoes  = sala.Observacoes;

        await db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _factory.CreateDbContextAsync(ct);
        var sala = await db.Salas.FindAsync([id], ct);
        if (sala is null) return;

        db.Salas.Remove(sala);
        await db.SaveChangesAsync(ct);
    }
}