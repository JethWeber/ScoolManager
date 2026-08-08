using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Financeiro;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfSessaoCaixaRepository : ISessaoCaixaRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfSessaoCaixaRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<SessaoCaixa?> ObterSessaoAbertaAsync(CancellationToken ct = default) =>
        await _db.SessoesCaixa.FirstOrDefaultAsync(s => s.Estado == EstadoCaixa.Aberta, ct);

    public async Task<SessaoCaixa?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.SessoesCaixa
            .Include(s => s.Movimentos)
            .Include(s => s.Pagamentos)
            .FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<IReadOnlyList<SessaoCaixa>> ObterHistoricoAsync(DateTime inicio, DateTime fim, CancellationToken ct = default) =>
        await _db.SessoesCaixa
            .Where(s => s.DataAbertura >= inicio && s.DataAbertura <= fim)
            .OrderByDescending(s => s.DataAbertura)
            .ToListAsync(ct);

    public async Task<SessaoCaixa> AdicionarAsync(SessaoCaixa sessao, CancellationToken ct = default)
    {
        _db.SessoesCaixa.Add(sessao);
        await _db.SaveChangesAsync(ct);
        return sessao;
    }

    public async Task AtualizarAsync(SessaoCaixa sessao, CancellationToken ct = default)
    {
        _db.SessoesCaixa.Update(sessao);
        await _db.SaveChangesAsync(ct);
    }
}
