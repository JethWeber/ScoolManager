using System;
using Avalonia.Controls;
using Avalonia.Input;
using ScoolManager.Desktop.ViewModels;
using ScoolManager.Desktop.ViewModels.Pages;

namespace ScoolManager.Desktop.Views.Pages;

public partial class DashboardView : UserControl
{
    private DashboardViewModel? _dashboardViewModel;

    public DashboardView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (_dashboardViewModel is not null)
            _dashboardViewModel.ChartAtualizado -= AtualizarGrafico;

        base.OnDataContextChanged(e);

        _dashboardViewModel = DataContext as DashboardViewModel;

        if (_dashboardViewModel is not null)
        {
            _dashboardViewModel.ChartAtualizado += AtualizarGrafico;
            AtualizarGrafico();
        }

        if (DataContext is IAsyncInitializable initializable)
            _ = initializable.InitializeAsync();
    }

    private void AtualizarGrafico()
    {
        if (_dashboardViewModel is null)
            return;

        var valores = _dashboardViewModel.ChartValues;
        var labels = _dashboardViewModel.ChartLabels;

        ReceitaPlot.Plot.Clear();

        if (valores.Count > 0)
        {
            // Dashboard: linha de tendência limpa em vez de barras de relatório.
            var xs = Enumerable.Range(0, valores.Count).Select(i => (double)i).ToArray();
            var linha = ReceitaPlot.Plot.Add.Scatter(xs, valores.ToArray());

            linha.LineWidth = 3;
            linha.MarkerSize = 7;
            linha.MarkerShape = ScottPlot.MarkerShape.FilledCircle;

            var ticks = new ScottPlot.Tick[labels.Count];
            for (var i = 0; i < labels.Count; i++)
                ticks[i] = new ScottPlot.Tick(i, labels[i]);

            ReceitaPlot.Plot.Axes.Bottom.TickGenerator =
                new ScottPlot.TickGenerators.NumericManual(ticks);

            ReceitaPlot.Plot.Axes.Bottom.MajorTickStyle.Length = 0;
            ReceitaPlot.Plot.Axes.Left.MajorTickStyle.Length = 0;
            ReceitaPlot.Plot.Axes.Margins(bottom: 0.05, top: 0.12);
            ReceitaPlot.Plot.YLabel("Kz");
            ReceitaPlot.Plot.Axes.AutoScale();

            // Mantém o gráfico visualmente leve: sem legenda, título ou moldura extra.
            ReceitaPlot.Plot.Axes.Right.IsVisible = false;
            ReceitaPlot.Plot.Axes.Top.IsVisible = false;
        }

        ReceitaPlot.Refresh();
    }

    private void NotificationBellButton_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        NotificationsPopup.IsOpen = !NotificationsPopup.IsOpen;
    }

    private void NotificationsPopup_Opened(object? sender, EventArgs e)
    {
        var shell = NotificationsPanelControl.FindControl<Border>("GlassShell");
        if (shell != null)
        {
            shell.Classes.Remove("closed");
            shell.Classes.Add("open");
        }

        if (NotificationsPanelControl.DataContext is NotificationsPanelViewModel vm)
        {
            vm.RequestClose += (_, _) => NotificationsPopup.IsOpen = false;
        }
    }
}
