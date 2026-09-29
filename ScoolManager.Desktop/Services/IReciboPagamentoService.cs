namespace ScoolManager.Desktop.Services;

public sealed record ResultadoReciboPagamento(
    string CaminhoPdf,
    bool ImpressaoEnviada);

public interface IReciboPagamentoService
{
    Task<ResultadoReciboPagamento> GuardarEImprimirAsync(
        Pagamento pagamento,
        string aluno,
        string descricao,
        CancellationToken ct = default);

    Task<ResultadoReciboPagamento> GuardarEImprimirAsync(
        string aluno,
        string numeroRecibo,
        decimal valor,
        DateTime data,
        string metodo,
        string descricao,
        CancellationToken ct = default);
}
