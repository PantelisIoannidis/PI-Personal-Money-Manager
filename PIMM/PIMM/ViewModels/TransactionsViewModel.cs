using PIMM.ViewModels;
using PIMM.ViewModels;
using PIMM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.Models.ViewModels
{
    public class TransactionsViewModel : INotifyPropertyChanged
    {
        private List<TransactionViewModel> transactions;
        private TransactionViewModel selectedTransaction;
        private readonly IPageService _pageService;
        private bool isRefreshing;

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectTransactionCommand { get; private set; }
        public ICommand NewTransactionCommand { get; private set; }
        public ICommand MoneyTransferCommand { get; private set; }
        public ICommand AdjustmentCommand { get; private set; }

        public List<TransactionViewModel> Transactions
        {
            get { return transactions; }
            set { transactions = value; OnPropertyChanged(nameof(Transactions)); }
        }

        public TransactionViewModel SelectedTransaction
        {
            get { return selectedTransaction; }
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

        

        public TransactionsViewModel(List<TransactionViewModel> transactions, IPageService pageService)
        {
            this.transactions = transactions;
            _pageService = pageService;
            RefreshCommand = new Command(CmdRefresh);
            SelectTransactionCommand = new Command<TransactionViewModel>(async vm => await SelectTransaction(vm));
            NewTransactionCommand = new Command(NewTransaction);
            MoneyTransferCommand = new Command(MoneyTransfer);
            AdjustmentCommand = new Command(Adjustment);
        }

        private void Adjustment()
        {
            
        }

        private void MoneyTransfer()
        {
            
        }

        private void NewTransaction()
        {
            
        }

        private async Task SelectTransaction(TransactionViewModel transaction)
        {
            if (transaction == null)
                return;
            SelectedTransaction = null;

            var response = await _pageService.DisplayActionSheet("What do you want to do?", "Cancel", null, 
                "Details","Delete","Edit");

            switch (response)
            {
                case "Delete":
                    var deleteConfirmation = await _pageService.DisplayAlert("Delete transaction", "Are you sure?", "Yes", "No");
                    //if(deleteConfirmation)
                    break;
                case "Edit":
                    await _pageService.PushAsync(new TransactionDetailsPage(transaction));
                    break;
                default:
                    break;
            }
        }

        private async void CmdRefresh()
        {
            IsRefreshing = true;
            await Task.Delay(3000);
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
