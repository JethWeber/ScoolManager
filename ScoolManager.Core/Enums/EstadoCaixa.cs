namespace ScoolManager.Core.Enums;

/// <summary>
/// Estado de uma <c>SessaoCaixa</c>.
///
/// Novo enum, sem equivalente direto no código do Desktop atual (o
/// <c>DashboardViewModel.FecharDia</c> é hoje um placeholder simples). A
/// necessidade de um ciclo de estado real vem da aba "Caixa" da View 4
/// (Financeiro) do SM_Flow.md, que prevê os modais "Abrir Caixa",
/// "Fechar Caixa" e "Reabrir Caixa".
///
/// Regra associada (ver <c>CaixaFechadoException</c> e <c>ICaixaService</c>):
/// só é possível registar um <c>MovimentoCaixa</c> (Entrada/Saída) enquanto
/// existir uma <c>SessaoCaixa</c> no estado <see cref="Aberta"/>.
/// </summary>
public enum EstadoCaixa
{
    Aberta,
    Fechada
}
