namespace ScoolManager.Core.Dtos.Financeiro;

public sealed class ResumoDividasDto
{
    public decimal TotalEmDivida { get; init; }
    public IReadOnlyList<DevedorDto> MaioresDevedores { get; init; } = Array.Empty<DevedorDto>();
}

public sealed class DevedorDto
{
    public int AlunoId { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string Turma { get; init; } = string.Empty;
    public decimal ValorEmDivida { get; init; }
}
