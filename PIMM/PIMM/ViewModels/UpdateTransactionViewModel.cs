using PIMM.Helpers;
using PIMM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class UpdateTransactionDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        private TransactionType type;

        public TransactionType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        // Helpful properties for the view
        // The lists bellow are populated from repository before the object is passed in detail page
        public List<TransactionDetailsCategoryViewModel> CategoriesList { get; set; }

        public List<FontIcon> FontsList { get; set; }
        public List<Account> AccountsList { get; set; }

        // Properties for income,expense button state
        public bool IsIncome
        {
            get
            {
                var result = Type == TransactionType.Income;
                return result;
            }
        }

        public bool IsExpense
        {
            get
            {
                var result = Type == TransactionType.Expense;
                return result;
            }
        }

        public string IncomeBackgroundColor
        {
            get
            {
                return IsIncome
                    ? ((Color)App.Current.Resources["IncomeColor"]).GetHexString()
                    : ((Color)App.Current.Resources["backgroundColor"]).GetHexString();
            }
        }

        public string ExpenseBackgroundColor
        {
            get
            {
                return IsExpense
                    ? ((Color)App.Current.Resources["ExpenseColor"]).GetHexString()
                    : ((Color)App.Current.Resources["backgroundColor"]).GetHexString();
            }
        }

        public bool IsAdjustment
        {
            get
            {
                return Type == TransactionType.Adjustment;
            }
        }

        public bool IsTransfer
        {
            get
            {
                return Type == TransactionType.Transfer;
            }
        }

        //more information about selected transaction
        public TransactionDetailsCategoryViewModel CurrentCategory
        {
            get
            {
                var category = CategoryListByType.FirstOrDefault(x => x.Id == this.CategoryId);
                if (category == null)
                    category = CategoryListByType.FirstOrDefault();
                return category;
            }
        }

        public FontIcon CurrentIcon
        {
            get
            {
                return FontsList.SingleOrDefault(x => x.Id == CurrentCategory.FontIconId);
            }
        }

        public Account CurrentAccount
        {
            get
            {
                var account = AccountsList.SingleOrDefault(x => x.Id == this.AccountId);
                if (account == null)
                    account = AccountsList.FirstOrDefault();
                return account;
            }
        }

        //returns only the categories that belongs to current transaction type
        public List<TransactionDetailsCategoryViewModel> CategoryListByType
        {
            get
            {
                return CategoriesList.Where(x => x.Type == this.Type).ToList();
            }
        }

        // The title of the form
        public string FormPurposeNewOrEdit { get { return Id == 0 ? "New Transaction" : "Edit Selected Transaction"; } }

        public string FormattedAmount
        {
            get
            {
                return String.Format("{0:C}", (decimal)Amount);
            }
        }

        public string FormattedDate
        {
            get
            {
                return String.Format("{0:d}", TransactionDate);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}