using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Converters
{
    internal class AnswerFieldTypeToMultiselectEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string) value;
            return type != "radiobutton";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
