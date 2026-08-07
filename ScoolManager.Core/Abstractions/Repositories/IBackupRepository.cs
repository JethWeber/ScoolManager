using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Abstractions.Repositories;

public interface IBackupRepository
{
    Task<IReadOnlyList<BackupRegistro>> ObterTodosAsync(CancellationToken ct = default);
    Task<BackupRegistro?> ObterPorIdAsync(int id, CancellationToken ct = default);
    Task<BackupRegistro> AdicionarAsync(BackupRegistro backup, CancellationToken ct = default);
}
