using Avalonia.Controls;
using Avalonia.Input;
using ScoolManager.Desktop.ViewModels;

namespace ScoolManager.Desktop.Views.Pages;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();
    }

    private void NotificationBellButton_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        NotificationsPopup.IsOpen = !NotificationsPopup.IsOpen;
    }

    private void NotificationsPopup_Opened(object? sender, System.EventArgs e)
    {
        // Dispara a animação de entrada (classes "closed" -> "open"
        // definidas nos Styles do NotificationsPanel.axaml).
        var shell = NotificationsPanelControl.FindControl<Border>("GlassShell");
        if (shell != null)
        {
            shell.Classes.Remove("closed");
            shell.Classes.Add("open");
        }

        // Liga o "✕" do painel ao fecho do Popup.
        if (NotificationsPanelControl.DataContext is NotificationsPanelViewModel vm)
        {
            vm.RequestClose += (_, _) => NotificationsPopup.IsOpen = false;
        }
    }
}