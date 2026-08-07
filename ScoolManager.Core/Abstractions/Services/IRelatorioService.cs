using ScoolManager.Core.Dtos.Relatorios;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>
/// Serviço da View 6 — Relatórios (ver SM_Flow.md). Um método por
/// <c>RelatorioTipo</c>, espelhando os 7 <c>Gerar*Exemplo</c> hoje escritos
/// em <c>RelatoriosViewModel</c> — a diferença é que aqui vão buscar dados
/// reais aos repositórios, em vez de gerar exemplos.
///
/// A implementação verifica <c>ILicenseGate.HasFeature("Relatorios")</c>
/// antes de qualquer método, lançando <c>FuncionalidadeNaoLicenciadaException</c>
/// se o módulo não constar da licença ativa.
/// </summary>
public interface IRelatorioService
{
    Task<IReadOnlyList<MatriculaRelatorioDto>> GerarMatriculasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default);
    Task<IReadOnlyList<AlunoRelatorioDto>> GerarListaAlunosAsync(FiltroRelatorioDto filtro, CancellationToken ct = default);
    Task<IReadOnlyList<PropinaRelatorioDto>> GerarPropinasPagasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default);
    Task<IReadOnlyList<PropinaRelatorioDto>> GerarPropinasAtrasoAsync(FiltroRelatorioDto filtro, CancellationToken ct = default);
    Task<IReadOnlyList<RelatorioMovimentoDto>> GerarEntradasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default);
    Task<IReadOnlyList<RelatorioMovimentoDto>> GerarSaidasAsync(FiltroRelatorioDto filtro, CancellationToken ct = default);
    Task<IReadOnlyList<FluxoCaixaRelatorioDto>> GerarFluxoCaixaAsync(FiltroRelatorioDto filtro, CancellationToken ct = default);
}
