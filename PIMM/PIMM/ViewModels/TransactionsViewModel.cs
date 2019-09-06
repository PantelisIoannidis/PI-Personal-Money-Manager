using PIMM.Model.ViewModels;
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
        private TransactionViewModel selectedItem;
        private readonly IPageService _pageService;
        private bool isRefreshing;

        public List<TransactionViewModel> Transactions
        {
            get { return transactions; }
            set { transactions = value; OnPropertyChanged(nameof(Transactions)); }
        }

        public TransactionViewModel SelectedTransaction
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
            }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectTransactionCommand { get; private set; }

        public TransactionsViewModel(List<TransactionViewModel> transactions, IPageService pageService)
        {
            this.transactions = transactions;
            _pageService = pageService;
            RefreshCommand = new Command(CmdRefresh);
            SelectTransactionCommand = new Command<TransactionViewModel>(async vm => await SelectTransaction(vm));
        }

        private async Task SelectTransaction(TransactionViewModel transaction)
        {
            if (transaction == null)
                return;
            SelectedTransaction = null;
            await _pageService.PushAsync(new TransactionDetailsPage(transaction));
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
