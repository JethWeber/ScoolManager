using ScoolManager.Core.Abstractions;

namespace ScoolManager.Desktop.Services;

public interface IExportacaoArquivoService
{
    Task<string?> ExportarPdfAsync(
        string titulo,
        string nomeSugerido,
        IReadOnlyList<string> colunas,
        IReadOnlyList<string[]> linhas,
        string? subtitulo = null);

    Task<string?> ExportarExcelAsync(
        string nomeSugerido,
        IReadOnlyList<string> colunas,
        IReadOnlyList<string[]> linhas,
        string nomeFolha = "Dados");
}

public sealed class ExportacaoArquivoService : IExportacaoArquivoService
{
    private readonly IExportService _export;
    private readonly IFilePickerService _filePicker;

    public ExportacaoArquivoService(IExportService export, IFilePickerService filePicker)
    {
        _export = export;
        _filePicker = filePicker;
    }

    public async Task<string?> ExportarPdfAsync(
        string titulo,
        string nomeSugerido,
        IReadOnlyList<string> colunas,
        IReadOnlyList<string[]> linhas,
        string? subtitulo = null)
    {
        var file = await _filePicker.SelecionarDestinoArquivoAsync(
            "Guardar relatório PDF", EnsureExtension(nomeSugerido, "pdf"), "pdf");

        if (file is null)
            return null;

        var bytes = _export.ExportarParaPdf(titulo, colunas, linhas);
        await using var stream = await file.OpenWriteAsync();
        await stream.WriteAsync(bytes);

        return file.TryGetLocalPath() ?? file.Name;
    }

    public async Task<string?> ExportarExcelAsync(
        string nomeSugerido,
        IReadOnlyList<string> colunas,
        IReadOnlyList<string[]> linhas,
        string nomeFolha = "Dados")
    {
        var file = await _filePicker.SelecionarDestinoArquivoAsync(
            "Guardar ficheiro Excel", EnsureExtension(nomeSugerido, "xlsx"), "xlsx");

        if (file is null)
            return null;

        var bytes = _export.ExportarParaExcel(colunas, linhas, nomeFolha);
        await using var stream = await file.OpenWriteAsync();
        await stream.WriteAsync(bytes);

        return file.TryGetLocalPath() ?? file.Name;
    }

    private static string EnsureExtension(string nome, string extensao)
        => nome.EndsWith($".{extensao}", StringComparison.OrdinalIgnoreCase)
            ? nome
            : $"{nome}.{extensao}";
}