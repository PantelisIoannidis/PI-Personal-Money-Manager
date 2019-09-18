using PIMM.Helpers;
using PIMM.Persistance;
using PIMM.ViewModels;
using PIMM.Views;
using PIMM.Views.TransactionDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.Models.ViewModels
{
    public class TransactionsViewModel : INotifyPropertyChanged
    {
        private Func<TransactionViewModel, bool> filter;
        private List<TransactionViewModel> transactions;
        private TransactionViewModel selectedTransaction;
        private readonly IPageService _pageService;
        private readonly IRepository _repository;
        private bool isRefreshing;
        Period period = (Application.Current as App).CurrentPeriod;

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectTransactionCommand { get; private set; }
        public ICommand NewTransactionCommand { get; private set; }
        public ICommand ChoosePeriodCommand { get; private set; }
        public ICommand NextTimePeriodCommand { get; private set; }
        public ICommand PreviousTimePeriodCommand { get; private set; }
        public ICommand ResetTimePeriodCommand { get; private set; }
        public ICommand DeleteActionCommand { get; private set; }
        public ICommand EditActionCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }


    public decimal Balance { get; private set; }
        public decimal IncomeSum { get; private set; }
        public decimal ExpenseSum { get; private set; }
        public decimal IncomeSumPercentage { get; private set; }
        public decimal ExpenseSumPercentage { get; private set; }

        public Period DisplayPeriod
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
                OnPropertyChanged(nameof(DisplayPeriod));
            }
        }


        public List<TransactionViewModel> Transactions
        {
            get {
                return transactions.Where(filter).ToList(); }
            set {
                transactions = value;
                CalculateSums();
                OnPropertyChanged(nameof(Transactions)); }
        }

        private void CalculateSums()
        {
            IncomeSum = transactions
                .Where(c=>c.Type==TransactionType.Income)
                .Sum(x => x.Amount);
            ExpenseSum = transactions
                .Where(c => c.Type == TransactionType.Expense)
                .Sum(x => x.Amount);
            var tempSum = IncomeSum + ExpenseSum;
            IncomeSumPercentage = tempSum != 0 ? (IncomeSum / tempSum) : 0;
            ExpenseSumPercentage = tempSum != 0 ? (ExpenseSum / tempSum) : 0;

            Balance = IncomeSum - ExpenseSum;
            Balance += transactions
                .Where(c => (c.Type == TransactionType.Adjustment && c.Amount>0) || (c.Type == TransactionType.Transfer && c.Amount > 0))
                .Sum(x => x.Amount);
            Balance += transactions
                .Where(c => (c.Type == TransactionType.Adjustment && c.Amount < 0) || (c.Type == TransactionType.Transfer && c.Amount < 0))
                .Sum(x => x.Amount);
            OnPropertyChanged(nameof(IncomeSum));
            OnPropertyChanged(nameof(ExpenseSum));
            OnPropertyChanged(nameof(IncomeSumPercentage));
            OnPropertyChanged(nameof(ExpenseSumPercentage));
            OnPropertyChanged(nameof(Balance));
        }

        public TransactionViewModel SelectedTransaction
        {
            get {
                return selectedTransaction; }
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

        

        public TransactionsViewModel(List<TransactionViewModel> transactions, IPageService pageService,IRepository repository,IPeriod period)
        {
            this.transactions = transactions;
            this.filter = (x) => { return true; };
            _pageService = pageService;
            this._repository = repository;
            RefreshCommand = new Command(CmdRefresh);
            SelectTransactionCommand = new Command<TransactionViewModel>(async vm => await SelectTransaction(vm));
            NewTransactionCommand = new Command(async x => await NewTransaction());
            ChoosePeriodCommand = new Command<TransactionViewModel>(async vm => await ChoosePeriod());
            NextTimePeriodCommand = new Command(NextTimePeriod);
            PreviousTimePeriodCommand = new Command(PreviousTimePeriod);
            ResetTimePeriodCommand = new Command(ResetTimePeriod);
            DeleteActionCommand = new Command<TransactionViewModel>(async vm => await DeleteAction(vm));
            EditActionCommand = new Command<TransactionViewModel>(async vm => await EditAction(vm));
            SearchCommand = new Command<string>(s => Search(s));

            CalculateSums();

            DisplayPeriod = (Period)period; ;
        }

        private void Search(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                this.filter = (x) => { return true; };
            } else
            {
                this.filter = (x) => { return x.Description.Contains(s); };
            }
            OnPropertyChanged(nameof(Transactions));
        }

        private async Task NewTransaction()
        {
            var vm = new TransactionViewModel()
            {
                Type = TransactionType.Expense,
                TransactionDate = DateTime.Now,
            };
            await _pageService.PushAsync(new TransactionDetailsPage(_pageService, _repository, vm));
        }

        private async Task EditAction(TransactionViewModel vm)
        {
            await _pageService.PushAsync(new TransactionDetailsPage(_pageService, _repository, vm));
        }

        private async Task DeleteAction(TransactionViewModel vm)
        {
            var deleteConfirmation = await _pageService.DisplayAlert("Delete transaction", "Are you sure?", "Yes", "No");
            var transaction = new Mapping().TransactionViewModel_2_Transaction(vm);
            if (deleteConfirmation)
            {
                _repository.DeleteTransaction(transaction);
                MessagingCenter.Send(this, "DeleteTransactions");
            }
                
        }

        private void ResetTimePeriod(object obj)
        {
            DisplayPeriod.ResetSelectedDate(DateTime.Now);
            Transactions = _repository.GetTransactions(DisplayPeriod);
        }

        private void PreviousTimePeriod()
        {
            DisplayPeriod.MoveToPrevious();
            Transactions = _repository.GetTransactions(DisplayPeriod);
        }

        private void NextTimePeriod()
        {
            DisplayPeriod.MoveToNext();
            Transactions = _repository.GetTransactions(DisplayPeriod);
        }

        private async Task ChoosePeriod()
        {
            var response = await _pageService.DisplayActionSheet("Choose Time Period", "Cancel", null,
               "Day", "Week", "Month","Year","All");

            switch (response)
            {
                case "Day":
                    DisplayPeriod.ChooseNewPeriod(PeriodType.Day);
                    break;
                case "Week":
                    DisplayPeriod.ChooseNewPeriod(PeriodType.Week);
                    break;
                case "Month":
                    DisplayPeriod.ChooseNewPeriod(PeriodType.Month);
                    break;
                case "Year":
                    DisplayPeriod.ChooseNewPeriod(PeriodType.Year);
                    break;
                case "All":
                    DisplayPeriod.ChooseNewPeriod(PeriodType.All);
                    break;
            }
            Transactions = _repository.GetTransactions(DisplayPeriod);
        }

        private async Task SelectTransaction(TransactionViewModel transaction)
        {
            SelectedTransaction = null;
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
