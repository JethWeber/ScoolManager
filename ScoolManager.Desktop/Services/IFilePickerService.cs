using Avalonia.Platform.Storage;

namespace ScoolManager.Desktop.Services;

public interface IFilePickerService
{
    Task<string?> SelecionarArquivoAsync(string titulo, params string[] extensoesPermitidas);
    Task<string?> SelecionarDestinoAsync(string titulo, string nomeSugerido, params string[] extensoesPermitidas);
    Task<IStorageFile?> SelecionarDestinoArquivoAsync(string titulo, string nomeSugerido, params string[] extensoesPermitidas);
}