using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUtilizadorRepository _utilizadores;
    public AuthService(IUtilizadorRepository utilizadores) => _utilizadores = utilizadores;

    public async Task<Utilizador> AutenticarAsync(string telefone, string password, CancellationToken ct = default)
    {
        var utilizador = await _utilizadores.ObterPorTelefoneAsync(telefone, ct);

        // Mensagem genérica (CredenciaisInvalidasException) tanto para
        // "telefone não existe" como para "senha errada" ou "conta inativa"
        // — não distinguir motivos é boa prática de segurança.
        if (utilizador is null || !utilizador.Ativo || !PasswordHasher.Verify(password, utilizador.PasswordHash))
            throw new CredenciaisInvalidasException();

        utilizador.UltimoAcesso = DateTime.Now;
        await _utilizadores.AtualizarAsync(utilizador, ct);

        return utilizador;
    }
}
