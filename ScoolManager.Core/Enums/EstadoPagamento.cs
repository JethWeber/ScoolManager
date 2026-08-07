namespace ScoolManager.Core.Enums;

/// <summary>
/// Estado de um <c>Pagamento</c> (propina).
///
/// Extraído para enum a partir de dois lugares do Desktop que hoje
/// representam a mesma informação de formas diferentes:
/// <c>PagamentoHistoricoItem.Pago</c> (bool) e
/// <c>PropinaRelatorioItem.Estado</c> ("Pago" / "Em Atraso", string livre).
/// </summary>
public enum EstadoPagamento
{
    Pago,
    EmAtraso
}
