using PIMM.Helpers;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        List<TransactionDto> trans;
        NavigationBarViewModel navigationBarViewModel;
        TransactionsViewModel transactionsViewModel;
        ChartsViewModel charts;
        Repository repository;
        PageService pageService;
        Period period;
        public MainPage()
        {
            InitializeComponent();

            // Create and Seed Database if there is none.
            InitializeDatabase();

            repository = new Repository();
            period = new Period();
            pageService = new PageService();
            period.Init(DateTime.Now, PeriodType.Month);
            trans = repository.GetTransactions(period);
            navigationBarViewModel = new NavigationBarViewModel(trans, pageService, repository, period);
            charts = new ChartsViewModel(navigationBarViewModel, pageService, repository);
            transactionsViewModel = new TransactionsViewModel(navigationBarViewModel, pageService, repository, period);

            overview.BindingContext = charts;
            transactions.BindingContext = transactionsViewModel;

            MessagingCenter.Subscribe<NavigationBarViewModel>(this, "UpdatePeriod", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsDetailsViewModel>(this, "UpdateTransactions", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsViewModel>(this, "DeleteTransactions", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsViewModel>(this, "RefreshTransactions", RefreshTransactions);
        }



        private async Task InitializeDatabase()
        {
            var createDatabase = new InitializeDatabase();
            if (createDatabase.IsAnEmptyDatabase())
            {
                var response = await DisplayAlert("New Database", "The database is empty. Do you want some sample data?", "Yes", "No");
                if (response) { 
                    createDatabase.InsertSampleData();
                    RefreshTransactions();        
                }
            }
        }

        private void RefreshTransactions(NavigationBarViewModel obj)
        {
            RefreshTransactions();
        }

        private void RefreshTransactions(TransactionsViewModel obj)
        {
            RefreshTransactions();
        }

        private void RefreshTransactions(TransactionsDetailsViewModel obj)
        {
            RefreshTransactions();
        }

        private void RefreshTransactions()
        {
            var transactions = repository.GetTransactions(period);
            navigationBarViewModel.Transactions = transactions;
            MessagingCenter.Send(this, "UpdateCharts");
        }
    }
}
