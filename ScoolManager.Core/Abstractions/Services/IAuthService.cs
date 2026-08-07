using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>
/// Serviço de autenticação, substituindo o <c>await Task.Delay(600)</c>
/// simulado de <c>LoginViewModel.LoginAsync</c> por verificação real contra
/// <c>Utilizador.PasswordHash</c>.
/// </summary>
public interface IAuthService
{
    /// <exception cref="Exceptions.CredenciaisInvalidasException">Telefone ou senha não correspondem.</exception>
    Task<Utilizador> AutenticarAsync(string telefone, string password, CancellationToken ct = default);
}
