using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>Serviço da aba "Dados da Escola" (View 7 — Configurações, ver SM_Flow.md). Sempre uma única linha.</summary>
public interface IConfiguracaoInstitucionalService
{
    Task<DadosInstituicao> ObterAsync(CancellationToken ct = default);
    Task AtualizarAsync(DadosInstituicao dados, CancellationToken ct = default);
}
