using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfBackupRepository : IBackupRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfBackupRepository(ScoolManagerDbContext db) => _db = db;

    public async Task<IReadOnlyList<BackupRegistro>> ObterTodosAsync(CancellationToken ct = default) =>
        await _db.Backups.OrderByDescending(b => b.DataCriacao).ToListAsync(ct);

    public async Task<BackupRegistro?> ObterPorIdAsync(int id, CancellationToken ct = default) =>
        await _db.Backups.FindAsync([id], ct);

    public async Task<BackupRegistro> AdicionarAsync(BackupRegistro backup, CancellationToken ct = default)
    {
        _db.Backups.Add(backup);
        await _db.SaveChangesAsync(ct);
        return backup;
    }
}
