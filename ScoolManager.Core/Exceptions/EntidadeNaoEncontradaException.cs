namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Lançada quando uma entidade pesquisada por Id não é encontrada.
/// Substitui os futuros <c>FirstOrDefault() is null</c> silenciosos
/// espalhados pelos Services.
/// </summary>
public sealed class EntidadeNaoEncontradaException : ScoolManagerDomainException
{
    public string Entidade { get; }
    public object Id { get; }

    public EntidadeNaoEncontradaException(string entidade, object id)
        : base($"{entidade} com id '{id}' não foi encontrado(a).")
    {
        Entidade = entidade;
        Id = id;
    }
}
