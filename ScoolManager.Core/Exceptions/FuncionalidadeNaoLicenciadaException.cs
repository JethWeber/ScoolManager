namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Lançada quando uma operação é bloqueada porque o módulo correspondente
/// não consta da licença ativa (ver <c>ILicenseGate</c> e
/// <c>WeberTech_Licensing_Documentacao_V01.pdf</c>, tabela de módulos por
/// produto — para o School Manager: Alunos, Propinas, Financeiro,
/// Relatórios).
/// </summary>
public sealed class FuncionalidadeNaoLicenciadaException : ScoolManagerDomainException
{
    public string Funcionalidade { get; }

    public FuncionalidadeNaoLicenciadaException(string funcionalidade)
        : base($"A funcionalidade '{funcionalidade}' não está incluída na licença ativa.")
    {
        Funcionalidade = funcionalidade;
    }
}
