namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Lançada quando as 26 letras (A-Z) disponíveis para turmas de uma mesma
/// combinação Ano Lectivo+Classe+Curso já estão todas em uso. Substitui o
/// <c>InvalidOperationException</c> genérico que
/// <c>TurmaNamingService.ProximaLetraDisponivel</c> lançava no Desktop.
/// </summary>
public sealed class LimiteDeLetrasAtingidoException : ScoolManagerDomainException
{
    public LimiteDeLetrasAtingidoException(int numeroClasse, int limite)
        : base($"Não é possível criar mais turmas para a {numeroClasse}ª classe: " +
               $"limite de {limite} turmas (A-Z) atingido.")
    {
    }
}
