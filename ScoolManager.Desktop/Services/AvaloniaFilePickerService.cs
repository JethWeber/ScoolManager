using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace ScoolManager.Desktop.Services;

public class AvaloniaFilePickerService : IFilePickerService
{
    public async Task<string?> SelecionarArquivoAsync(string titulo, params string[] extensoesPermitidas)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is null)
            return null;

        var filtros = new List<FilePickerFileType>();
        if (extensoesPermitidas.Length > 0)
        {
            filtros.Add(new FilePickerFileType("Arquivos suportados")
            {
                Patterns = extensoesPermitidas.Select(e => $"*.{e.TrimStart('.')}").ToArray()
            });
        }

        var resultado = await desktop.MainWindow.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = titulo,
            AllowMultiple = false,
            FileTypeFilter = filtros.Count > 0 ? filtros : null
        });

        // TryGetLocalPath() resolve o caminho real do arquivo tanto em
        // Fedora (~/...) quanto em Windows (C:\Users\...).
        return resultado.FirstOrDefault()?.TryGetLocalPath();
    }
}
