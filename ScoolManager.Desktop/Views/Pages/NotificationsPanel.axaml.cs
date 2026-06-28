using Avalonia.Controls;
using ScoolManager.Desktop.ViewModels;

namespace ScoolManager.Desktop.Views.Pages
{
    public partial class NotificationsPanel : UserControl
    {
        public NotificationsPanel()
        {
            InitializeComponent();
            DataContext = new NotificationsPanelViewModel();
        }
    }
}
