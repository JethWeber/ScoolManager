namespace ScoolManager.Core.Dtos.Alunos;

/// <summary>
/// Resultado de uma importação em lote de Alunos (View 2, modal
/// "Importar Alunos" — ver SM_Flow.md).
/// </summary>
public class ImportacaoAlunosResultadoDto
{
    public int TotalLinhas { get; set; }
    public int Sucesso { get; set; }
    public List<ErroImportacaoAlunoDto> Erros { get; set; } = new();
}

/// <summary>Uma linha do ficheiro importado que falhou, com o motivo.</summary>
public class ErroImportacaoAlunoDto
{
    public int Linha { get; set; }
    public string Motivo { get; set; } = string.Empty;
}
