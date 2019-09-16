using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PIMM.Views.Converters
{
    public class AmountFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return String.Format("{0:C}", (decimal)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            decimal amount = 0m;
            NumberStyles style = NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;
            CultureInfo culrure = CultureInfo.CurrentCulture;
            decimal.TryParse((string)value, style,culrure, out amount);

            return amount;
        }
    }
}
