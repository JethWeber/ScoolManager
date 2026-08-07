using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IPerfilPermissaoRepository
{
    Task<IReadOnlyList<PerfilPermissao>> ObterTodosAsync(CancellationToken ct = default);
    Task<PerfilPermissao?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<PerfilPermissao> AdicionarAsync(PerfilPermissao perfil, CancellationToken ct = default);
    Task AtualizarAsync(PerfilPermissao perfil, CancellationToken ct = default);
}
