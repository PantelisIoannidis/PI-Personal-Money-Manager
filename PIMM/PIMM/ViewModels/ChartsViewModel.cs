using Microcharts;
using PIMM.Helpers;
using PIMM.Persistance;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

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

        public List<Microcharts.Entry> PrepareCategoriesExpenses
        {
            get
            {
                var data = repository.GetAmountByCategory(NavigationBar.DisplayPeriod)
                    .Where(x=>x.Type==Models.TransactionType.Expense)
                    .Take(8);

                var entries = new List<Microcharts.Entry>();
                foreach(var entry in data)
                {
                    entries.Add(new Microcharts.Entry((float)entry.Amount) {
                        Label=entry.Description,
                        ValueLabel= entry.Amount.FormatAmount(),
                        Color= SKColor.Parse(entry.Color)
                    });
                }
                return entries;
            }
        }

        public List<Microcharts.Entry> PrepareAccounts
        {
            get
            {
                var data = repository.GetAmountByAccount(NavigationBar.DisplayPeriod);

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

}
