namespace ScoolManager.Core.Entities.Escola;

/// <summary>
/// Curso oferecido pela instituição (ex.: "Gestão de Redes e Sistemas
/// Informáticos" / sigla "GRSI"). Só se aplica a Turmas do Ensino Médio;
/// nas Turmas de Primário/Secundário o Curso fica por preencher (null em
/// <c>Turma.CursoId</c>).
///
/// Migrado de <c>ScoolManager.Desktop.Models.CursoModel</c>.
/// </summary>
public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;
}
