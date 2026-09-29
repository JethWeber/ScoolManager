using ScoolManager.Core.Abstractions;
using ScoolManager.Core.Abstractions.Repositories;
using ScoolManager.Core.Entities.Financeiro;

namespace ScoolManager.Desktop.Services;

public sealed class ReciboPagamentoService : IReciboPagamentoService
{
    private readonly IExportService _export;
    private readonly IDadosInstituicaoRepository _dadosInstituicao;
    private readonly IImpressoraTermicaService _impressora;

    public ReciboPagamentoService(
        IExportService export,
        IDadosInstituicaoRepository dadosInstituicao,
        IImpressoraTermicaService impressora)
    {
        _export = export;
        _dadosInstituicao = dadosInstituicao;
        _impressora = impressora;
    }

    public Task<ResultadoReciboPagamento> GuardarEImprimirAsync(
        Pagamento pagamento,
        string aluno,
        string descricao,
        CancellationToken ct = default)
        => GuardarEImprimirAsync(
            aluno,
            pagamento.NumeroRecibo,
            pagamento.Valor,
            pagamento.DataPagamento ?? DateTime.Now,
            pagamento.MetodoPagamento ?? "—",
            descricao,
            ct);

    public async Task<ResultadoReciboPagamento> GuardarEImprimirAsync(
        string aluno,
        string numeroRecibo,
        decimal valor,
        DateTime data,
        string metodo,
        string descricao,
        CancellationToken ct = default)
    {
        var escola = await _dadosInstituicao.ObterAsync(ct);
        var pasta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ScoolManager",
            "Recibos");

        Directory.CreateDirectory(pasta);

        var nome = $"Recibo_{numeroRecibo}_{data:yyyyMMdd_HHmmss}.pdf";
        var caminho = Path.Combine(pasta, nome);

        var linhas = new List<IReadOnlyList<string>>
        {
            new[] { "Aluno", aluno },
            new[] { "Recibo", numeroRecibo },
            new[] { "Descrição", descricao },
            new[] { "Valor", $"{valor:N2} Kz" },
            new[] { "Data", data.ToString("dd/MM/yyyy HH:mm") },
            new[] { "Método", metodo },
            new[] { "Estado", "Pago" }
        };

        var pdf = _export.ExportarParaPdf(
            string.IsNullOrWhiteSpace(escola.NomeInstituicao)
                ? "Recibo de Pagamento"
                : escola.NomeInstituicao,
            new[] { "Campo", "Valor" },
            linhas);

        await File.WriteAllBytesAsync(caminho, pdf, ct);

        var reciboTermico = $"""
{escola.NomeInstituicao}
{escola.TelefonePrincipal}

          RECIBO
------------------------------
Aluno: {aluno}
Recibo: {numeroRecibo}
Descricao: {descricao}
Valor: {valor:N2} Kz
Data: {data:dd/MM/yyyy HH:mm}
Metodo: {metodo}
------------------------------
        PAGAMENTO OK
------------------------------

""";

        var impresso = await _impressora.ImprimirAsync(reciboTermico, ct);
        return new ResultadoReciboPagamento(caminho, impresso);
    }
}
