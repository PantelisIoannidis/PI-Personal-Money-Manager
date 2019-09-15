using PIMM.Helpers;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.TransactionDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailsPage : ContentPage
    {
        private readonly IPageService pageService;
        private readonly IRepository repository;
        private readonly NewEditTransactionViewModel newTransVM;
        private readonly TransactionsDetailsViewModel transDetailVM;

        public TransactionDetailsPage(IPageService pageService,IRepository repository, TransactionViewModel transaction)
        {
            InitializeComponent();
            var mapping = new Mapping();
            this.pageService = pageService;
            this.repository = repository;
            this.newTransVM = mapping.TransactionViewModel_2_NewEditTransactionViewModel(transaction);
            newTransVM = repository.PopulateTransactionWithConnectedLists(newTransVM);
            transDetailVM = new TransactionsDetailsViewModel(pageService, repository, newTransVM);
            BindingContext = transDetailVM;
        }

        public TransactionsDetailsViewModel ViewModel
        {
            get { return BindingContext as TransactionsDetailsViewModel; }
            set { BindingContext = value; }
        }
    }
}