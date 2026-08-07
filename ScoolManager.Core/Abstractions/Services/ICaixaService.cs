using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>
/// Serviço da aba "Caixa" (View 4 — Financeiro, ver SM_Flow.md): modais
/// "Abrir Caixa", "Fechar Caixa", "Reabrir Caixa". Só pode existir uma
/// <c>SessaoCaixa</c> aberta por vez.
/// </summary>
public interface ICaixaService
{
    /// <exception cref="InvalidOperationException">Já existe uma sessão de caixa aberta.</exception>
    Task<SessaoCaixa> AbrirCaixaAsync(int utilizadorId, decimal saldoInicial, CancellationToken ct = default);

    /// <summary>Fecha a sessão aberta, calculando SaldoFinal a partir de SaldoInicial + Entradas - Saídas.</summary>
    /// <exception cref="Exceptions.EntidadeNaoEncontradaException">Não há sessão aberta para fechar.</exception>
    Task<SessaoCaixa> FecharCaixaAsync(int utilizadorId, CancellationToken ct = default);

    /// <summary>Reabre a última sessão fechada (ex.: para corrigir um lançamento esquecido).</summary>
    Task<SessaoCaixa> ReabrirCaixaAsync(int utilizadorId, CancellationToken ct = default);

    Task<SessaoCaixa?> ObterSessaoAtualAsync(CancellationToken ct = default);
}
