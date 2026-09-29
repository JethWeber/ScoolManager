using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace ScoolManager.Desktop.Services;

public sealed class AvaloniaFilePickerService : IFilePickerService
{
    public async Task<string?> SelecionarArquivoAsync(string titulo, params string[] extensoesPermitidas)
    {
        var file = await SelecionarArquivoStorageAsync(titulo, extensoesPermitidas);
        return file?.TryGetLocalPath();
    }

    public async Task<string?> SelecionarDestinoAsync(
        string titulo, string nomeSugerido, params string[] extensoesPermitidas)
    {
        var file = await SelecionarDestinoArquivoAsync(titulo, nomeSugerido, extensoesPermitidas);
        return file?.TryGetLocalPath();
    }

    public async Task<IStorageFile?> SelecionarDestinoArquivoAsync(
        string titulo, string nomeSugerido, params string[] extensoesPermitidas)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is null)
            return null;

        var filtros = extensoesPermitidas
            .Where(e => !string.IsNullOrWhiteSpace(e))
            .Select(e => e.TrimStart('.'))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(e => new FilePickerFileType(e.ToUpperInvariant())
            {
                Patterns = [$"*.{e}"]
            })
            .ToList();

        if (filtros.Count == 0)
            filtros.Add(new FilePickerFileType("Todos os ficheiros") { Patterns = ["*"] });

        var resultado = await desktop.MainWindow.StorageProvider.SaveFilePickerAsync(
            new FilePickerSaveOptions
            {
                Title = titulo,
                SuggestedFileName = nomeSugerido,
                DefaultExtension = extensoesPermitidas.FirstOrDefault()?.TrimStart('.'),
                FileTypeChoices = filtros,
                ShowOverwritePrompt = true
            });

        return resultado;
    }

    private static async Task<IStorageFile?> SelecionarArquivoStorageAsync(
        string titulo, params string[] extensoesPermitidas)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is null)
            return null;

        var filtros = extensoesPermitidas
            .Where(e => !string.IsNullOrWhiteSpace(e))
            .Select(e => e.TrimStart('.'))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(e => new FilePickerFileType(e.ToUpperInvariant())
            {
                Patterns = [$"*.{e}"]
            })
            .ToList();

        var resultado = await desktop.MainWindow.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = titulo,
                AllowMultiple = false,
                FileTypeFilter = filtros.Count > 0 ? filtros : null
            });

        return resultado.FirstOrDefault();
    }
}