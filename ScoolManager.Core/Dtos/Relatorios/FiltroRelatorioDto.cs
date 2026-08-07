namespace ScoolManager.Core.Dtos.Relatorios;

/// <summary>
/// Filtros do modal "Configurar Relatório" (View 6 — Relatórios, ver
/// SM_Flow.md). Um único objeto partilhado entre todos os tipos de
/// relatório — cada `IRelatorioService.Gerar*Async` usa só os campos
/// relevantes ao seu <c>RelatorioTipo</c>.
///
/// Migrado de <c>ScoolManager.Desktop.ViewModels.Pages.RelatorioFiltro</c>.
/// Aqui é DTO (não entidade) porque é um contrato de consulta, não algo
/// persistido.
/// </summary>
public class FiltroRelatorioDto
{
    public string? Periodo { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public string? AnoLectivo { get; set; }
    public string? Turma { get; set; }
    public string? Classe { get; set; }
    public string? MetodoPagamento { get; set; }
}
