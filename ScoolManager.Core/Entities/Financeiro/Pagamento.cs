using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Financeiro;

/// <summary>
/// Um pagamento/cobrança de um Aluno (aba "Recebimentos" da View 4 —
/// Financeiro, ver SM_Flow.md e FinanceiroViewModel real).
///
/// Migrado de <c>PagamentoHistoricoItem</c> (classe interna de
/// <c>DetalhesAlunoViewModel</c>). Diferenças: <c>Valor</c> era
/// <c>string</c> formatada ("25.000 Kz") — aqui é <c>decimal</c>;
/// <c>MesReferencia</c> era <c>string</c> ("Abril 2026") — aqui é
/// <c>DateOnly</c> (primeiro dia do mês); <c>Pago: bool</c> passa a
/// <see cref="EstadoPagamento"/>. <c>StatusTexto</c>/<c>StatusBrush</c>
/// não sobem — ficam na UI.
///
/// CORREÇÃO (gap identificado ao ler o FinanceiroViewModel real): esta
/// entidade tinha sido desenhada só para propinas mensais. A view real
/// trata qualquer cobrança da escola (matrícula, confirmação, uniforme,
/// etc. — ver <see cref="TipoCobranca"/>) e permite anular um pagamento já
/// registado (com motivo) — isto é ortogonal a <see cref="Estado"/>
/// (Pago/EmAtraso, que é sobre atraso de propina): um pagamento "Pago" pode
/// mais tarde ser "Anulado" por engano de registo.
/// </summary>
public class Pagamento
{
    public int Id { get; set; }

    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    /// <summary>Mês a que a propina se refere (guardado como 1º dia do mês). Só relevante quando Tipo == Propina.</summary>
    public DateOnly MesReferencia { get; set; }

    /// <summary>Categoria da cobrança (Matrícula, Propina, Confirmação, Uniforme, ...).</summary>
    public TipoCobranca Tipo { get; set; }

    /// <summary>Referência/número do recibo (ex.: "#REC-4560").</summary>
    public string NumeroRecibo { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }

    public EstadoPagamento Estado { get; set; }

    /// <summary>Cancelamento de um pagamento já registado — ortogonal a Estado, ver nota da classe.</summary>
    public bool Anulado { get; set; }
    public string? MotivoAnulacao { get; set; }

    public string? MetodoPagamento { get; set; }

    /// <summary>Sessão de caixa em que o pagamento foi registado (ver <see cref="SessaoCaixa"/>).</summary>
    public int? SessaoCaixaId { get; set; }
    public SessaoCaixa? SessaoCaixa { get; set; }
}
