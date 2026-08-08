using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfPagamentoRepository : IPagamentoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfPagamentoRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<Pagamento>> ObterPorAlunoAsync(int alunoId, CancellationToken ct = default) =>
        await _db.Pagamentos.Where(p => p.AlunoId == alunoId).OrderByDescending(p => p.DataVencimento).ToListAsync(ct);

    public async Task<IReadOnlyList<Pagamento>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim, CancellationToken ct = default) =>
        await _db.Pagamentos.Where(p => p.DataVencimento >= inicio && p.DataVencimento <= fim).ToListAsync(ct);

    public async Task<Pagamento?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.Pagamentos.FindAsync([id], ct);

    public async Task<Pagamento> AdicionarAsync(Pagamento pagamento, CancellationToken ct = default)
    {
        _db.Pagamentos.Add(pagamento);
        await _db.SaveChangesAsync(ct);
        return pagamento;
    }

    public async Task AtualizarAsync(Pagamento pagamento, CancellationToken ct = default)
    {
        _db.Pagamentos.Update(pagamento);
        await _db.SaveChangesAsync(ct);
    }
}
