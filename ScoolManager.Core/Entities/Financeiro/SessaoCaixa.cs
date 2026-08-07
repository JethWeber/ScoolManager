using ScoolManager.Core.Entities.Identidade;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Financeiro;

/// <summary>
/// Uma sessão/turno de caixa: abre com um saldo inicial, acumula
/// <see cref="MovimentoCaixa"/> (entradas/saídas) e <see cref="Pagamento"/>
/// enquanto estiver <see cref="EstadoCaixa.Aberta"/>, e fecha com um saldo
/// final. Corresponde à aba "Caixa" da View 4 (Financeiro) do SM_Flow.md,
/// que prevê os modais "Abrir Caixa", "Fechar Caixa" e "Reabrir Caixa".
///
/// Entidade nova — sem equivalente direto no código do Desktop atual (o
/// <c>DashboardViewModel.FecharDia</c> é hoje só um placeholder). Regra
/// associada: <c>FinanceiroService</c> só regista Entradas/Saídas/
/// Pagamentos enquanto existir uma <see cref="SessaoCaixa"/> Aberta —
/// caso contrário, lança <c>CaixaFechadoException</c>.
/// </summary>
public class SessaoCaixa
{
    public int Id { get; set; }

    public DateTime DataAbertura { get; set; }
    public DateTime? DataFechamento { get; set; }

    public decimal SaldoInicial { get; set; }
    public decimal? SaldoFinal { get; set; }

    public EstadoCaixa Estado { get; set; }

    public int UtilizadorAberturaId { get; set; }
    public Utilizador? UtilizadorAbertura { get; set; }

    public int? UtilizadorFechamentoId { get; set; }
    public Utilizador? UtilizadorFechamento { get; set; }

    public List<MovimentoCaixa> Movimentos { get; set; } = new();
    public List<Pagamento> Pagamentos { get; set; } = new();
}
