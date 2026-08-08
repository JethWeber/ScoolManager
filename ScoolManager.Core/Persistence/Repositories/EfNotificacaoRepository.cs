using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Notificacoes;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfNotificacaoRepository : INotificacaoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfNotificacaoRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Notificacao>> ObterTodasAsync(CancellationToken ct = default) =>
        await _db.Notificacoes.OrderByDescending(n => n.Timestamp).ToListAsync(ct);

    public async Task<int> ContarNaoLidasAsync(CancellationToken ct = default) =>
        await _db.Notificacoes.CountAsync(n => !n.Lida, ct);

    public async Task<Notificacao> AdicionarAsync(Notificacao notificacao, CancellationToken ct = default)
    {
        _db.Notificacoes.Add(notificacao);
        await _db.SaveChangesAsync(ct);
        return notificacao;
    }

    public async Task MarcarTodasComoLidasAsync(CancellationToken ct = default)
    {
        // Substitui o truque de Clear()+Add() que existia no Desktop
        // (NotificationsPanelViewModel.MarkAllAsRead) só para forçar refresh
        // de binding — aqui é um UPDATE em massa direto na base de dados.
        await _db.Notificacoes.Where(n => !n.Lida).ExecuteUpdateAsync(s => s.SetProperty(n => n.Lida, true), ct);
    }
}
