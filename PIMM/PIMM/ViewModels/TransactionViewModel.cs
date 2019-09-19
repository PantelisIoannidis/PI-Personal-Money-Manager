using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PIMM.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        public string CategoryDescription { get; set; }
        public string FontFamily { get; set; }
        public string Glyph { get; set; }
        public string GlyphColor { get; set; }

        public string AccountColor { get; set; }

        public string FormattedAmount
        { get
            {
                return String.Format("{0:C}", (decimal)Amount);
            } }

        public Color FormattedColorAmount
        {
            get
            {
                switch (Type)
                {
                    case TransactionType.Income:
                        return (Color)Application.Current.Resources["IncomeColor"];
                    case TransactionType.Expense:
                        return (Color)Application.Current.Resources["ExpenseColor"];
                    case TransactionType.Transfer:
                        return (Color)Application.Current.Resources["TransferColor"];
                    case TransactionType.Adjustment:
                        return (Color)Application.Current.Resources["AdjustmentColor"];
                    default:
                        return Color.Black;

                }
            }
        }

        public string FormattedDate { get
            {
                return String.Format("{0:d}", TransactionDate);
            } }

       

    }
}
