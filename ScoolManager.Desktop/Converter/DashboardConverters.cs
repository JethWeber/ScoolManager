using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Material.Icons;

namespace ScoolManager.Desktop.Converters;

/// <summary>
/// Converte um bool "TrendIsPositive" na cor correta (verde para subida, vermelho para queda).
/// </summary>
public class TrendToBrushConverter : IValueConverter
{
    public static readonly TrendToBrushConverter Instance = new();

    private static readonly IBrush PositiveBrush = new SolidColorBrush(Color.Parse("#34D399")); // emerald-400
    private static readonly IBrush NegativeBrush = new SolidColorBrush(Color.Parse("#FFB4AB")); // error

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? PositiveBrush : NegativeBrush;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

/// <summary>
/// Converte um bool "TrendIsPositive" no ícone de seta correspondente.
/// </summary>
public class TrendToIconConverter : IValueConverter
{
    public static readonly TrendToIconConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? MaterialIconKind.TrendingUp : MaterialIconKind.TrendingDown;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

/// <summary>
/// Converte um bool "IsAlert" na cor do ícone principal do cartão KPI
/// (vermelho/erro quando true, azul primário quando false).
/// </summary>
public class AlertToBrushConverter : IValueConverter
{
    public static readonly AlertToBrushConverter Instance = new();

    private static readonly IBrush AlertBrush = new SolidColorBrush(Color.Parse("#FFB4AB"));   // error
    private static readonly IBrush NormalBrush = new SolidColorBrush(Color.Parse("#ADC6FF"));  // primary

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? AlertBrush : NormalBrush;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
