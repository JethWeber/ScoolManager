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

    public DashboardService(
        IAlunoRepository alunos,
        IPagamentoRepository pagamentos,
        IMovimentoCaixaRepository movimentos,
        ISessaoCaixaRepository sessoesCaixa)
    {
        _alunos = alunos;
        _pagamentos = pagamentos;
        _movimentos = movimentos;
        _sessoesCaixa = sessoesCaixa;
    }

    public async Task<ResumoDashboardDto> ObterResumoAsync(DateTime dia, CancellationToken ct = default)
    {
        var alunos = await _alunos.ObterTodosAsync(ct);
        var totalAlunos = alunos.Count(a => a.Ativo);
        var matriculasDoAno = alunos.Count(a => a.DataMatricula?.Year == dia.Year);

        var inicioAno = new DateTime(dia.Year, 1, 1);
        var fimAno = new DateTime(dia.Year, 12, 31);
        var pagamentosDoAno = await _pagamentos.ObterPorPeriodoAsync(inicioAno, fimAno, ct);
        var propinasPagas = pagamentosDoAno.Where(p => p.Estado == EstadoPagamento.Pago).Sum(p => p.Valor);
        var propinasEmAtraso = pagamentosDoAno.Where(p => p.Estado == EstadoPagamento.EmAtraso).Sum(p => p.Valor);

        var inicioDia = dia.Date;
        var fimDia = inicioDia.AddDays(1).AddTicks(-1);
        var movimentosDoDia = await _movimentos.ObterPorPeriodoAsync(inicioDia, fimDia, null, ct);
        var entradas = movimentosDoDia.Where(m => m.Tipo == TipoMovimentoCaixa.Entrada).Sum(m => m.Valor);
        var saidas = movimentosDoDia.Where(m => m.Tipo == TipoMovimentoCaixa.Saida).Sum(m => m.Valor);

        var sessaoAtual = await _sessoesCaixa.ObterSessaoAbertaAsync(ct);
        var saldoCaixa = sessaoAtual is null ? 0m : sessaoAtual.SaldoInicial + entradas - saidas;

        var ultimosPagamentos = pagamentosDoAno
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

        // CORREÇÃO (gap 1): Top 5 Devedores — soma o valor em atraso por
        // aluno e ordena do maior para o menor. Usa os pagamentos em atraso
        // já filtrados do ano corrente (pagamentosDoAno); um devedor de anos
        // anteriores que já não tenha registos "em atraso" no ano atual não
        // aparece aqui — é uma limitação aceitável para o Dashboard (visão
        // do ano corrente), não para os Relatórios (que cobrem qualquer período).
        var topDevedores = pagamentosDoAno
            .Where(p => p.Estado == EstadoPagamento.EmAtraso)
            .GroupBy(p => p.AlunoId)
            .Select(g => new
            {
                AlunoId = g.Key,
                Total = g.Sum(p => p.Valor)
            })
            .OrderByDescending(g => g.Total)
            .Take(5)
            .Select(g =>
            {
                var aluno = alunos.FirstOrDefault(a => a.Id == g.AlunoId);
                return new DevedorDto
                {
                    Nome = aluno?.Nome ?? string.Empty,
                    Turma = aluno?.Turma?.Nome ?? string.Empty,
                    ValorEmDivida = g.Total
                };
            })
            .ToList();

        return new ResumoDashboardDto
        {
            TotalAlunos = totalAlunos,
            MatriculasDoAno = matriculasDoAno,
            PropinasPagas = propinasPagas,
            PropinasEmAtraso = propinasEmAtraso,
            Entradas = entradas,
            Saidas = saidas,
            SaldoCaixa = saldoCaixa,
            UltimosPagamentos = ultimosPagamentos,
            TopDevedores = topDevedores
        };
    }
}
