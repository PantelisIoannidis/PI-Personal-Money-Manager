using PIMM.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PIMM.Models.ViewModels
{
    public class TransactionsViewModel2 : INotifyPropertyChanged
    {
        #region fields
        private List<TransactionViewModel> transactions;
        private TransactionViewModel selectedItem;
        private bool isRefreshing;
        #endregion

        #region Properties
        public List<TransactionViewModel> Transactions
        {
            get { return transactions; }
            set { transactions = value; OnPropertyChanged(nameof(Transactions)); }
        }

        public TransactionViewModel SelectedTeam
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
        #endregion

        //public TransactionsViewModel()
        //{

        //    //RefreshCommand = new Command(CmdRefresh);
        //}

        private async void CmdRefresh()
        {
            IsRefreshing = true;
            // wait 3 secs for demo
            await Task.Delay(3000);
            IsRefreshing = false;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
