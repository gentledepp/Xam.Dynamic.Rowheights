using System;
using System.Globalization;
using Xam.Dynamic.Rowheights.Controls;
using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Converters
{
    internal class AnswerFieldTypeToCheckboxVariantConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string) value;
            switch (type)
            {
                case "radiobutton":
                    return CheckboxVariant.Radio;
                case "checkbox":
                    return CheckboxVariant.Box;
            }
            return CheckboxVariant.Custom;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
