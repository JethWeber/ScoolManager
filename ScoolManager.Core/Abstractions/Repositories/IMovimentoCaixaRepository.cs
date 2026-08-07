using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IMovimentoCaixaRepository
{
    Task<IReadOnlyList<MovimentoCaixa>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim, TipoMovimentoCaixa? tipo = null, CancellationToken ct = default);
    Task<IReadOnlyList<MovimentoCaixa>> ObterPorSessaoAsync(int sessaoCaixaId, CancellationToken ct = default);
    Task<MovimentoCaixa> AdicionarAsync(MovimentoCaixa movimento, CancellationToken ct = default);
}
