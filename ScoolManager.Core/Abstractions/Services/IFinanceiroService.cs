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

    /// <summary>
    /// CORREÇÃO (gap): faltava — a aba "Pagamentos" do Financeiro (SM_Flow.md,
    /// View 4) mostra TODOS os pagamentos de um período, não só os de um
    /// aluno. Colunas da lista: Aluno, Referência, Valor, Data.
    /// </summary>
    Task<IReadOnlyList<Pagamento>> ObterPagamentosAsync(DateTime inicio, DateTime fim, CancellationToken ct = default);

    /// <exception cref="Exceptions.CaixaFechadoException">Não há sessão de caixa aberta.</exception>
    Task<Pagamento> RegistarPagamentoAsync(int alunoId, TipoCobranca tipo, decimal valor, string? metodoPagamento, CancellationToken ct = default);

    /// <summary>
    /// CORREÇÃO (gap): faltava — "Anular Pagamento" (com motivo) é uma ação
    /// real do FinanceiroViewModel, ortogonal a EstadoPagamento. Autorização
    /// por perfil/permissão fica para quando ISessaoAtualService/
    /// PerfilPermissao forem verificados aqui — não implementado ainda.
    /// </summary>
    /// <exception cref="Exceptions.EntidadeNaoEncontradaException">Pagamento não existe.</exception>
    Task AnularPagamentoAsync(int pagamentoId, string motivo, CancellationToken ct = default);

    Task<decimal> ObterSaldoDevedorAsync(int alunoId, CancellationToken ct = default);

    Task<IReadOnlyList<MovimentoCaixa>> ObterMovimentosAsync(DateTime inicio, DateTime fim, TipoMovimentoCaixa? tipo = null, CancellationToken ct = default);

    /// <summary>CORREÇÃO (gap): faltava — necessário para "Detalhes Entrada"/"Detalhes Saída".</summary>
    Task<MovimentoCaixa> ObterMovimentoPorIdAsync(int id, CancellationToken ct = default);

    /// <exception cref="Exceptions.CaixaFechadoException">Não há sessão de caixa aberta.</exception>
    Task<MovimentoCaixa> RegistarMovimentoAsync(MovimentoCaixa movimento, CancellationToken ct = default);

    /// <summary>CORREÇÃO (gap): faltava — necessário para os modais "Editar Entrada"/"Editar Saída".</summary>
    Task AtualizarMovimentoAsync(MovimentoCaixa movimento, CancellationToken ct = default);

    Task<(decimal Entradas, decimal Saidas, decimal Saldo)> ObterResumoDiarioAsync(DateTime dia, CancellationToken ct = default);
}
