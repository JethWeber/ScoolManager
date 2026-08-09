using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Alunos;

/// <summary>
/// Encarregado de educação de um Aluno (pai, mãe ou outro responsável).
///
/// Novo — extraído dos campos fixos <c>NomePai</c>/<c>ContactoPai</c> e
/// <c>NomeMae</c>/<c>ContactoMae</c> de <c>DetalhesAlunoViewModel</c>, que
/// hoje só admitem exatamente um "Pai" e uma "Mãe" por aluno. Aqui vira uma
/// lista (<c>Aluno.Encarregados</c>), mais flexível e alinhada com o
/// <see cref="TipoEncarregado"/>.
/// </summary>
public class Encarregado
{
    public int Id { get; set; }

    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    public TipoEncarregado Tipo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Contacto { get; set; } = string.Empty;

    /// <summary>CORREÇÃO (gap): faltava — o wizard "Novo Aluno" do Desktop (Passo 2) pede a profissão do Pai/Mãe.</summary>
    public string? Profissao { get; set; }
}
