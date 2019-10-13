using AutoMapper;
using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.Views.TransactionsDetails;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class TransactionsDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IPageService pageService;
        private readonly IRepository repository;
        private UpdateTransactionDto transVM;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SaveCommand { get; set; }
        public ICommand IncomeSelectedCommand { get; set; }
        public ICommand ExpenseSelectedCommand { get; set; }
        public ICommand SelectedAccountCommand { get; set; }
        public ICommand SelectedCategoryCommand { get; set; }

        public UpdateTransactionDto TransVM
        {
            get
            {
                return transVM;
            }
            set
            {
                transVM = value;
                OnPropertyChanged(nameof(TransVM));
            }
        }

        public TransactionsDetailsViewModel(IPageService pageService, IRepository repository, TransactionDto transactionDto)
        {
            this.pageService = pageService;
            this.repository = repository;
            CheckIfCategoriesAndAccountsExistsAsync().ContinueWith(r =>
            {
                var result = r.Result;
                if (!result)
                    return;
                prepareViewModel(transactionDto);
            }, TaskScheduler.FromCurrentSynchronizationContext());

            SaveCommand = new Command(async x => await Save());
            IncomeSelectedCommand = new Command(IncomeSelected);
            ExpenseSelectedCommand = new Command(ExpenseSelected);
            SelectedAccountCommand = new Command(async x => await SelectedAccount());
            SelectedCategoryCommand = new Command(async x => await SelectedCategory());
        }

        private void prepareViewModel(TransactionDto vm)
        {
            this.TransVM = Mapper.Map<TransactionDto, UpdateTransactionDto>(vm);
            TransVM = repository.PopulateTransactionWithConnectedLists(TransVM);
            if (TransVM.CategoryId == 0)
                TransVM.CategoryId = TransVM.CurrentCategory.Id;
            if (TransVM.AccountId <= 0)
                TransVM.AccountId = TransVM.CurrentAccount.Id;
            if (string.IsNullOrEmpty(TransVM.Description))
                TransVM.Description = TransVM.CurrentCategory.Description;
        }

        private async Task<bool> CheckIfCategoriesAndAccountsExistsAsync()
        {
            var result = false;
            if (repository.GetAllCategories().Count > 0)
                result = true;
            if (repository.GetAllAccounts().Count > 0)
                result = true;

            if (result == false)
            {
                await pageService.DisplayAlert("Can't create trancaction"
                    , "You have to create categories and accounts first.", "Ok");
                await pageService.PopAsync();
            }
            return result;
        }

        private async Task SelectedCategory()
        {
            var page = new TransactionsDetailsCategories(TransVM);
            page.CategoriesListView.ItemSelected += async (source, args) =>
            {
                TransVM.CategoryId = (args.SelectedItem as Category).Id;
                TransVM.Description = (args.SelectedItem as Category).Description;
                OnPropertyChanged(nameof(TransVM));
                await pageService.PopAsync();
            };
            await pageService.PushAsync(page);
        }

        private async Task SelectedAccount()
        {
            var page = new TransactionsDetailsAccounts(TransVM);
            page.AccountsListView.ItemSelected += async (source, args) =>
            {
                TransVM.AccountId = (args.SelectedItem as Account).Id;
                OnPropertyChanged(nameof(TransVM));
                await pageService.PopAsync();
            };
            await pageService.PushAsync(page);
        }

        private void ExpenseSelected()
        {
            TransVM.Type = TransactionType.Expense;
            TransVM.Description = TransVM.CurrentCategory.Description;
            TransVM.CategoryId = TransVM.CurrentCategory.Id;
            OnPropertyChanged(nameof(TransVM));
        }

        private void IncomeSelected()
        {
            TransVM.Type = TransactionType.Income;
            TransVM.Description = TransVM.CurrentCategory.Description;
            TransVM.CategoryId = TransVM.CurrentCategory.Id;
            OnPropertyChanged(nameof(TransVM));
        }

        private async Task Save()
        {
            repository.UpdateTransaction(TransVM);
            MessagingCenter.Send(this, MessagingString.UpdateTransactions);
            await pageService.PopAsync();
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}