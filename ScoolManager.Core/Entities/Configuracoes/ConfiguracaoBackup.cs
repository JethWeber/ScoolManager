namespace ScoolManager.Core.Entities.Configuracoes;

/// <summary>
/// Configurações do módulo de Backup (aba "Backup" da View 7 —
/// Configurações, ver SM_Flow.md). Singleton de domínio: uma só linha
/// (seed com Id = 1), tal como <see cref="DadosInstituicao"/>.
///
/// CORREÇÃO (gap identificado ao cruzar com ConfiguracoesViewModel): os 3
/// toggles (BackupDiarioAutomatico, SincronizacaoNuvem, NotificarFalhasEmail)
/// e a "Última verificação de integridade" não tinham nenhuma entidade do
/// Core onde viver — hoje só existem como propriedades soltas na ViewModel,
/// nunca persistidas de verdade.
/// </summary>
public class ConfiguracaoBackup
{
    public int Id { get; set; }
    public bool BackupDiarioAutomatico { get; set; }
    public bool SincronizacaoNuvem { get; set; }
    public bool NotificarFalhasEmail { get; set; }

    /// <summary>
    /// Timestamp cru da última verificação de integridade. A UI compõe a
    /// label ("há 2 horas. Nenhum erro encontrado.") a partir disto — o
    /// "nenhum erro encontrado" fica implícito enquanto não existir um
    /// campo próprio para registar falhas (não pedido ainda pela spec).
    /// </summary>
    public DateTime? UltimaVerificacaoIntegridade { get; set; }
}
