using PIMM.Helpers;
using PIMM.Models;
using PIMM.Persistance;
using SkiaSharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PIMM.ViewModels
{
    public class ChartsViewModel : INotifyPropertyChanged
    {
        private readonly IPageService pageService;
        private readonly IRepository repository;

        public ChartsViewModel(NavigationBarViewModel navigationBarViewModel, IPageService pageService, IRepository repository)
        {
            this.NavigationBar = navigationBarViewModel;
            this.pageService = pageService;
            this.repository = repository;
        }

        private NavigationBarViewModel navigationBar;

        public NavigationBarViewModel NavigationBar
        {
            get { return navigationBar; }
            set { navigationBar = value; OnPropertyChanged(nameof(NavigationBar)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        ///  Charts
        ///
        public Microcharts.Entry[] PrepareIncomeExpense
        {
            get
            {
                var entries = new[]
                {
                    new Microcharts.Entry((float)NavigationBar.IncomeSum)
                    {
                        Label = "Income",
                        ValueLabel = (NavigationBar.IncomeSum).FormatAmount(),
                        Color = "IncomeColor".SKFromResources()
                    },
                    new Microcharts.Entry((float)NavigationBar.ExpenseSum)
                        {
                        Label = "Expense",
                        ValueLabel = (NavigationBar.ExpenseSum).FormatAmount(),
                        Color = "ExpenseColor".SKFromResources()
                    }
                };
                return entries;
            }
        }

        public List<Microcharts.Entry> PrepareCategories(TransactionType type = TransactionType.Expense)
        {
            var first8 = repository.GetAmountByCategory(NavigationBar.DisplayPeriod)
                .Where(x => x.Type == type)
                .Take(8);
            var all = repository.GetAmountByCategory(NavigationBar.DisplayPeriod)
                .Where(x => x.Type == type);

            var entries = new List<Microcharts.Entry>();
            foreach (var entry in first8)
            {
                entries.Add(new Microcharts.Entry((float)entry.Amount)
                {
                    Label = entry.Description,
                    ValueLabel = entry.Amount.FormatAmount(),
                    Color = SKColor.Parse(entry.Color)
                });
            }
            if (all.ToList().Count > 8)
            {
                var otherAmount = all.Sum(x => x.Amount) - first8.Sum(x => x.Amount);
                entries.Add(new Microcharts.Entry((float)otherAmount)
                {
                    Label = "Other",
                    ValueLabel = otherAmount.FormatAmount(),
                    Color = SKColor.Parse("#888888")
                });
            }

            return entries;
        }

        public List<Microcharts.Entry> PrepareAccounts(TransactionType type = TransactionType.Expense)
        {
            var data = repository.GetAmountByAccount(NavigationBar.DisplayPeriod)
                .Where(x => x.Type == type);

            var entries = new List<Microcharts.Entry>();
            foreach (var entry in data)
            {
                entries.Add(new Microcharts.Entry((float)entry.Amount)
                {
                    Label = entry.Description,
                    ValueLabel = entry.Amount.FormatAmount(),
                    Color = SKColor.Parse(entry.Color)
                });
            }
            return entries;
        }
    }
}