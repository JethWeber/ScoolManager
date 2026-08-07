using ScoolManager.Core.Entities.Alunos;
using ScoolManager.Core.Enums;

namespace ScoolManager.Core.Entities.Financeiro;

/// <summary>
/// Um pagamento de propina de um Aluno (aba "Pagamentos" da View 4 —
/// Financeiro, ver SM_Flow.md).
///
/// Migrado de <c>PagamentoHistoricoItem</c> (classe interna de
/// <c>DetalhesAlunoViewModel</c>). Diferenças: <c>Valor</c> era
/// <c>string</c> formatada ("25.000 Kz") — aqui é <c>decimal</c>;
/// <c>MesReferencia</c> era <c>string</c> ("Abril 2026") — aqui é
/// <c>DateOnly</c> (primeiro dia do mês); <c>Pago: bool</c> passa a
/// <see cref="EstadoPagamento"/>. <c>StatusTexto</c>/<c>StatusBrush</c>
/// não sobem — ficam na UI.
///
/// NOTA (pendência aberta no roteiro): <c>MetodoPagamento</c> ficou como
/// <c>string?</c> livre porque o <c>FinanceiroViewModel</c> real (onde
/// provavelmente já existiria um conjunto fixo de métodos) não chegou a
/// ser enviado. Ajustar para enum quando esse ficheiro chegar.
/// </summary>
public class Pagamento
{
    public int Id { get; set; }

    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    /// <summary>Mês a que a propina se refere (guardado como 1º dia do mês).</summary>
    public DateOnly MesReferencia { get; set; }

    /// <summary>Referência/número do recibo (ex.: "#REC-4560").</summary>
    public string NumeroRecibo { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }

    public EstadoPagamento Estado { get; set; }

    /// <summary>Ver nota acima — pendente confirmação do FinanceiroViewModel real.</summary>
    public string? MetodoPagamento { get; set; }

    /// <summary>Sessão de caixa em que o pagamento foi registado (ver <see cref="SessaoCaixa"/>).</summary>
    public int? SessaoCaixaId { get; set; }
    public SessaoCaixa? SessaoCaixa { get; set; }
}
