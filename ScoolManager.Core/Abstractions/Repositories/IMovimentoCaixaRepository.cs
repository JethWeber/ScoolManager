using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IMovimentoCaixaRepository
{
    Task<IReadOnlyList<MovimentoCaixa>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim, TipoMovimentoCaixa? tipo = null, CancellationToken ct = default);
    Task<IReadOnlyList<MovimentoCaixa>> ObterPorSessaoAsync(int sessaoCaixaId, CancellationToken ct = default);

    /// <summary>CORREÇÃO (gap): faltava — necessário para "Detalhes Entrada"/"Detalhes Saída" e para AtualizarAsync poder ir buscar o registo primeiro.</summary>
    Task<MovimentoCaixa?> ObterPorIdAsync(int id, CancellationToken ct = default);

    Task<MovimentoCaixa> AdicionarAsync(MovimentoCaixa movimento, CancellationToken ct = default);

    /// <summary>CORREÇÃO (gap): faltava — necessário para os modais "Editar Entrada"/"Editar Saída" (SM_Flow.md, View 4).</summary>
    Task AtualizarAsync(MovimentoCaixa movimento, CancellationToken ct = default);
}
