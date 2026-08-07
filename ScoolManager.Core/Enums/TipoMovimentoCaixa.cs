namespace ScoolManager.Core.Enums;

/// <summary>
/// Tipo de um <c>MovimentoCaixa</c> (aba "Entradas" / "Saídas" da View 4 —
/// Financeiro, ver SM_Flow.md).
///
/// Extraído para enum a partir de <c>RelatorioMovimentoItem.Tipo</c>, que
/// hoje é uma string livre ("Entrada" / "Saida").
/// </summary>
public enum TipoMovimentoCaixa
{
    Entrada,
    Saida
}
