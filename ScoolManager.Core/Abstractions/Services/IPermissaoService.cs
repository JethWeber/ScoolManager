using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>Serviço da aba "Permissões" (View 7 — Configurações, ver SM_Flow.md).</summary>
public interface IPermissaoService
{
    Task<IReadOnlyList<PerfilPermissao>> ObterTodosAsync(CancellationToken ct = default);
    Task<PerfilPermissao> CriarAsync(PerfilPermissao perfil, CancellationToken ct = default);

    /// <exception cref="InvalidOperationException">O perfil está marcado como Bloqueado (ex.: Administrador).</exception>
    Task AtualizarAsync(PerfilPermissao perfil, CancellationToken ct = default);
}
