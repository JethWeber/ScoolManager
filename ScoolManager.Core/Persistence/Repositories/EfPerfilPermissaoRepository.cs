using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Identidade;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfPerfilPermissaoRepository : IPerfilPermissaoRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfPerfilPermissaoRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<PerfilPermissao>> ObterTodosAsync(CancellationToken ct = default) =>
        await _db.PerfisPermissao.OrderBy(p => p.Perfil).ToListAsync(ct);

    public async Task<PerfilPermissao?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.PerfisPermissao.FindAsync([id], ct);

    public async Task<PerfilPermissao> AdicionarAsync(PerfilPermissao perfil, CancellationToken ct = default)
    {
        _db.PerfisPermissao.Add(perfil);
        await _db.SaveChangesAsync(ct);
        return perfil;
    }

    public async Task AtualizarAsync(PerfilPermissao perfil, CancellationToken ct = default)
    {
        _db.PerfisPermissao.Update(perfil);
        await _db.SaveChangesAsync(ct);
    }
}
