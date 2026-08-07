using ScoolManager.Core.Entities.Notificacoes;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface INotificacaoRepository
{
    Task<IReadOnlyList<Notificacao>> ObterTodasAsync(CancellationToken ct = default);
    Task<int> ContarNaoLidasAsync(CancellationToken ct = default);
    Task<Notificacao> AdicionarAsync(Notificacao notificacao, CancellationToken ct = default);
    Task MarcarTodasComoLidasAsync(CancellationToken ct = default);
}
