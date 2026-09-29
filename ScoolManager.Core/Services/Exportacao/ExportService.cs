using System.Text;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ScoolManager.Core.Abstractions;

namespace ScoolManager.Core.Services.Exportacao;

public sealed class ExportService : IExportService
{
    static ExportService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public byte[] ExportarParaPdf(string titulo, IReadOnlyList<string> colunas, IReadOnlyList<IReadOnlyList<string>> linhas)
    {
        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(9));

                page.Header().Column(col =>
                {
                    col.Item().Text(titulo).FontSize(18).Bold();
                    col.Item().Text($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(8).FontColor(Colors.Grey.Medium);
                    col.Item().PaddingTop(6).LineHorizontal(1);
                });

                page.Content().PaddingTop(12).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        foreach (var _ in colunas)
                            columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        foreach (var coluna in colunas)
                            header.Cell().Background(Colors.Blue.Darken2).Padding(5)
                                .Text(coluna).FontColor(Colors.White).Bold();
                    });

                    foreach (var linha in linhas)
                    {
                        foreach (var valor in linha)
                            table.Cell().BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(5).Text(valor ?? string.Empty);
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        });

        return documento.GeneratePdf();
    }

    public byte[] ExportarParaCsv(IReadOnlyList<string> colunas, IReadOnlyList<IReadOnlyList<string>> linhas)
    {
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(';', colunas.Select(EscaparCampo)));

        foreach (var linha in linhas)
            sb.AppendLine(string.Join(';', linha.Select(EscaparCampo)));

        var preamble = Encoding.UTF8.GetPreamble();
        var corpo = Encoding.UTF8.GetBytes(sb.ToString());
        return [.. preamble, .. corpo];
    }

    public byte[] ExportarParaExcel(
        IReadOnlyList<string> colunas,
        IReadOnlyList<IReadOnlyList<string>> linhas,
        string nomeFolha = "Dados")
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(
            string.IsNullOrWhiteSpace(nomeFolha) ? "Dados" : nomeFolha);

        for (var coluna = 0; coluna < colunas.Count; coluna++)
        {
            var cell = worksheet.Cell(1, coluna + 1);
            cell.Value = colunas[coluna];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1E3A5F");
            cell.Style.Font.FontColor = XLColor.White;
        }

        for (var linha = 0; linha < linhas.Count; linha++)
        {
            var valores = linhas[linha];
            for (var coluna = 0; coluna < valores.Count; coluna++)
                worksheet.Cell(linha + 2, coluna + 1).Value = valores[coluna] ?? string.Empty;
        }

        if (colunas.Count > 0)
        {
            var ultimaLinha = Math.Max(1, linhas.Count + 1);
            worksheet.Range(1, 1, ultimaLinha, colunas.Count).SetAutoFilter();
            worksheet.SheetView.FreezeRows(1);
            worksheet.Columns().AdjustToContents();
        }

        worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
        worksheet.PageSetup.FitToPages(1, 0);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static string EscaparCampo(string valor)
    {
        if (valor.Contains(';') || valor.Contains('"') || valor.Contains('\n'))
            return $"\"{valor.Replace("\"", "\"\"")}\"";

        return valor;
    }
}