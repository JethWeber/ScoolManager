namespace ScoolManager.Core.Enums;

/// <summary>
/// Tipo de um <c>Encarregado</c> de educação de um Aluno.
///
/// Novo enum, extraído dos campos <c>NomePai</c>/<c>ContactoPai</c> e
/// <c>NomeMae</c>/<c>ContactoMae</c> hoje "hardcoded" (dois pares de campos
/// fixos) em <c>DetalhesAlunoViewModel</c>. No Core, um Aluno passa a ter
/// uma lista de <c>Encarregado</c>, cada um marcado com este tipo — o que
/// também acomoda o caso (não representado hoje na UI) de um encarregado
/// que não seja nem pai nem mãe.
/// </summary>
public enum TipoEncarregado
{
    Pai,
    Mae,
    Responsavel
}
