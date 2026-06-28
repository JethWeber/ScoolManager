using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.ViewModels
{
    /// <summary>
    /// ViewModel do NotificationsPanel (o "sino" do Dashboard).
    ///
    /// TODO: substituir o método LoadMockData() por uma chamada real
    /// a um INotificationService / repositório quando este existir.
    /// Por agora os dados são fixos para já dares forma ao visual.
    /// </summary>
    public partial class NotificationsPanelViewModel : ObservableObject
    {
        public ObservableCollection<NotificationItem> Notifications { get; } = new();

        [ObservableProperty]
        private int _unreadCount;

        [ObservableProperty]
        private bool _hasNoNotifications;

        public string SubtitleText =>
            UnreadCount > 0 ? $"{UnreadCount} não lida(s)" : "Tudo em dia";

        /// <summary>
        /// Disparado quando o utilizador clica no "✕" do painel.
        /// O code-behind do DashboardView subscreve isto para fechar o Popup.
        /// </summary>
        public event EventHandler? RequestClose;

        public NotificationsPanelViewModel()
        {
            LoadMockData();
            RecalculateUnreadCount();
        }

        [RelayCommand]
        private void Close()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        private void MarkAllAsRead()
        {
            foreach (var n in Notifications)
                n.IsRead = true;

            // Força o refresh dos bindings (IsRead não é observable no modelo).
            var snapshot = Notifications.ToList();
            Notifications.Clear();
            foreach (var n in snapshot)
                Notifications.Add(n);

            RecalculateUnreadCount();
        }

        private void RecalculateUnreadCount()
        {
            UnreadCount = Notifications.Count(n => !n.IsRead);
            HasNoNotifications = Notifications.Count == 0;
        }

        private void LoadMockData()
        {
            Notifications.Add(new NotificationItem
            {
                Title = "Nova matrícula registada",
                Message = "João Manuel Pereira foi matriculado na turma 7ª A.",
                Timestamp = DateTime.Now.AddMinutes(-6),
                Type = NotificationType.Success,
                IsRead = false
            });

            Notifications.Add(new NotificationItem
            {
                Title = "Pagamento em atraso",
                Message = "5 alunos com propinas em atraso há mais de 15 dias.",
                Timestamp = DateTime.Now.AddHours(-2),
                Type = NotificationType.Warning,
                IsRead = false
            });

            Notifications.Add(new NotificationItem
            {
                Title = "Backup concluído",
                Message = "O backup automático da base de dados foi concluído com sucesso.",
                Timestamp = DateTime.Now.AddHours(-5),
                Type = NotificationType.Info,
                IsRead = true
            });

            Notifications.Add(new NotificationItem
            {
                Title = "Falha ao gerar relatório",
                Message = "Não foi possível gerar o relatório de Fluxo de Caixa. Tenta novamente.",
                Timestamp = DateTime.Now.AddDays(-1),
                Type = NotificationType.Error,
                IsRead = true
            });
        }
    }
}
