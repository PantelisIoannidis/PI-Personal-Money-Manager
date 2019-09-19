using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.Views.TransactionsDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class TransactionsDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IPageService pageService;
        private readonly IRepository repository;
        private NewEditTransactionViewModel transVM;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SaveCommand { get; set; }
        public ICommand IncomeSelectedCommand { get; set; }
        public ICommand ExpenseSelectedCommand { get; set; }
        public ICommand SelectedAccountCommand { get; set; }
        public ICommand SelectedCategoryCommand { get; set; }
        

        public NewEditTransactionViewModel TransVM { get {
                return transVM;
                }
            set {
                transVM = value;
                OnPropertyChanged(nameof(TransVM));
            } }

        public TransactionsDetailsViewModel(IPageService pageService, IRepository repository, NewEditTransactionViewModel transactionViewModel)
        {
            this.pageService = pageService;
            this.repository = repository;
            TransVM = transactionViewModel;

            SaveCommand = new Command(async x => await Save());
            IncomeSelectedCommand = new Command(IncomeSelected);
            ExpenseSelectedCommand = new Command(ExpenseSelected);
            SelectedAccountCommand = new Command(async x => await SelectedAccount());
            SelectedCategoryCommand = new Command(async x => await SelectedCategory());
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
            OnPropertyChanged(nameof(TransVM));
        }

        private void IncomeSelected()
        {
            TransVM.Type = TransactionType.Income;
            TransVM.Description = TransVM.CurrentCategory.Description;
            OnPropertyChanged(nameof(TransVM));
        }

        private async Task Save()
        {
            repository.UpdateTransaction(TransVM);
            MessagingCenter.Send(this, "UpdateTransactions");
            await pageService.PopAsync();
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
