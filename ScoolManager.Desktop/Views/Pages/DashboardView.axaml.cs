using System;
using Avalonia.Controls;
using Avalonia.Input;
using ScoolManager.Desktop.ViewModels;
using ScoolManager.Desktop.ViewModels.Pages;

namespace ScoolManager.Desktop.Views.Pages;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext is IAsyncInitializable initializable)
            _ = initializable.InitializeAsync();
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
