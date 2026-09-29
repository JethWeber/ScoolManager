namespace ScoolManager.Core.Abstractions;

public interface IExportService
{
    byte[] ExportarParaPdf(string titulo, IReadOnlyList<string> colunas, IReadOnlyList<IReadOnlyList<string>> linhas);
    byte[] ExportarParaCsv(IReadOnlyList<string> colunas, IReadOnlyList<IReadOnlyList<string>> linhas);
    byte[] ExportarParaExcel(IReadOnlyList<string> colunas, IReadOnlyList<IReadOnlyList<string>> linhas, string nomeFolha = "Dados");
}