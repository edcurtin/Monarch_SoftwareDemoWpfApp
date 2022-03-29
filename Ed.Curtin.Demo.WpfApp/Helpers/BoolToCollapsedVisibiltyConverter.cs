using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ed.Curtin.Demo.WpfApp.Helpers
{
    internal class BoolToCollapsedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return Visibility.Collapsed;
            var visibility = (bool)value;
            return visibility ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
