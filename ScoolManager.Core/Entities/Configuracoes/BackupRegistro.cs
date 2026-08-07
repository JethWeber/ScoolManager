namespace ScoolManager.Core.Entities.Configuracoes;

/// <summary>
/// Uma entrada do histórico de backups (aba "Backup" da View 7 —
/// Configurações).
///
/// Migrado de <c>BackupItemModel</c>. Diferença: <c>DetalheLabel</c> (ex.:
/// "24 Out 2023 | 124.5 MB | Servidor Local", string já formatada) é
/// decomposto nos campos crus <see cref="DataCriacao"/>,
/// <see cref="TamanhoBytes"/> e <see cref="Localizacao"/> — a UI volta a
/// compor a label como quiser. <c>Icon</c> (MaterialIconKind) não sobe.
/// </summary>
public class BackupRegistro
{
    public int Id { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public long TamanhoBytes { get; set; }
    public string Localizacao { get; set; } = string.Empty;

    /// <summary>Backup na nuvem mostra o ícone "Restaurar"; backup local mostra "Descarregar" (decisão de UI).</summary>
    public bool EhNaNuvem { get; set; }
}
