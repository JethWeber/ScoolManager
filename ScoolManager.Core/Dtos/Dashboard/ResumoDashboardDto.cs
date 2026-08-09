namespace ScoolManager.Core.Dtos.Dashboard;

/// <summary>
/// Resumo agregado do Dashboard (View 1, ver SM_Flow.md): "Total de alunos,
/// Matrículas do ano, Propinas pagas, Propinas em atraso, Entradas, Saídas,
/// Saldo de caixa, Últimos pagamentos".
///
/// Substitui o rascunho anterior baseado só em <c>KpiCardModel</c>/
/// <c>DevedorModel</c> — esses continuam 100% na UI (ícones, texto de
/// tendência), este DTO só devolve os números crus que a spec pede.
/// </summary>
public class ResumoDashboardDto
{
    public int TotalAlunos { get; set; }
    public int MatriculasDoAno { get; set; }
    public decimal PropinasPagas { get; set; }
    public decimal PropinasEmAtraso { get; set; }
    public decimal Entradas { get; set; }
    public decimal Saidas { get; set; }
    public decimal SaldoCaixa { get; set; }
    public List<PagamentoResumoDto> UltimosPagamentos { get; set; } = new();

    /// <summary>
    /// CORREÇÃO (gap identificado por TODO em DashboardViewModel.InitializeAsync):
    /// os 5 alunos com maior saldo em dívida, para a lista "Top 5 Devedores"
    /// do Dashboard. Rank/Iniciais continuam a ser calculados na UI a partir
    /// desta lista (posição + Nome), como já acontecia com DevedorModel.
    /// </summary>
    public List<DevedorDto> TopDevedores { get; set; } = new();
}

/// <summary>Linha resumida de um pagamento recente, para a lista "Últimos pagamentos" do Dashboard.</summary>
public class PagamentoResumoDto
{
    public string Aluno { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
}

/// <summary>Um aluno com saldo em dívida, para a lista "Top 5 Devedores" do Dashboard.</summary>
public class DevedorDto
{
    public string Nome { get; set; } = string.Empty;
    public string Turma { get; set; } = string.Empty;
    public decimal ValorEmDivida { get; set; }
}
