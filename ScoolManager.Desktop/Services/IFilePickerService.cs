namespace ScoolManager.Desktop.Services;

public interface IFilePickerService
{
    /// <summary>Abre o diálogo nativo de seleção de arquivo e devolve o caminho local escolhido (ou null se cancelado).</summary>
    Task<string?> SelecionarArquivoAsync(string titulo, params string[] extensoesPermitidas);
}
