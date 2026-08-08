using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Abstractions.Repositories;

/// <summary>
/// CORREÇÃO ao roteiro original (Fase 5): esta interface faltava — é
/// necessária para <c>IConfiguracaoInstitucionalService</c>, que ficou sem
/// repositório definido. <c>DadosInstituicao</c> é singleton (uma só linha,
/// seed com Id = 1 — ver DadosInstituicaoConfiguration), por isso não há
/// <c>ObterTodosAsync</c> nem <c>AdicionarAsync</c>: só leitura e
/// atualização da linha única.
/// </summary>
public interface IDadosInstituicaoRepository
{
    Task<DadosInstituicao> ObterAsync(CancellationToken ct = default);
    Task AtualizarAsync(DadosInstituicao dados, CancellationToken ct = default);
}
