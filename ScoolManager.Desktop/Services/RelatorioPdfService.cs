using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using PdfColors = QuestPDF.Helpers.Colors;
namespace ScoolManager.Desktop.Services;

public interface IRelatorioPdfService
{
    Task GerarAsync(string titulo, string subtitulo, IReadOnlyList<string> cabecalhos, IReadOnlyList<IReadOnlyList<string>> linhas, string caminho);
}

public sealed class RelatorioPdfService : IRelatorioPdfService
{
    public RelatorioPdfService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public Task GerarAsync(string titulo, string subtitulo, IReadOnlyList<string> cabecalhos, IReadOnlyList<IReadOnlyList<string>> linhas, string caminho)
    {
        Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(32);
                page.DefaultTextStyle(x => x.FontSize(9));

                page.Header().Column(column =>
                {
                    column.Item().Text(titulo).FontSize(20).Bold();
                    column.Item().Text(subtitulo).FontSize(9).FontColor(PdfColors.Grey.Darken1);
                    column.Item().PaddingTop(8).LineHorizontal(1);
                });

                page.Content().PaddingTop(16).Column(column =>
                {
                    if (linhas.Count == 0)
                    {
                        column.Item().Padding(20).Text("Não existem dados para os filtros selecionados.");
                        return;
                    }

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var _ in cabecalhos)
                                columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            foreach (var h in cabecalhos)
                                header.Cell().Background(PdfColors.Blue.Darken2).Padding(5).Text(h).FontColor(PdfColors.White).Bold();
                        });

                        foreach (var linha in linhas)
                        {
                            foreach (var valor in linha)
                                table.Cell().BorderBottom(0.5f).BorderColor(PdfColors.Grey.Lighten2).Padding(5).Text(valor ?? string.Empty);
                        }
                    });

                    column.Item().PaddingTop(12)
                        .Text($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm} · {linhas.Count} registo(s)")
                        .FontSize(8).FontColor(PdfColors.Grey.Darken1);
                });
            });
        }).GeneratePdf(caminho);

        return Task.CompletedTask;
    }
}
