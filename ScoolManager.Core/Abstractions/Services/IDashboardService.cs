using ScoolManager.Core.Dtos.Dashboard;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>
/// Serviço da View 1 — Dashboard (ver SM_Flow.md). Agrega
/// <c>IAlunoRepository</c> + <c>IFinanceiroService</c> num único
/// <see cref="ResumoDashboardDto"/> — sem dados fixos como hoje
/// (<c>DashboardViewModel.CarregarKpis</c> tem valores "78", "1.200.000" hardcoded).
/// </summary>
public interface IDashboardService
{
    Task<ResumoDashboardDto> ObterResumoAsync(DateTime dia, CancellationToken ct = default);
}
