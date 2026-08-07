using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>
/// Serviço do módulo Financeiro (View 4, abas Pagamentos/Entradas/Saídas —
/// ver SM_Flow.md). A aba "Caixa" tem serviço próprio: <see cref="ICaixaService"/>.
///
/// Todas as operações de escrita aqui exigem uma <c>SessaoCaixa</c> aberta
/// (ver <see cref="ICaixaService.ObterSessaoAtualAsync"/>) — caso contrário
/// lançam <c>CaixaFechadoException</c>. A implementação também verifica
/// <c>ILicenseGate.HasFeature("Financeiro")</c> antes de qualquer operação.
/// </summary>
public interface IFinanceiroService
{
    Task<IReadOnlyList<Pagamento>> ObterHistoricoPagamentosAsync(int alunoId, CancellationToken ct = default);

    /// <exception cref="Exceptions.CaixaFechadoException">Não há sessão de caixa aberta.</exception>
    Task<Pagamento> RegistarPagamentoAsync(int alunoId, decimal valor, string? metodoPagamento, CancellationToken ct = default);

    Task<decimal> ObterSaldoDevedorAsync(int alunoId, CancellationToken ct = default);

    Task<IReadOnlyList<MovimentoCaixa>> ObterMovimentosAsync(DateTime inicio, DateTime fim, TipoMovimentoCaixa? tipo = null, CancellationToken ct = default);

    /// <exception cref="Exceptions.CaixaFechadoException">Não há sessão de caixa aberta.</exception>
    Task<MovimentoCaixa> RegistarMovimentoAsync(MovimentoCaixa movimento, CancellationToken ct = default);

    Task<(decimal Entradas, decimal Saidas, decimal Saldo)> ObterResumoDiarioAsync(DateTime dia, CancellationToken ct = default);
}
