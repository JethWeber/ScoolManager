using ScoolManager.Core.Entities.Configuracoes;

namespace ScoolManager.Core.Abstractions.Services;

/// <summary>Serviço da aba "Backup" (View 7 — Configurações, ver SM_Flow.md).</summary>
public interface IBackupService
{
    Task<IReadOnlyList<BackupRegistro>> ObterTodosAsync(CancellationToken ct = default);

    /// <summary>Gera um novo backup do ficheiro SQLite e regista a entrada correspondente.</summary>
    Task<BackupRegistro> CriarBackupAsync(CancellationToken ct = default);

    Task RestaurarAsync(int backupId, CancellationToken ct = default);

    /// <summary>CORREÇÃO (gap 7): faltava onde persistir os 3 toggles (BackupDiarioAutomatico, SincronizacaoNuvem, NotificarFalhasEmail).</summary>
    Task<ConfiguracaoBackup> ObterConfiguracaoAsync(CancellationToken ct = default);
    Task AtualizarConfiguracaoAsync(ConfiguracaoBackup configuracao, CancellationToken ct = default);
}
