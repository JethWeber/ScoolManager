namespace ScoolManager.Desktop.Services;

public interface IFilePickerService
{
    Task<string?> SelecionarArquivoAsync(string titulo, params string[] extensoesPermitidas);

    /// <summary>Abre o diálogo nativo "Guardar como" e devolve o caminho escolhido.</summary>
    Task<string?> SelecionarDestinoAsync(string titulo, string nomeSugerido, params string[] extensoesPermitidas);
}
