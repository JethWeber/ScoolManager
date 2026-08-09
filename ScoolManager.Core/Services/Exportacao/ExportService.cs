using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ScoolManager.Core.Abstractions;

namespace ScoolManager.Core.Services.Exportacao;

public class ExportService : IExportService
{
    static ExportService()
    {
        // QuestPDF exige declarar o tipo de licença antes de gerar
        // qualquer documento — Community é gratuita para este perfil de uso
        // (produto interno, não uma ferramenta de geração de PDF vendida a terceiros).
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public byte[] ExportarParaPdf(string titulo, IReadOnlyList<string> colunas, IReadOnlyList<string[]> linhas)
    {
        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header().Column(col =>
                {
                    col.Item().Text(titulo).FontSize(16).Bold();
                    col.Item().Text($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Medium);
                });

                page.Content().PaddingTop(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        foreach (var _ in colunas)
                            columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        foreach (var coluna in colunas)
                        {
                            header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text(coluna).Bold();
                        }
                    });

                    foreach (var linha in linhas)
                    {
                        foreach (var valor in linha)
                        {
                            table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4).Text(valor);
                        }
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

    public byte[] ExportarParaCsv(IReadOnlyList<string> colunas, IReadOnlyList<string[]> linhas)
    {
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(';', colunas.Select(EscaparCampo)));

        foreach (var linha in linhas)
            sb.AppendLine(string.Join(';', linha.Select(EscaparCampo)));

        // BOM UTF-8 para o Excel reconhecer acentuação em português corretamente.
        var preamble = Encoding.UTF8.GetPreamble();
        var corpo = Encoding.UTF8.GetBytes(sb.ToString());
        return [.. preamble, .. corpo];
    }

    private static string EscaparCampo(string valor)
    {
        if (valor.Contains(';') || valor.Contains('"') || valor.Contains('\n'))
            return $"\"{valor.Replace("\"", "\"\"")}\"";

        return valor;
    }
}
