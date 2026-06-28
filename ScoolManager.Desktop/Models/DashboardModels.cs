using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;

namespace ScoolManager.Desktop.Models;

/// <summary>
/// Representa um cartão de KPI no topo do Dashboard
/// (Alunos Ativos, Receita do Mês, Em Dívida, Recebido Hoje).
/// </summary>
public partial class KpiCardModel : ObservableObject
{
    public MaterialIconKind Icon { get; init; }
    public string Label { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
    public string? Suffix { get; init; }
    public string TrendText { get; init; } = string.Empty;

    /// <summary>True = tendência positiva (verde, seta para cima).</summary>
    public bool TrendIsPositive { get; init; }

    /// <summary>True = ícone principal usa a cor de erro em vez da cor primária (ex: "Em Dívida").</summary>
    public bool IsAlert { get; init; }
}

/// <summary>
/// Representa uma linha na lista "Top 5 Devedores".
/// </summary>
public partial class DevedorModel : ObservableObject
{
    public int Rank { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string Turma { get; init; } = string.Empty;
    public string ValorEmDivida { get; init; } = string.Empty;

    /// <summary>Iniciais usadas no avatar (ex.: "JP" para João Pedro).</summary>
    public string Iniciais { get; init; } = string.Empty;
}

/// <summary>
/// Representa um botão de atalho rápido (Novo Aluno, Novo Pagamento, etc.).
/// </summary>
public partial class AtalhoRapidoModel : ObservableObject
{
    public MaterialIconKind Icon { get; init; }
    public string Label { get; init; } = string.Empty;
}

/// <summary>
/// Um ponto (círculo) sobre a linha do gráfico de receita.
/// Coordenadas no mesmo sistema do viewBox "0 0 800 250" usado no Path do fundo/linha.
/// </summary>
public class ChartMarkerPoint
{
    public double X { get; init; }
    public double Y { get; init; }
    public string MesLabel { get; init; } = string.Empty;
}
