using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Abstractions.Services;
using ScoolManager.Core.Entities.Notificacoes;

namespace ScoolManager.Core.Services.Notificacoes;

public class NotificacaoService : INotificacaoService
{
    private readonly INotificacaoRepository _notificacoes;
    public NotificacaoService(INotificacaoRepository notificacoes) => _notificacoes = notificacoes;

    public Task<IReadOnlyList<Notificacao>> ObterTodasAsync(CancellationToken ct = default) => _notificacoes.ObterTodasAsync(ct);
    public Task<int> ContarNaoLidasAsync(CancellationToken ct = default) => _notificacoes.ContarNaoLidasAsync(ct);
    public Task MarcarTodasComoLidasAsync(CancellationToken ct = default) => _notificacoes.MarcarTodasComoLidasAsync(ct);
    public Task<Notificacao> CriarAsync(Notificacao notificacao, CancellationToken ct = default) => _notificacoes.AdicionarAsync(notificacao, ct);
}
