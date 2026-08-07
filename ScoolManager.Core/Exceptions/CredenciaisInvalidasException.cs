namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Lançada por <c>IAuthService.AutenticarAsync</c> quando o telefone e/ou a
/// password não correspondem a um utilizador válido.
///
/// Mensagem propositadamente genérica: não distingue "telefone não existe"
/// de "senha errada" — boa prática de segurança (evita que um atacante
/// descubra quais telefones estão registados por tentativa e erro).
/// </summary>
public sealed class CredenciaisInvalidasException : ScoolManagerDomainException
{
    public CredenciaisInvalidasException() : base("Telefone ou senha inválidos.") { }
}
