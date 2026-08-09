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
    private readonly IAutorizacaoService _autorizacao;

    public FinanceiroService(
        IPagamentoRepository pagamentos,
        IMovimentoCaixaRepository movimentos,
        ISessaoCaixaRepository sessoesCaixa,
        ILicenseGate licenseGate,
        IAutorizacaoService autorizacao)
    {
        _pagamentos = pagamentos;
        _movimentos = movimentos;
        _sessoesCaixa = sessoesCaixa;
        _licenseGate = licenseGate;
        _autorizacao = autorizacao;
    }

    /// <summary>Substitui GarantirLicenciado() em todas as chamadas — licença E permissão são exigidas juntas para qualquer operação do módulo Financeiro.</summary>
    private void GarantirAcesso()
    {
        if (!_licenseGate.HasFeature(Feature))
            throw new FuncionalidadeNaoLicenciadaException(Feature);

        _autorizacao.GarantirPermissao(p => p.Financeiro, "Financeiro");
    }

    private async Task<SessaoCaixa> GarantirCaixaAbertaAsync(CancellationToken ct)
    {
        return await _sessoesCaixa.ObterSessaoAbertaAsync(ct) ?? throw new CaixaFechadoException();
    }

    public Task<IReadOnlyList<Pagamento>> ObterHistoricoPagamentosAsync(int alunoId, CancellationToken ct = default)
    {
        GarantirAcesso();
        return _pagamentos.ObterPorAlunoAsync(alunoId, ct);
    }

    public Task<IReadOnlyList<Pagamento>> ObterPagamentosAsync(DateTime inicio, DateTime fim, CancellationToken ct = default)
    {
        GarantirAcesso();
        return _pagamentos.ObterPorPeriodoAsync(inicio, fim, ct);
    }

    public async Task<Pagamento> RegistarPagamentoAsync(int alunoId, TipoCobranca tipo, decimal valor, string? metodoPagamento, CancellationToken ct = default)
    {
        GarantirAcesso();
        var sessao = await GarantirCaixaAbertaAsync(ct);

        var pagamento = new Pagamento
        {
            AlunoId = alunoId,
            MesReferencia = DateOnly.FromDateTime(DateTime.Now),
            Tipo = tipo,
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

    public async Task AnularPagamentoAsync(int pagamentoId, string motivo, CancellationToken ct = default)
    {
        GarantirAcesso();

        var pagamento = await _pagamentos.ObterPorIdAsync(pagamentoId, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(Pagamento), pagamentoId);

        pagamento.Anulado = true;
        pagamento.MotivoAnulacao = motivo;
        await _pagamentos.AtualizarAsync(pagamento, ct);
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
        GarantirAcesso();
        var historico = await _pagamentos.ObterPorAlunoAsync(alunoId, ct);
        return historico.Where(p => p.Estado == EstadoPagamento.EmAtraso && !p.Anulado).Sum(p => p.Valor);
    }

    public Task<IReadOnlyList<MovimentoCaixa>> ObterMovimentosAsync(DateTime inicio, DateTime fim, TipoMovimentoCaixa? tipo = null, CancellationToken ct = default)
    {
        GarantirAcesso();
        return _movimentos.ObterPorPeriodoAsync(inicio, fim, tipo, ct);
    }

    public async Task<MovimentoCaixa> ObterMovimentoPorIdAsync(int id, CancellationToken ct = default)
    {
        GarantirAcesso();
        return await _movimentos.ObterPorIdAsync(id, ct)
            ?? throw new EntidadeNaoEncontradaException(nameof(MovimentoCaixa), id);
    }

    public Task AtualizarMovimentoAsync(MovimentoCaixa movimento, CancellationToken ct = default)
    {
        GarantirAcesso();
        // Não reabre a validação de "caixa aberta" aqui de propósito: editar
        // um lançamento de uma sessão já FECHADA continua a ser uma correção
        // legítima (ex.: descrição errada), desde que não se altere o Valor/
        // Tipo de forma a desequilibrar um fecho de caixa já conferido — essa
        // validação mais fina fica para quando houver um caso de uso real
        // que a exija (não especificado ainda no SM_Flow.md).
        return _movimentos.AtualizarAsync(movimento, ct);
    }

    public async Task<MovimentoCaixa> RegistarMovimentoAsync(MovimentoCaixa movimento, CancellationToken ct = default)
    {
        GarantirAcesso();
        var sessao = await GarantirCaixaAbertaAsync(ct);

        movimento.SessaoCaixaId = sessao.Id;
        if (movimento.Data == default)
            movimento.Data = DateTime.Now;

        return await _movimentos.AdicionarAsync(movimento, ct);
    }

    public async Task<(decimal Entradas, decimal Saidas, decimal Saldo)> ObterResumoDiarioAsync(DateTime dia, CancellationToken ct = default)
    {
        GarantirAcesso();
        var inicio = dia.Date;
        var fim = inicio.AddDays(1).AddTicks(-1);

        var movimentos = await _movimentos.ObterPorPeriodoAsync(inicio, fim, null, ct);
        var entradas = movimentos.Where(m => m.Tipo == TipoMovimentoCaixa.Entrada).Sum(m => m.Valor);
        var saidas = movimentos.Where(m => m.Tipo == TipoMovimentoCaixa.Saida).Sum(m => m.Valor);

        return (entradas, saidas, entradas - saidas);
    }
}
