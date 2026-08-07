namespace ScoolManager.Core.Dtos.Relatorios;

/// <summary>
/// DTOs de resultado da Pré-Visualização de cada um dos 7 relatórios
/// (View 6 — Relatórios, ver SM_Flow.md), um por <c>RelatorioTipo</c>.
///
/// Migrados de <c>ScoolManager.Desktop.ViewModels.Pages.RelatoriosModels.cs</c>.
/// Diferença: lá, os campos já vinham como <c>string</c> formatada para
/// exibição direta na tabela (datas em "dd/MM/yyyy", valores em "25.000 Kz")
/// — aqui usam tipos primitivos (<c>decimal</c>, <c>DateTime</c>); a
/// formatação final para a tabela/exportação fica na camada de apresentação.
/// </summary>

public class MatriculaRelatorioDto
{
    public string Aluno { get; set; } = string.Empty;
    public string NumeroMatricula { get; set; } = string.Empty;
    public string Turma { get; set; } = string.Empty;
    public string Classe { get; set; } = string.Empty;
    public DateTime DataMatricula { get; set; }
    public string Estado { get; set; } = string.Empty;
}

public class AlunoRelatorioDto
{
    public string Nome { get; set; } = string.Empty;
    public string NumeroMatricula { get; set; } = string.Empty;
    public string Classe { get; set; } = string.Empty;
    public string Turma { get; set; } = string.Empty;
    public string Situacao { get; set; } = string.Empty;
    public string Contacto { get; set; } = string.Empty;
}

/// <summary>Usado tanto por "Propinas Pagas" como por "Propinas em Atraso" — o campo Estado distingue os dois.</summary>
public class PropinaRelatorioDto
{
    public string Aluno { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public string Estado { get; set; } = string.Empty; // "Pago" ou "Em Atraso"
}

/// <summary>Usado tanto por "Entradas" como por "Saídas" — o campo Tipo distingue os dois relatórios.</summary>
public class RelatorioMovimentoDto
{
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public string Tipo { get; set; } = string.Empty; // "Entrada" ou "Saida"
}

public class FluxoCaixaRelatorioDto
{
    public string Periodo { get; set; } = string.Empty;
    public decimal SaldoInicial { get; set; }
    public decimal TotalEntradas { get; set; }
    public decimal TotalSaidas { get; set; }
    public decimal SaldoFinal { get; set; }
}
