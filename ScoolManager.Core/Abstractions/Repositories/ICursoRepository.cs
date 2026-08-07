using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface ICursoRepository
{
    Task<IReadOnlyList<Curso>> ObterTodosAsync(CancellationToken ct = default);
    Task<Curso?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<Curso> AdicionarAsync(Curso curso, CancellationToken ct = default);
    Task AtualizarAsync(Curso curso, CancellationToken ct = default);
    Task RemoverAsync(int id, CancellationToken ct = default);
}
