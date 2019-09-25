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
            repository = new Repository();
            period = new Period();
            pageService = new PageService();
            period.Init(DateTime.Now, PeriodType.Month);
            trans = repository.GetTransactions(period);
            navigationBarViewModel = new NavigationBarViewModel(trans, pageService, repository, period);
            charts = new ChartsViewModel(navigationBarViewModel, pageService, repository);
            transactionsViewModel = new TransactionsViewModel(navigationBarViewModel, pageService, repository,period);

            overview.BindingContext = charts;
            transactions.BindingContext = transactionsViewModel;

            MessagingCenter.Subscribe<TransactionsDetailsViewModel>(this, "UpdateTransactions", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsViewModel>(this, "DeleteTransactions", RefreshTransactions);
            MessagingCenter.Subscribe<TransactionsViewModel>(this, "RefreshTransactions", RefreshTransactions);
        }

        private void RefreshTransactions(TransactionsViewModel obj)
        {
            var transactions = repository.GetTransactions(period);
            navigationBarViewModel.Transactions = transactions;
        }

        private void RefreshTransactions(TransactionsDetailsViewModel obj)
        {
            var transactions = repository.GetTransactions(period);
            navigationBarViewModel.Transactions = transactions;
        }
    }
}
