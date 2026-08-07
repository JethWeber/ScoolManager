namespace ScoolManager.Core.Dtos.Alunos;

/// <summary>
/// Filtros da listagem de Alunos (View 2, modal "Filtros Avançados" + campo
/// de pesquisa — ver SM_Flow.md). Confirmado pela spec; não existia ainda
/// como contrato explícito no Desktop.
/// </summary>
public class FiltroAlunoDto
{
    public string? Classe { get; set; }
    public string? Turma { get; set; }
    public string? Situacao { get; set; }
    public string? TextoBusca { get; set; }
    public bool? ApenasAtivos { get; set; }
}
