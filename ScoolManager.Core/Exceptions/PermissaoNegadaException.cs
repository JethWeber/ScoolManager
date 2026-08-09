namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Lançada quando o utilizador autenticado (via <c>ISessaoAtualService</c>)
/// não tem, no seu <c>PerfilPermissao</c>, a permissão exigida pela
/// operação (ex.: tentar editar um Aluno sem <c>EditarAlunos</c>).
/// </summary>
public sealed class PermissaoNegadaException : ScoolManagerDomainException
{
    public string Permissao { get; }

    public PermissaoNegadaException(string permissao)
        : base($"Não tem permissão para: {permissao}.")
    {
        Permissao = permissao;
    }
}
