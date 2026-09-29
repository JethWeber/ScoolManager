using ScoolManager.Core.Dtos.Financeiro;

namespace ScoolManager.Core.Abstractions.Services;

public interface IDividaService
{
    Task<ResumoDividasDto> ObterResumoAsync(DateTime dia, int limite = 5, CancellationToken ct = default);
}
