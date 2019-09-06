using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
                return Color.LightSalmon;

            if (amount > 0)
                return Color.LightGreen;

            return Color.Transparent;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
