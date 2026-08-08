using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Enums;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Financeiro;

public class FinanceiroService : IFinanceiroService
{
    private const string Feature = "Financeiro";

    private readonly IPagamentoRepository _pagamentos;
    private readonly IMovimentoCaixaRepository _movimentos;
    private readonly ISessaoCaixaRepository _sessoesCaixa;
    private readonly ILicenseGate _licenseGate;

    public FinanceiroService(
        IPagamentoRepository pagamentos,
        IMovimentoCaixaRepository movimentos,
        ISessaoCaixaRepository sessoesCaixa,
        ILicenseGate licenseGate)
    {
        _pagamentos = pagamentos;
        _movimentos = movimentos;
        _sessoesCaixa = sessoesCaixa;
        _licenseGate = licenseGate;
    }

    private void GarantirLicenciado()
    {
        if (!_licenseGate.HasFeature(Feature))
            throw new FuncionalidadeNaoLicenciadaException(Feature);
    }

    private async Task<SessaoCaixa> GarantirCaixaAbertaAsync(CancellationToken ct)
    {
        return await _sessoesCaixa.ObterSessaoAbertaAsync(ct) ?? throw new CaixaFechadoException();
    }

    public Task<IReadOnlyList<Pagamento>> ObterHistoricoPagamentosAsync(int alunoId, CancellationToken ct = default)
    {
        GarantirLicenciado();
        return _pagamentos.ObterPorAlunoAsync(alunoId, ct);
    }

    public async Task<Pagamento> RegistarPagamentoAsync(int alunoId, decimal valor, string? metodoPagamento, CancellationToken ct = default)
    {
        GarantirLicenciado();
        var sessao = await GarantirCaixaAbertaAsync(ct);

        var pagamento = new Pagamento
        {
            AlunoId = alunoId,
            MesReferencia = DateOnly.FromDateTime(DateTime.Now),
            NumeroRecibo = await GerarProximoNumeroReciboAsync(ct),
            Valor = valor,
            DataVencimento = DateTime.Now,
            DataPagamento = DateTime.Now,
            Estado = EstadoPagamento.Pago,
            MetodoPagamento = metodoPagamento,
            SessaoCaixaId = sessao.Id
        };

        return await _pagamentos.AdicionarAsync(pagamento, ct);
    }

    private async Task<string> GerarProximoNumeroReciboAsync(CancellationToken ct)
    {
        // Determinístico (ano + sequencial), substitui o Random.Shared.Next
        // usado hoje em DetalhesAlunoViewModel.ConfirmarEfetuarPagamento —
        // inadequado para recibos com valor fiscal.
        var ano = DateTime.Now.Year;
        var doAno = await _pagamentos.ObterPorPeriodoAsync(new DateTime(ano, 1, 1), new DateTime(ano, 12, 31), ct);
        return $"REC-{ano}{doAno.Count + 1:0000}";
    }

    public async Task<decimal> ObterSaldoDevedorAsync(int alunoId, CancellationToken ct = default)
    {
        GarantirLicenciado();
        var historico = await _pagamentos.ObterPorAlunoAsync(alunoId, ct);
        return historico.Where(p => p.Estado == EstadoPagamento.EmAtraso).Sum(p => p.Valor);
    }

    public Task<IReadOnlyList<MovimentoCaixa>> ObterMovimentosAsync(DateTime inicio, DateTime fim, TipoMovimentoCaixa? tipo = null, CancellationToken ct = default)
    {
        GarantirLicenciado();
        return _movimentos.ObterPorPeriodoAsync(inicio, fim, tipo, ct);
    }

    public async Task<MovimentoCaixa> RegistarMovimentoAsync(MovimentoCaixa movimento, CancellationToken ct = default)
    {
        GarantirLicenciado();
        var sessao = await GarantirCaixaAbertaAsync(ct);

        movimento.SessaoCaixaId = sessao.Id;
        if (movimento.Data == default)
            movimento.Data = DateTime.Now;

        return await _movimentos.AdicionarAsync(movimento, ct);
    }

    public async Task<(decimal Entradas, decimal Saidas, decimal Saldo)> ObterResumoDiarioAsync(DateTime dia, CancellationToken ct = default)
    {
        GarantirLicenciado();
        var inicio = dia.Date;
        var fim = inicio.AddDays(1).AddTicks(-1);

        var movimentos = await _movimentos.ObterPorPeriodoAsync(inicio, fim, null, ct);
        var entradas = movimentos.Where(m => m.Tipo == TipoMovimentoCaixa.Entrada).Sum(m => m.Valor);
        var saidas = movimentos.Where(m => m.Tipo == TipoMovimentoCaixa.Saida).Sum(m => m.Valor);

        return (entradas, saidas, entradas - saidas);
    }
}
