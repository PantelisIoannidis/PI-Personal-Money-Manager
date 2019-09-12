﻿using PIMM.Helpers;
using PIMM.Persistance;
using PIMM.ViewModels;
using PIMM.Views;
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
                return transactions; }
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
            IncomeSumPercentage = ((IncomeSum) / (IncomeSum + ExpenseSum));
            ExpenseSumPercentage = ((ExpenseSum) / (IncomeSum + ExpenseSum));

            Balance = IncomeSum - ExpenseSum;
            Balance += transactions
                .Where(c => (c.Type == TransactionType.Adjustment && c.Amount>0) || (c.Type == TransactionType.Transfer && c.Amount > 0))
                .Sum(x => x.Amount);
            Balance -= transactions
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
            _pageService = pageService;
            this._repository = repository;
            RefreshCommand = new Command(CmdRefresh);
            SelectTransactionCommand = new Command<TransactionViewModel>(async vm => await SelectTransaction(vm));
            NewTransactionCommand = new Command(NewTransaction);
            ChoosePeriodCommand = new Command<TransactionViewModel>(async vm => await ChoosePeriod());
            NextTimePeriodCommand = new Command(NextTimePeriod);
            PreviousTimePeriodCommand = new Command(PreviousTimePeriod);
            ResetTimePeriodCommand = new Command(ResetTimePeriod);

            CalculateSums();

            DisplayPeriod = (Period)period; ;
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
