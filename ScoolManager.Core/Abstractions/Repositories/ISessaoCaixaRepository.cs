using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface ISessaoCaixaRepository
{
    /// <summary>Devolve a sessão com Estado == Aberta, se existir (só pode haver uma de cada vez).</summary>
    Task<SessaoCaixa?> ObterSessaoAbertaAsync(CancellationToken ct = default);

    Task<SessaoCaixa?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<SessaoCaixa>> ObterHistoricoAsync(DateTime inicio, DateTime fim, CancellationToken ct = default);
    Task<SessaoCaixa> AdicionarAsync(SessaoCaixa sessao, CancellationToken ct = default);
    Task AtualizarAsync(SessaoCaixa sessao, CancellationToken ct = default);
}
