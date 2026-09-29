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
            var bars = ReceitaPlot.Plot.Add.Bars(valores.ToArray());
            ReceitaPlot.Plot.Axes.Margins(bottom: 0, top: 0.15);

            var ticks = new ScottPlot.Tick[labels.Count];
            for (var i = 0; i < labels.Count; i++)
                ticks[i] = new ScottPlot.Tick(i, labels[i]);

            ReceitaPlot.Plot.Axes.Bottom.TickGenerator =
                new ScottPlot.TickGenerators.NumericManual(ticks);

            ReceitaPlot.Plot.Axes.Bottom.MajorTickStyle.Length = 0;
            ReceitaPlot.Plot.Axes.Margins(bottom: 0);
            ReceitaPlot.Plot.YLabel("Kz");
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
