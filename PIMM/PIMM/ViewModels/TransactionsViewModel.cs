using AutoMapper;
using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.Views.TransactionDetails;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class TransactionsViewModel : INotifyPropertyChanged
    {
        private Func<TransactionDto, bool> filter;
        private TransactionDto selectedTransaction;
        private bool isRefreshing;
        private readonly IPageService pageService;
        private readonly IRepository repository;
        private Period period = (Application.Current as App).CurrentPeriod;

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectTransactionCommand { get; set; }
        public ICommand NewTransactionCommand { get; set; }
        public ICommand DeleteActionCommand { get; set; }
        public ICommand EditActionCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public TransactionDto SelectedTransaction
        {
            get
            {
                return selectedTransaction;
            }
            set
            {
                selectedTransaction = value;
                OnPropertyChanged(nameof(SelectedTransaction));
            }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        private NavigationBarViewModel navigationBar;

        public NavigationBarViewModel NavigationBar
        {
            get { return navigationBar; }
            set { navigationBar = value; OnPropertyChanged(nameof(NavigationBar)); }
        }

        public TransactionsViewModel(NavigationBarViewModel navigationBarViewModel, IPageService pageService, IRepository repository, IPeriod period)
        {
            NavigationBar = navigationBarViewModel;
            this.filter = (x) => { return true; };
            this.pageService = pageService;
            this.repository = repository;
            RefreshCommand = new Command(CmdRefresh);
            SelectTransactionCommand = new Command<TransactionDto>(async vm => await SelectTransaction(vm));
            NewTransactionCommand = new Command(async vm => await NewTransaction());

            DeleteActionCommand = new Command<TransactionDto>(async vm => await DeleteAction(vm));
            EditActionCommand = new Command<TransactionDto>(async vm => await EditAction(vm));
        }

        private async Task NewTransaction()
        {
            var vm = new TransactionDto()
            {
                Type = TransactionType.Expense,
                TransactionDate = DateTime.Now,
            };
            await pageService.PushAsync(new TransactionDetailsPage(pageService, repository, vm));
        }

        private async Task EditAction(TransactionDto vm)
        {
            await pageService.PushAsync(new TransactionDetailsPage(pageService, repository, vm));
        }

        private async Task DeleteAction(TransactionDto vm)
        {
            var deleteConfirmation = await pageService.DisplayAlert("Delete transaction", "Are you sure?", "Yes", "No");
            var transactions = Mapper.Map<TransactionDto, Transaction>(vm);
            if (deleteConfirmation)
            {
                repository.DeleteTransaction(transactions);
                MessagingCenter.Send(this, MessagingString.DeleteTransactions);
            }
        }

        private async Task SelectTransaction(TransactionDto transaction)
        {
            SelectedTransaction = null;
        }

        private void CmdRefresh()
        {
            IsRefreshing = true;
            MessagingCenter.Send(this, MessagingString.RefreshTransactions);
            IsRefreshing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}