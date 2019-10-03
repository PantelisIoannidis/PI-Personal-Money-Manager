using PIMM.Helpers;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PIMM
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPageViewModel MainVM { set; get; }

        private readonly List<TransactionDto> trans;
        private NavigationBarViewModel navigationBarViewModel;
        private TransactionsViewModel transactionsViewModel;
        private ChartsViewModel charts;
        private Repository repository;
        private PageService pageService;
        private InitializeDatabase initializeDatabase;
        private Period period;

        public MainPage()
        {
            var themeLoader = new ThemeLoader();
            themeLoader.LoadTheme();

            InitializeComponent();

            repository = new Repository();
            period = new Period();
            pageService = new PageService();
            initializeDatabase = new InitializeDatabase(pageService);
            period.Init(DateTime.Now, PeriodType.Month);
            trans = repository.GetTransactions(period);
            navigationBarViewModel = new NavigationBarViewModel(trans, pageService, repository, period);
            charts = new ChartsViewModel(navigationBarViewModel, pageService, repository);
            transactionsViewModel = new TransactionsViewModel(navigationBarViewModel, pageService, repository, period);

            MainVM = new MainPageViewModel(pageService, repository, initializeDatabase);

            this.BindingContext = MainVM;
            overview.BindingContext = charts;
            transactions.BindingContext = transactionsViewModel;

            MessagingCenter.Subscribe<NavigationBarViewModel>(this, "UpdatePeriod", RefreshTransactions);
            MessagingCenter.Subscribe<SettingsPage>(this, "UpdateTransactionsAfterSettingsChange", RefreshTransactions);
            MessagingCenter.Subscribe<SettingsViewModel>(this, "UpdateTransactionsAfterReset", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsDetailsViewModel>(this, "UpdateTransactions", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsViewModel>(this, "DeleteTransactions", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsViewModel>(this, "RefreshTransactions", RefreshTransactions);
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            string currentPageName = CurrentPage.ClassId;
            //CurrentPage.Title = currentPageName;
            this.Title = currentPageName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Create and Seed Database if there is none.
            InitializeDatabase();
        }

        private async Task InitializeDatabase()
        {
            var createDatabase = new InitializeDatabase(pageService);
            await createDatabase.PopulateDatabase();
            RefreshTransactions();
        }

        private void RefreshTransactions(SettingsViewModel obj) => RefreshTransactions();

        private void RefreshTransactions(SettingsPage obj) => RefreshTransactions();

        private void RefreshTransactions(NavigationBarViewModel obj) => RefreshTransactions();

        private void RefreshTransactions(TransactionsViewModel obj) => RefreshTransactions();

        private void RefreshTransactions(TransactionsDetailsViewModel obj) => RefreshTransactions();

        private void RefreshTransactions()
        {
            var transactions = repository.GetTransactions(period);
            navigationBarViewModel.Transactions = transactions;
            MessagingCenter.Send(this, "UpdateCharts");
        }

        public MainPageViewModel ViewModel
        {
            get { return BindingContext as MainPageViewModel; }
            set { BindingContext = value; }
        }
    }
}