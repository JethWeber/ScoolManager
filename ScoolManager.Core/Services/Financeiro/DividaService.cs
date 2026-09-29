using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Dtos.Financeiro;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Services.Financeiro;

public sealed class DividaService : IDividaService
{
    private const int MesInicioAnoLectivo = 9;
    private const int DiaLimitePagamento = 10;

    private readonly IAlunoRepository _alunos;
    private readonly IPagamentoRepository _pagamentos;
    private readonly IServicoEscolarRepository _servicos;

    public DividaService(
        IAlunoRepository alunos,
        IPagamentoRepository pagamentos,
        IServicoEscolarRepository servicos)
    {
        _alunos = alunos;
        _pagamentos = pagamentos;
        _servicos = servicos;
    }

    public async Task<ResumoDividasDto> ObterResumoAsync(
        DateTime dia,
        int limite = 5,
        CancellationToken ct = default)
    {
        limite = Math.Max(1, limite);

        var alunos = (await _alunos.ObterTodosAsync(ct))
            .Where(a => a.Ativo)
            .ToList();

        var inicioAnoLectivo = ObterInicioAnoLectivo(dia);
        var fimAnoLectivo = inicioAnoLectivo.AddYears(1).AddTicks(-1);

        var pagamentos = (await _pagamentos.ObterPorPeriodoAsync(
                inicioAnoLectivo,
                fimAnoLectivo,
                ct))
            .Where(p =>
                !p.Anulado &&
                p.Tipo == TipoCobranca.Propina &&
                p.DataPagamento is not null)
            .ToList();

        var servicosPropina = (await _servicos.ObterTodosAsync(ct))
            .Where(s => s.Categoria == CategoriaServico.Propina && s.Ativo && s.TurmaId is not null)
            .GroupBy(s => s.TurmaId!.Value)
            .ToDictionary(g => g.Key, g => g.OrderByDescending(s => s.Id).First().Preco);

        var devedores = new List<DevedorDto>();

        foreach (var aluno in alunos)
        {
            if (!servicosPropina.TryGetValue(aluno.TurmaId, out var mensalidade) || mensalidade <= 0)
                continue;

            var primeiroMes = PrimeiroMesDevido(aluno, inicioAnoLectivo, dia);
            if (primeiroMes is null)
                continue;

            var ultimoMes = ObterUltimoMesDevido(dia);
            if (ultimoMes < primeiroMes.Value)
                continue;

            var mesesDevidos = MesesEntre(primeiroMes.Value, ultimoMes).Count;
            var valorDevido = mensalidade * mesesDevidos;

            // O pagamento de propina é tratado como abatimento do total devido.
            // Isto também suporta pagamentos que liquidem vários meses de uma vez.
            var valorPago = pagamentos
                .Where(p => p.AlunoId == aluno.Id)
                .Sum(p => p.Valor);

            var divida = Math.Max(0m, valorDevido - valorPago);
            if (divida <= 0)
                continue;

            devedores.Add(new DevedorDto
            {
                AlunoId = aluno.Id,
                Nome = aluno.Nome,
                Turma = aluno.Turma?.Nome ?? string.Empty,
                ValorEmDivida = divida
            });
        }

        var ordenados = devedores
            .OrderByDescending(d => d.ValorEmDivida)
            .ThenBy(d => d.Nome)
            .ToList();

        return new ResumoDividasDto
        {
            TotalEmDivida = ordenados.Sum(d => d.ValorEmDivida),
            MaioresDevedores = ordenados.Take(limite).ToList()
        };
    }

    private static DateTime ObterInicioAnoLectivo(DateTime dia)
        => new(dia.Month >= MesInicioAnoLectivo ? dia.Year : dia.Year - 1, MesInicioAnoLectivo, 1);

    private static DateTime? PrimeiroMesDevido(
        ScoolManager.Core.Entities.Alunos.Aluno aluno,
        DateTime inicioAnoLectivo,
        DateTime dia)
    {
        var inicio = inicioAnoLectivo;

        if (aluno.DataMatricula is DateTime matricula && matricula > inicio)
            inicio = new DateTime(matricula.Year, matricula.Month, 1);

        if (inicio > dia.Date)
            return null;

        return inicio;
    }

    private static DateTime ObterUltimoMesDevido(DateTime dia)
    {
        var primeiroDoMes = new DateTime(dia.Year, dia.Month, 1);
        return dia.Day >= DiaLimitePagamento
            ? primeiroDoMes
            : primeiroDoMes.AddMonths(-1);
    }

    private static List<DateTime> MesesEntre(DateTime inicio, DateTime fim)
    {
        var meses = new List<DateTime>();
        for (var atual = new DateTime(inicio.Year, inicio.Month, 1);
             atual <= fim;
             atual = atual.AddMonths(1))
        {
            meses.Add(atual);
        }

        return meses;
    }
}
