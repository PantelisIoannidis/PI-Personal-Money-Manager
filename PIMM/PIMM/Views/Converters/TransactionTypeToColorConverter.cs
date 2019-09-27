using PIMM.Models;
using PIMM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PIMM.Views.Converters
{
    public class TransactionTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Transparent;

            Color color = new Color();

            var type = (value as TransactionDto).Type;


            switch (type)
            {
                case TransactionType.Income:
                    color = (Color)Application.Current.Resources["IncomeColor"];
                    break;
                case TransactionType.Expense:
                    color = (Color)Application.Current.Resources["ExpenseColor"];
                    break;
                case TransactionType.Transfer:
                    color = (Color)Application.Current.Resources["TransferColor"];
                    break;
                case TransactionType.Adjustment:
                    color = (Color)Application.Current.Resources["AdjustmentColor"];
                    break;
                default:
                    color = (Color)Application.Current.Resources["textColor"]; 
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
