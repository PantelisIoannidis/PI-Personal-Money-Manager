using PIMM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.TransactionsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsDetailsAccounts : ContentPage
    {
        private readonly UpdateTransactionDto transVM;

        public TransactionsDetailsAccounts(UpdateTransactionDto transVM)
        {
            InitializeComponent();
            this.transVM = transVM;
            BindingContext = transVM;
        }

        public ListView AccountsListView { get { return accountsListView; } }
    }
}