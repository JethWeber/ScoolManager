using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfMovimentoCaixaRepository : IMovimentoCaixaRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfMovimentoCaixaRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<MovimentoCaixa>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim, TipoMovimentoCaixa? tipo = null, CancellationToken ct = default)
    {
        var query = _db.MovimentosCaixa.Where(m => m.Data >= inicio && m.Data <= fim);
        if (tipo is not null)
            query = query.Where(m => m.Tipo == tipo);

        return await query.OrderBy(m => m.Data).ToListAsync(ct);
    }

    public async Task<IReadOnlyList<MovimentoCaixa>> ObterPorSessaoAsync(int sessaoCaixaId, CancellationToken ct = default) =>
        await _db.MovimentosCaixa.Where(m => m.SessaoCaixaId == sessaoCaixaId).ToListAsync(ct);

    public async Task<MovimentoCaixa> AdicionarAsync(MovimentoCaixa movimento, CancellationToken ct = default)
    {
        _db.MovimentosCaixa.Add(movimento);
        await _db.SaveChangesAsync(ct);
        return movimento;
    }
}
