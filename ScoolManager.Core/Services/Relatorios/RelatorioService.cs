using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Relatorios;
using ScoolManager.Core.Enums;
using ScoolManager.Core.Exceptions;

namespace ScoolManager.Core.Services.Relatorios;

public class RelatorioService : IRelatorioService
{
    private const string Feature = "Relatorios";

    private readonly IAlunoRepository _alunos;
    private readonly IPagamentoRepository _pagamentos;
    private readonly IMovimentoCaixaRepository _movimentos;
    private readonly ILicenseGate _licenseGate;
    private readonly IAutorizacaoService _autorizacao;

    public RelatorioService(
        IAlunoRepository alunos,
        IPagamentoRepository pagamentos,
        IMovimentoCaixaRepository movimentos,
        ILicenseGate licenseGate,
        IAutorizacaoService autorizacao)
    {
        _alunos = alunos;
        _pagamentos = pagamentos;
        _movimentos = movimentos;
        _licenseGate = licenseGate;
        _autorizacao = autorizacao;
    }

    private void GarantirAcesso()
    {
        if (!_licenseGate.HasFeature(Feature))
            throw new FuncionalidadeNaoLicenciadaException(Feature);

        _autorizacao.GarantirPermissao(p => p.Relatorios, "Relatorios");
    }

    public async Task<IReadOnlyList<MatriculaRelatorioDto>> GerarMatriculasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default)
    {
        GarantirAcesso();
        var inicio = filtro.DataInicio ?? DateTime.Today.AddMonths(-1);
        var fim = filtro.DataFim ?? DateTime.Today;

        var alunos = (await _alunos.ObterTodosAsync(ct))
            .Where(a => a.DataMatricula is not null && a.DataMatricula >= inicio && a.DataMatricula <= fim);

        return alunos.Select(a => new MatriculaRelatorioDto
        {
            Aluno = a.Nome,
            NumeroMatricula = a.Codigo,
            Turma = a.Turma?.Nome ?? string.Empty,
            Classe = a.Turma?.Classe is not null ? $"{a.Turma.Classe.Numero}ª" : string.Empty,
            DataMatricula = a.DataMatricula!.Value,
            Estado = a.Ativo ? "Ativo" : "Inativo"
        }).ToList();
    }

    public async Task<IReadOnlyList<AlunoRelatorioDto>> GerarListaAlunosAsync(FiltroRelatorioDto filtro, CancellationToken ct = default)
    {
        GarantirAcesso();
        var alunos = await _alunos.ObterTodosAsync(ct);

        return alunos.Select(a => new AlunoRelatorioDto
        {
            Nome = a.Nome,
            NumeroMatricula = a.Codigo,
            Classe = a.Turma?.Classe is not null ? $"{a.Turma.Classe.Numero}ª" : string.Empty,
            Turma = a.Turma?.Nome ?? string.Empty,
            Situacao = a.Ativo ? "Ativo" : "Inativo",
            Contacto = a.Telefone
        }).ToList();
    }

    public Task<IReadOnlyList<PropinaRelatorioDto>> GerarPropinasPagasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default) =>
        GerarPropinasAsync(filtro, EstadoPagamento.Pago, ct);

    public Task<IReadOnlyList<PropinaRelatorioDto>> GerarPropinasAtrasoAsync(FiltroRelatorioDto filtro, CancellationToken ct = default) =>
        GerarPropinasAsync(filtro, EstadoPagamento.EmAtraso, ct);

    private async Task<IReadOnlyList<PropinaRelatorioDto>> GerarPropinasAsync(FiltroRelatorioDto filtro, EstadoPagamento estado, CancellationToken ct)
    {
        GarantirAcesso();
        var inicio = filtro.DataInicio ?? DateTime.Today.AddMonths(-1);
        var fim = filtro.DataFim ?? DateTime.Today;

        var pagamentos = (await _pagamentos.ObterPorPeriodoAsync(inicio, fim, ct))
            .Where(p => p.Estado == estado && !p.Anulado);
        var alunosPorId = (await _alunos.ObterTodosAsync(ct)).ToDictionary(a => a.Id);

        return pagamentos.Select(p => new PropinaRelatorioDto
        {
            Aluno = alunosPorId.TryGetValue(p.AlunoId, out var aluno) ? aluno.Nome : string.Empty,
            Referencia = p.NumeroRecibo,
            Valor = p.Valor,
            DataVencimento = p.DataVencimento,
            DataPagamento = p.DataPagamento,
            Estado = estado == EstadoPagamento.Pago ? "Pago" : "Em Atraso"
        }).ToList();
    }

    public Task<IReadOnlyList<RelatorioMovimentoDto>> GerarEntradasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default) =>
        GerarMovimentosAsync(filtro, TipoMovimentoCaixa.Entrada, ct);

    public Task<IReadOnlyList<RelatorioMovimentoDto>> GerarSaidasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default) =>
        GerarMovimentosAsync(filtro, TipoMovimentoCaixa.Saida, ct);

    private async Task<IReadOnlyList<RelatorioMovimentoDto>> GerarMovimentosAsync(FiltroRelatorioDto filtro, TipoMovimentoCaixa tipo, CancellationToken ct)
    {
        GarantirAcesso();
        var inicio = filtro.DataInicio ?? DateTime.Today.AddMonths(-1);
        var fim = filtro.DataFim ?? DateTime.Today;

        var movimentos = await _movimentos.ObterPorPeriodoAsync(inicio, fim, tipo, ct);

        return movimentos.Select(m => new RelatorioMovimentoDto
        {
            Descricao = m.Descricao,
            Categoria = m.Categoria,
            Valor = m.Valor,
            Data = m.Data,
            Tipo = tipo == TipoMovimentoCaixa.Entrada ? "Entrada" : "Saida"
        }).ToList();
    }

    public async Task<IReadOnlyList<FluxoCaixaRelatorioDto>> GerarFluxoCaixaAsync(FiltroRelatorioDto filtro, CancellationToken ct = default)
    {
        GarantirAcesso();
        var inicio = filtro.DataInicio ?? DateTime.Today.AddMonths(-6);
        var fim = filtro.DataFim ?? DateTime.Today;

        var movimentos = await _movimentos.ObterPorPeriodoAsync(inicio, fim, null, ct);
        var resultado = new List<FluxoCaixaRelatorioDto>();
        var saldo = 0m;

        foreach (var grupo in movimentos.GroupBy(m => new DateTime(m.Data.Year, m.Data.Month, 1)).OrderBy(g => g.Key))
        {
            var saldoInicial = saldo;
            var entradas = grupo.Where(m => m.Tipo == TipoMovimentoCaixa.Entrada).Sum(m => m.Valor);
            var saidas = grupo.Where(m => m.Tipo == TipoMovimentoCaixa.Saida).Sum(m => m.Valor);
            saldo += entradas - saidas;

            resultado.Add(new FluxoCaixaRelatorioDto
            {
                Periodo = grupo.Key.ToString("MMMM yyyy"),
                SaldoInicial = saldoInicial,
                TotalEntradas = entradas,
                TotalSaidas = saidas,
                SaldoFinal = saldo
            });
        }

        return resultado;
    }
}
