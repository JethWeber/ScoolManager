
namespace ScoolManager.Desktop.Converter;

public class SelecionadoBackgroundConverter : IValueConverter
{
    private static readonly IBrush Selecionado = new SolidColorBrush(Color.Parse("#1A4D8EFF"));
    private static readonly IBrush Normal     = new SolidColorBrush(Color.Parse("#222A3D"));

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? Selecionado : Normal;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

public class SelecionadoBorderConverter : IValueConverter
{
    private static readonly IBrush Selecionado = new SolidColorBrush(Color.Parse("#4D8EFF"));
    private static readonly IBrush Normal     = new SolidColorBrush(Color.Parse("#424754"));

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? Selecionado : Normal;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}