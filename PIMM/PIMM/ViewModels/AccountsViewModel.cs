using PIMM.Helpers;
using PIMM.Persistance;
using PIMM.Views.AccountsDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class AccountsViewModel : INotifyPropertyChanged
    {
        private Func<AccountViewModel, bool> filter;
        public List<AccountViewModel> accounts;
        private AccountViewModel selectedAccount;
        private readonly IPageService _pageService;
        private readonly IRepository _repository;
        private bool isRefreshing;
        private bool isSearchVisible;
        private bool isSetDateVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectAccountCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand DeleteActionCommand { get; private set; }
        public ICommand EditActionCommand { get; private set; }
        public ICommand NewTransactionCommand { get; private set; }

        public AccountsViewModel(List<AccountViewModel> accounts, IPageService pageService, IRepository repository)
        {
            this.Accounts = accounts;
            this.filter = (x) => { return true; };
            _pageService = pageService;
            this._repository = repository;
            RefreshCommand = new Command(CmdRefresh);
            SelectAccountCommand = new Command<AccountViewModel>(async vm => await SelectAccount(vm));
            DeleteActionCommand = new Command<AccountViewModel>(async vm => await DeleteAction(vm));
            EditActionCommand = new Command<AccountViewModel>(async vm => await EditAction(vm));
            NewTransactionCommand = new Command<AccountViewModel>(async vm => await NewAction());
            SearchCommand = new Command<string>(s => Search(s));
            MessagingCenter.Subscribe<AccountsDetailsPage,AccountViewModel>(this, "UpdateAccount", async (page, vm) => { await UpdateAccount(page, vm); });
        }

        private async Task NewAction()
        {
            var vm = new AccountViewModel() {
                Color = "#ffffff",
                Description="Default Account"
            };
            await _pageService.PushAsync(new AccountsDetailsPage(_pageService, _repository, vm));
        }

        private async Task UpdateAccount(AccountsDetailsPage page, AccountViewModel vm)
        {
            await _pageService.PopAsync();
            _repository.UpdateAccount(vm);
            MessagingCenter.Send(this, "RefreshAccounts");
        }

        private async Task EditAction(AccountViewModel vm)
        {
            await _pageService.PushAsync(new AccountsDetailsPage(_pageService, _repository, vm));
        }

        private async Task DeleteAction(AccountViewModel vm)
        {
            var deleteConfirmation = await _pageService.DisplayAlert("Delete account", "Are you sure?", "Yes", "No");
            var account = new Mapping().AccountViewModel_2_Account(vm);
            if (deleteConfirmation)
            {
                var status =_repository.DeleteAccount(account);
                if (status != "OK")
                {
                    await _pageService.DisplayAlert("Erase canceled", status, "OK");
                }
                else { 
                    MessagingCenter.Send(this, "DeleteAccounts");
                }
            }
        }
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        public AccountViewModel SelectedAccount
        {
            get
            {
                return selectedAccount;
            }
            set
            {
                selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }

        public List<AccountViewModel> Accounts
        {
            get
            {
                return accounts.Where(filter).ToList();
            }
            set
            {
                accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }

        private void Search(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                this.filter = (x) => { return true; };
            }
            else
            {
                this.filter = (x) => {
                    return (x.Description.ToLower().Contains(s.ToLower()));
                };
            }
            OnPropertyChanged(nameof(Accounts));
        }

        private async Task SelectAccount(AccountViewModel account)
        {
            SelectedAccount = null;
        }

        private void CmdRefresh()
        {
            IsRefreshing = true;
            MessagingCenter.Send(this, "RefreshAccounts");
            IsRefreshing = false;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
