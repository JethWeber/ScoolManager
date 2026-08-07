using ScoolManager.Core.Entities.Escola;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IAnoLectivoRepository
{
    Task<IReadOnlyList<AnoLectivo>> ObterTodosAsync(CancellationToken ct = default);
    Task<AnoLectivo?> ObterPorIdAsync(int id, CancellationToken ct = default);

    /// <summary>Atalho usado com frequência pelo módulo Escola e por Alunos: o Ano Lectivo com Estado == Aberto.</summary>
    Task<AnoLectivo?> ObterAnoAbertoAsync(CancellationToken ct = default);

    Task<AnoLectivo> AdicionarAsync(AnoLectivo anoLectivo, CancellationToken ct = default);
    Task AtualizarAsync(AnoLectivo anoLectivo, CancellationToken ct = default);
}
