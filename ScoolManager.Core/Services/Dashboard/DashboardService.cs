using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Dashboard;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IAlunoRepository _alunos;
    private readonly IPagamentoRepository _pagamentos;
    private readonly IMovimentoCaixaRepository _movimentos;
    private readonly ISessaoCaixaRepository _sessoesCaixa;
    private readonly IDividaService _dividas;

    public DashboardService(
        IAlunoRepository alunos,
        IPagamentoRepository pagamentos,
        IMovimentoCaixaRepository movimentos,
        ISessaoCaixaRepository sessoesCaixa,
        IDividaService dividas)
    {
        _alunos = alunos;
        _pagamentos = pagamentos;
        _movimentos = movimentos;
        _sessoesCaixa = sessoesCaixa;
        _dividas = dividas;
    }

    public async Task<ResumoDashboardDto> ObterResumoAsync(DateTime dia, CancellationToken ct = default)
    {
        var alunos = await _alunos.ObterTodosAsync(ct);
        var totalAlunos = alunos.Count(a => a.Ativo);
        var matriculasDoAno = alunos.Count(a => a.DataMatricula?.Year == dia.Year);

        var inicioAnoLectivo = new DateTime(dia.Month >= 9 ? dia.Year : dia.Year - 1, 9, 1);
        var fimAnoLectivo = inicioAnoLectivo.AddYears(1).AddTicks(-1);

        var pagamentos = (await _pagamentos.ObterPorPeriodoAsync(inicioAnoLectivo, fimAnoLectivo, ct))
            .Where(p => !p.Anulado)
            .ToList();

        var inicioMes = new DateTime(dia.Year, dia.Month, 1);
        var fimMes = inicioMes.AddMonths(1).AddTicks(-1);

        var propinasPagas = pagamentos
            .Where(p => p.Tipo == TipoCobranca.Propina &&
                        p.Estado == EstadoPagamento.Pago &&
                        p.DataPagamento >= inicioMes &&
                        p.DataPagamento <= fimMes)
            .Sum(p => p.Valor);

        var dividas = await _dividas.ObterResumoAsync(dia, 5, ct);

        var inicioDia = dia.Date;
        var fimDia = inicioDia.AddDays(1).AddTicks(-1);
        var movimentosDoDia = await _movimentos.ObterPorPeriodoAsync(inicioDia, fimDia, null, ct);
        var entradas = movimentosDoDia.Where(m => m.Tipo == TipoMovimentoCaixa.Entrada).Sum(m => m.Valor);
        var saidas = movimentosDoDia.Where(m => m.Tipo == TipoMovimentoCaixa.Saida).Sum(m => m.Valor);

        var sessaoAtual = await _sessoesCaixa.ObterSessaoAbertaAsync(ct);
        var saldoCaixa = sessaoAtual is null ? 0m : sessaoAtual.SaldoInicial + entradas - saidas;

        var ultimosPagamentos = pagamentos
            .Where(p => p.DataPagamento is not null)
            .OrderByDescending(p => p.DataPagamento)
            .Take(5)
            .Select(p => new PagamentoResumoDto
            {
                Aluno = alunos.FirstOrDefault(a => a.Id == p.AlunoId)?.Nome ?? string.Empty,
                Valor = p.Valor,
                Data = p.DataPagamento!.Value
            })
            .ToList();

        var meses = Enumerable.Range(0, 10)
            .Select(i => inicioAnoLectivo.AddMonths(i))
            .ToList();

        var receitaPorMes = meses
            .Select(mes => new ReceitaMensalDto
            {
                Mes = mes,
                Label = NomeMes(mes.Month),
                Valor = pagamentos
                    .Where(p => p.DataPagamento?.Year == mes.Year &&
                                p.DataPagamento?.Month == mes.Month &&
                                p.Estado == EstadoPagamento.Pago)
                    .Sum(p => p.Valor)
            })
            .ToList();

        return new ResumoDashboardDto
        {
            TotalAlunos = totalAlunos,
            MatriculasDoAno = matriculasDoAno,
            PropinasPagas = propinasPagas,
            PropinasEmAtraso = dividas.TotalEmDivida,
            TotalEmDivida = dividas.TotalEmDivida,
            Entradas = entradas,
            Saidas = saidas,
            SaldoCaixa = saldoCaixa,
            UltimosPagamentos = ultimosPagamentos,
            TopDevedores = dividas.MaioresDevedores.ToList(),
            ReceitaPorMes = receitaPorMes
        };
    }

    private static string NomeMes(int mes) => mes switch
    {
        1 => "Jan", 2 => "Fev", 3 => "Mar", 4 => "Abr", 5 => "Mai", 6 => "Jun",
        7 => "Jul", 8 => "Ago", 9 => "Set", 10 => "Out", 11 => "Nov", 12 => "Dez",
        _ => string.Empty
    };
}
