using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountsPage : ContentPage
    {
        List<AccountDto> accounts;
        AccountsViewModel accountsViewModel;
        Repository repository;

        public AccountsPage()
        {
            InitializeComponent();

            repository = new Repository();
            accounts = repository.GetAccountsAsViewModels();
            accountsViewModel = new AccountsViewModel(accounts, new PageService(), repository);
            MessagingCenter.Unsubscribe<AccountsViewModel>(this, "DeleteAccounts");
            MessagingCenter.Unsubscribe<AccountsViewModel>(this, "RefreshAccounts");
            MessagingCenter.Subscribe<AccountsViewModel>(this, "DeleteAccounts", RefreshAccounts);
            MessagingCenter.Subscribe<AccountsViewModel>(this, "RefreshAccounts", RefreshAccounts);
            BindingContext = accountsViewModel;
        }

        private void RefreshAccounts(AccountsViewModel obj)
        {
            accounts = repository.GetAccountsAsViewModels();
            accountsViewModel.Accounts = accounts;
        }


        private void AccountsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectAccountCommand.Execute(e.SelectedItem);
        }

        public AccountsViewModel ViewModel
        {
            get { return BindingContext as AccountsViewModel; }
            set { BindingContext = value; }
        }
    }
}