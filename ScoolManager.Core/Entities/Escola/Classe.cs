using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Escola;

/// <summary>
/// Uma Classe representa o ano/grau de escolaridade (1ª à 13ª). É um
/// catálogo interno fornecido pelo sistema (não é cadastrado pelo
/// utilizador) — existe apenas para ser escolhido ao criar uma Turma.
///
/// Migrado de <c>ScoolManager.Desktop.Models.ClasseModel</c>. O repositório
/// correspondente (<c>IClasseRepository</c>) é propositadamente só-leitura:
/// não há CRUD para Classe, espelhando o comportamento atual.
/// </summary>
public class Classe
{
    public int Id { get; set; }

    /// <summary>Número da classe (1, 2, ..., 13).</summary>
    public int Numero { get; set; }

    public NivelEnsino Nivel { get; set; }
}
