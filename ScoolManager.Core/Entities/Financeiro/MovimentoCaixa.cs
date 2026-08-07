using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Financeiro;

/// <summary>
/// Uma entrada ou saída de caixa (abas "Entradas"/"Saídas" da View 4 —
/// Financeiro, ver SM_Flow.md).
///
/// Migrado a partir dos campos vistos em <c>RelatorioMovimentoItem</c>
/// (que é a "vista" destes mesmos dados usada nos Relatórios). <c>Valor</c>
/// e <c>Data</c> passam de <c>string</c> formatada para <c>decimal</c>/
/// <c>DateTime</c>; <c>Tipo</c> ("Entrada"/"Saida" livre) passa a
/// <see cref="TipoMovimentoCaixa"/>.
///
/// DECISÃO DE DESIGN (não pedida explicitamente, mas necessária pela regra
/// da aba "Caixa"): todo movimento passa a estar ligado à
/// <see cref="SessaoCaixa"/> em que ocorreu, para permitir o fecho de caixa
/// somar exatamente os movimentos daquela sessão. <c>FinanceiroService</c>
/// preenche <see cref="SessaoCaixaId"/> automaticamente a partir da sessão
/// aberta no momento — a UI não precisa de escolher isto.
/// </summary>
public class MovimentoCaixa
{
    public int Id { get; set; }

    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public TipoMovimentoCaixa Tipo { get; set; }

    public int SessaoCaixaId { get; set; }
    public SessaoCaixa? SessaoCaixa { get; set; }
}
