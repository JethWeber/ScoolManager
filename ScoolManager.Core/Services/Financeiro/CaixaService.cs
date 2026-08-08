using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Enums;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Financeiro;

public class CaixaService : ICaixaService
{
    private readonly ISessaoCaixaRepository _sessoesCaixa;
    private readonly IMovimentoCaixaRepository _movimentos;
    private readonly IPagamentoRepository _pagamentos;

    public CaixaService(ISessaoCaixaRepository sessoesCaixa, IMovimentoCaixaRepository movimentos, IPagamentoRepository pagamentos)
    {
        _sessoesCaixa = sessoesCaixa;
        _movimentos = movimentos;
        _pagamentos = pagamentos;
    }

    public async Task<SessaoCaixa> AbrirCaixaAsync(int utilizadorId, decimal saldoInicial, CancellationToken ct = default)
    {
        if (await _sessoesCaixa.ObterSessaoAbertaAsync(ct) is not null)
            throw new InvalidOperationException("Já existe uma sessão de caixa aberta — feche-a antes de abrir uma nova.");

        var sessao = new SessaoCaixa
        {
            DataAbertura = DateTime.Now,
            SaldoInicial = saldoInicial,
            Estado = EstadoCaixa.Aberta,
            UtilizadorAberturaId = utilizadorId
        };

        return await _sessoesCaixa.AdicionarAsync(sessao, ct);
    }

    public async Task<SessaoCaixa> FecharCaixaAsync(int utilizadorId, CancellationToken ct = default)
    {
        var sessao = await _sessoesCaixa.ObterSessaoAbertaAsync(ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(SessaoCaixa), "sessão aberta");

        var movimentosDaSessao = await _movimentos.ObterPorSessaoAsync(sessao.Id, ct);
        var entradas = movimentosDaSessao.Where(m => m.Tipo == TipoMovimentoCaixa.Entrada).Sum(m => m.Valor);
        var saidas = movimentosDaSessao.Where(m => m.Tipo == TipoMovimentoCaixa.Saida).Sum(m => m.Valor);

        var pagamentosDaSessao = (await _pagamentos.ObterPorPeriodoAsync(sessao.DataAbertura, DateTime.Now, ct))
            .Where(p => p.SessaoCaixaId == sessao.Id)
            .Sum(p => p.Valor);

        sessao.SaldoFinal = sessao.SaldoInicial + entradas + pagamentosDaSessao - saidas;
        sessao.DataFechamento = DateTime.Now;
        sessao.Estado = EstadoCaixa.Fechada;
        sessao.UtilizadorFechamentoId = utilizadorId;

        await _sessoesCaixa.AtualizarAsync(sessao, ct);
        return sessao;
    }

    public async Task<SessaoCaixa> ReabrirCaixaAsync(int utilizadorId, CancellationToken ct = default)
    {
        if (await _sessoesCaixa.ObterSessaoAbertaAsync(ct) is not null)
            throw new InvalidOperationException("Já existe uma sessão de caixa aberta.");

        var ultimaFechada = (await _sessoesCaixa.ObterHistoricoAsync(DateTime.MinValue, DateTime.Now, ct))
            .Where(s => s.Estado == EstadoCaixa.Fechada)
            .OrderByDescending(s => s.DataFechamento)
            .FirstOrDefault()
            ?? throw new EntidadeNaoEncontradaException(nameof(SessaoCaixa), "sessão fechada");

        ultimaFechada.Estado = EstadoCaixa.Aberta;
        ultimaFechada.DataFechamento = null;
        ultimaFechada.SaldoFinal = null;
        ultimaFechada.UtilizadorFechamentoId = null;

        await _sessoesCaixa.AtualizarAsync(ultimaFechada, ct);
        return ultimaFechada;
    }

    public Task<SessaoCaixa?> ObterSessaoAtualAsync(CancellationToken ct = default) => _sessoesCaixa.ObterSessaoAbertaAsync(ct);
}
