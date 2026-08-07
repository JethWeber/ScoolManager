using ScoolManager.Core.Entities.Notificacoes;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>Serviço do painel "sino" do Dashboard. Substitui o LoadMockData() de NotificationsPanelViewModel.</summary>
public interface INotificacaoService
{
    Task<IReadOnlyList<Notificacao>> ObterTodasAsync(CancellationToken ct = default);
    Task<int> ContarNaoLidasAsync(CancellationToken ct = default);
    Task MarcarTodasComoLidasAsync(CancellationToken ct = default);
    Task<Notificacao> CriarAsync(Notificacao notificacao, CancellationToken ct = default);
}
