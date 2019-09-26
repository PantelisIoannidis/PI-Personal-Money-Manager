using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
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
    public class NavigationBarViewModel : INotifyPropertyChanged
    {
        private Func<TransactionDto, bool> filter;
        private List<TransactionDto> transactions;
         private readonly IPageService _pageService;
        private readonly IRepository _repository;
        Period period = (Application.Current as App).CurrentPeriod;
        private bool isSearchVisible;
        private bool isSetDateVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ChoosePeriodCommand { get; private set; }
        public ICommand NextTimePeriodCommand { get; private set; }
        public ICommand PreviousTimePeriodCommand { get; private set; }
        public ICommand ResetTimePeriodCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand ShowSearchFieldCommand { get; private set; }
        public ICommand ShowSetDateCommand { get; private set; }
        public ICommand SetDateCommand { get; private set; }

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
            set { period = value; OnPropertyChanged(nameof(DisplayPeriod)); }
        }


        public bool IsSetDateVisible
        {
            get { return isSetDateVisible; }
            set { isSetDateVisible = value; OnPropertyChanged(nameof(IsSetDateVisible)); }
        }

        public bool IsSearchVisible
        {
            get { return isSearchVisible; }
            set
            {
                isSearchVisible = value;
                OnPropertyChanged(nameof(IsSearchVisible));
            }
        }


        public List<TransactionDto> Transactions
        {
            get
            {
                return transactions.Where(filter).ToList();
            }
            set
            {
                transactions = value;
                CalculateSums();
                OnPropertyChanged(nameof(Transactions));
            }
        }

        private void CalculateSums()
        {
            IncomeSum = transactions
                .Where(c => c.Type == TransactionType.Income)
                .Sum(x => x.Amount);
            ExpenseSum = transactions
                .Where(c => c.Type == TransactionType.Expense)
                .Sum(x => x.Amount);
            var tempSum = IncomeSum + ExpenseSum;
            IncomeSumPercentage = tempSum != 0 ? (IncomeSum / tempSum) : 0;
            ExpenseSumPercentage = tempSum != 0 ? (ExpenseSum / tempSum) : 0;

            Balance = IncomeSum - ExpenseSum;
            Balance += transactions
                .Where(c => (c.Type == TransactionType.Adjustment && c.Amount > 0) || (c.Type == TransactionType.Transfer && c.Amount > 0))
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

        public NavigationBarViewModel(List<TransactionDto> transactions, IPageService pageService, IRepository repository, IPeriod period)
        {
            this.Transactions = transactions;
            this.filter = (x) => { return true; };
            _pageService = pageService;
            this._repository = repository;
            ChoosePeriodCommand = new Command(async vm => await ChoosePeriod());
            NextTimePeriodCommand = new Command(NextTimePeriod);
            PreviousTimePeriodCommand = new Command(PreviousTimePeriod);
            ResetTimePeriodCommand = new Command(ResetTimePeriod);
            SearchCommand = new Command<string>(s => Search(s));
            ShowSearchFieldCommand = new Command(ShowSearchField);
            ShowSetDateCommand = new Command(ShowSetDate);
            SetDateCommand = new Command(async s => await SetDate(s));

            CalculateSums();

            DisplayPeriod = (Period)period; ;
        }

        private async Task SetDate(object newDate)
        {
            DisplayPeriod.ResetSelectedDate((DateTime)newDate);
            MessagingCenter.Send(this, "UpdatePeriod");
        }

        private void ShowSetDate()
        {
            IsSetDateVisible = !IsSetDateVisible;
        }

        private void ShowSearchField()
        {
            IsSearchVisible = !IsSearchVisible;
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
                    return ((x.Description.ToLower().Contains(s.ToLower()))
                    || (x.CategoryDescription.ToLower().Contains(s.ToLower())));
                };
            }
            OnPropertyChanged(nameof(Transactions));
        }


        private void ResetTimePeriod(object obj)
        {
            DisplayPeriod.ResetSelectedDate(DateTime.Now);
            MessagingCenter.Send(this, "UpdatePeriod");
        }

        private void PreviousTimePeriod()
        {
            DisplayPeriod.MoveToPrevious();
            MessagingCenter.Send(this, "UpdatePeriod");
        }

        private void NextTimePeriod()
        {
            DisplayPeriod.MoveToNext();
            MessagingCenter.Send(this, "UpdatePeriod");
        }

        private async Task ChoosePeriod()
        {
            var response = await _pageService.DisplayActionSheet("Choose Time Period", "Cancel", null,
               "Day", "Week", "Month", "Year", "All");

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
            MessagingCenter.Send(this, "UpdatePeriod");
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
