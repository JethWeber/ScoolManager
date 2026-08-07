namespace ScoolManager.Core.Exceptions;

/// <summary>
/// Lançada quando se tenta registar um <c>MovimentoCaixa</c> (Entrada/Saída)
/// ou um <c>Pagamento</c> sem existir uma <c>SessaoCaixa</c> no estado
/// <c>Aberta</c>. Corresponde à regra da aba "Caixa" (View 4 — Financeiro,
/// ver SM_Flow.md): é preciso "Abrir Caixa" antes de operar.
/// </summary>
public sealed class CaixaFechadoException : ScoolManagerDomainException
{
    public CaixaFechadoException()
        : base("Não é possível registar movimentos ou pagamentos: não existe uma sessão de caixa aberta.")
    {
    }
}
