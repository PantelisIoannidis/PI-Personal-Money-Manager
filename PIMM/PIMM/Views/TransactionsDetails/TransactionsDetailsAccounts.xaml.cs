using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.TransactionsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsDetailsAccounts : ContentPage
    {
        private readonly UpdateTransactionViewModel transVM;
        public TransactionsDetailsAccounts(UpdateTransactionViewModel transVM)
        {
            InitializeComponent();
            this.transVM = transVM;
            BindingContext = transVM;
        }

        public ListView AccountsListView { get { return accountsListView; } }
    }
}