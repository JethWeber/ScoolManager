using ScoolManager.Core.Dtos.Financeiro;

namespace ScoolManager.Core.Dtos.Dashboard;

public class ResumoDashboardDto
{
    public int TotalAlunos { get; set; }
    public int MatriculasDoAno { get; set; }
    public decimal PropinasPagas { get; set; }
    public decimal PropinasEmAtraso { get; set; }
    public decimal Entradas { get; set; }
    public decimal Saidas { get; set; }
    public decimal SaldoCaixa { get; set; }
    public decimal TotalEmDivida { get; set; }
    public List<PagamentoResumoDto> UltimosPagamentos { get; set; } = new();
    public List<DevedorDto> TopDevedores { get; set; } = new();
    public List<ReceitaMensalDto> ReceitaPorMes { get; set; } = new();
}

public class PagamentoResumoDto
{
    public string Aluno { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
}

public class ReceitaMensalDto
{
    public DateTime Mes { get; set; }
    public string Label { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
