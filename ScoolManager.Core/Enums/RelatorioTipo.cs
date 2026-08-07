namespace ScoolManager.Core.Enums;

/// <summary>
/// Os 7 relatórios previstos na View 6 (Relatórios) do SM_Flow.md.
///
/// Migrado tal como está de <c>ScoolManager.Desktop.ViewModels.Pages</c>
/// (ficheiro <c>RelatoriosModels.cs</c>) — já era um enum "puro", sem
/// dependência de UI.
/// </summary>
public enum RelatorioTipo
{
    Matriculas,
    ListaAlunos,
    PropinasPagas,
    PropinasAtraso,
    Entradas,
    Saidas,
    FluxoCaixa
}
