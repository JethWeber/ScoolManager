using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IUtilizadorRepository
{
    Task<IReadOnlyList<Utilizador>> ObterTodosAsync(CancellationToken ct = default);
    Task<Utilizador?> ObterPorIdAsync(int id, CancellationToken ct = default);

    /// <summary>Usado por IAuthService.AutenticarAsync — Telefone é o identificador de login.</summary>
    Task<Utilizador?> ObterPorTelefoneAsync(string telefone, CancellationToken ct = default);

    Task<Utilizador> AdicionarAsync(Utilizador utilizador, CancellationToken ct = default);
    Task AtualizarAsync(Utilizador utilizador, CancellationToken ct = default);
}
