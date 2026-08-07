using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IPagamentoRepository
{
    Task<IReadOnlyList<Pagamento>> ObterPorAlunoAsync(int alunoId, CancellationToken ct = default);
    Task<IReadOnlyList<Pagamento>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim, CancellationToken ct = default);
    Task<Pagamento?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<Pagamento> AdicionarAsync(Pagamento pagamento, CancellationToken ct = default);
    Task AtualizarAsync(Pagamento pagamento, CancellationToken ct = default);
}
