using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface ISalaRepository
{
    Task<IReadOnlyList<Sala>> ObterTodasAsync(CancellationToken ct = default);
    Task<Sala?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<Sala> AdicionarAsync(Sala sala, CancellationToken ct = default);
    Task AtualizarAsync(Sala sala, CancellationToken ct = default);
    Task RemoverAsync(int id, CancellationToken ct = default);
}
