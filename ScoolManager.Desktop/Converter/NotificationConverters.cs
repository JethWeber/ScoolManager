using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ScoolManager.Desktop.Models;

namespace ScoolManager.Desktop.Converters
{
    /// <summary>
    /// NotificationType -> Brush (cor de destaque do cartão/indicador).
    /// Mantemos isto a cores simples (sem MaterialIconKind) para não
    /// depender de nomes de ícone que podem não existir no pacote
    /// instalado e voltar a rebentar o build (já aconteceu uma vez).
    /// </summary>
    public class NotificationTypeToBrushConverter : IValueConverter
    {
        private static readonly SolidColorBrush InfoBrush    = new(Color.Parse("#60A5FA")); // azul
        private static readonly SolidColorBrush SuccessBrush = new(Color.Parse("#4ADE80")); // verde
        private static readonly SolidColorBrush WarningBrush = new(Color.Parse("#FBBF24")); // âmbar
        private static readonly SolidColorBrush ErrorBrush   = new(Color.Parse("#F87171")); // vermelho

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                NotificationType.Success => SuccessBrush,
                NotificationType.Warning => WarningBrush,
                NotificationType.Error => ErrorBrush,
                _ => InfoBrush
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }

    /// <summary>
    /// bool (IsRead) -> Opacity do texto/cartão. Notificações já lidas
    /// ficam ligeiramente esbatidas.
    /// </summary>
    public class IsReadToOpacityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is true ? 0.55 : 1.0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }

    /// <summary>
    /// bool (IsRead) -> Visibility do "pontinho" indicador de não-lida.
    /// </summary>
    public class IsUnreadToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is false; // não lida => mostra o pontinho
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }

    /// <summary>
    /// int (NumeroNotificacoes) -> bool. Usado para esconder o badge
    /// do sino do Dashboard quando a contagem é 0.
    /// </summary>
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is int count && count > 0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
