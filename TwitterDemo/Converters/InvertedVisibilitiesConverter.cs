using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DelayedSender.Converters
{
    class InvertedVisibilitiesConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var src = (Visibility) value;
            if (src == Visibility.Visible)
                return Visibility.Collapsed;
            else if (src == Visibility.Collapsed)
                return Visibility.Visible;
            else if (src == Visibility.Hidden)
                return Visibility.Visible;
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var src = (Visibility)value;
            if (src == Visibility.Visible)
                return Visibility.Collapsed;
            else if (src == Visibility.Collapsed)
                return Visibility.Visible;
            else if (src == Visibility.Hidden)
                return Visibility.Visible;
            return null;
        }
    }
}
