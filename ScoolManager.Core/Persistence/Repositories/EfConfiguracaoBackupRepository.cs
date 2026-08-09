using Microsoft.EntityFrameworkCore;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Persistence.Repositories;

public class EfConfiguracaoBackupRepository : IConfiguracaoBackupRepository
{
    private readonly ScoolManagerDbContext _db;
    public EfConfiguracaoBackupRepository(ScoolManagerDbContext db) => _db = db;

    // Seed garante a linha Id = 1 desde a primeira migration.
    public async Task<ConfiguracaoBackup> ObterAsync(CancellationToken ct = default) =>
        await _db.ConfiguracoesBackup.FirstAsync(ct);

    public async Task AtualizarAsync(ConfiguracaoBackup configuracao, CancellationToken ct = default)
    {
        _db.ConfiguracoesBackup.Update(configuracao);
        await _db.SaveChangesAsync(ct);
    }
}
