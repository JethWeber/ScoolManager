using Material.Icons;

namespace ScoolManager.Desktop.Models;

/// <summary>
/// Representa uma entrada do histórico de backups (aba Backup &amp; Segurança).
/// </summary>
public class BackupItemModel
{
    public string NomeArquivo { get; set; } = string.Empty;

    /// <summary>Ex.: "24 Out 2023 | 124.5 MB | Servidor Local".</summary>
    public string DetalheLabel { get; set; } = string.Empty;

    public MaterialIconKind Icon { get; set; } = MaterialIconKind.FileDocumentOutline;

    /// <summary>Backup na nuvem mostra o ícone "Restaurar"; backup local mostra "Descarregar".</summary>
    public bool EhNaNuvem { get; set; }
}
