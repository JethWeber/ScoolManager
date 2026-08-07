namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Classe base de todas as exceções de domínio do ScoolManager.Core.
/// Permite aos consumidores (Desktop, futura API) capturar
/// <c>catch (ScoolManagerDomainException)</c> para distinguir erros de
/// regra de negócio de erros de infraestrutura/inesperados.
/// </summary>
public abstract class ScoolManagerDomainException : Exception
{
    protected ScoolManagerDomainException(string message) : base(message) { }

    protected ScoolManagerDomainException(string message, Exception innerException)
        : base(message, innerException) { }
}
