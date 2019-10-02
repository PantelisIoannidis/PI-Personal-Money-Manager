using System;
using System.Globalization;
using Xamarin.Forms;

namespace PIMM.Views.Converters
{
    public class AmountToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Transparent;

            if (!(value is decimal))
                return Color.Transparent;

            decimal amount = (decimal)value;

            if (amount < 0)
                return (Color)Application.Current.Resources["amountNegativeColor"];

            if (amount > 0)
                return (Color)Application.Current.Resources["amountPositiveColor"];

            return (Color)Application.Current.Resources["amountZeroColor"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}