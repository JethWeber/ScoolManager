using System;

namespace ScoolManager.Desktop.Models
{
    /// <summary>
    /// Tipo de notificação — controla a cor e o ícone exibidos no painel.
    /// </summary>
    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Error
    }

    /// <summary>
    /// Representa uma notificação individual mostrada no painel
    /// "sino" do Dashboard (NotificationsPanel).
    /// </summary>
    public class NotificationItem
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public NotificationType Type { get; set; } = NotificationType.Info;
        public bool IsRead { get; set; }

        /// <summary>
        /// Texto relativo tipo "há 5 min" / "há 2 h" / "há 3 d",
        /// calculado a partir do Timestamp.
        /// </summary>
        public string TimeAgo => FormatTimeAgo(Timestamp);

        private static string FormatTimeAgo(DateTime timestamp)
        {
            var diff = DateTime.Now - timestamp;

            if (diff.TotalSeconds < 60) return "agora";
            if (diff.TotalMinutes < 60) return $"há {(int)diff.TotalMinutes} min";
            if (diff.TotalHours < 24) return $"há {(int)diff.TotalHours} h";
            if (diff.TotalDays < 7) return $"há {(int)diff.TotalDays} d";

            return timestamp.ToString("dd/MM/yyyy");
        }
    }
}
