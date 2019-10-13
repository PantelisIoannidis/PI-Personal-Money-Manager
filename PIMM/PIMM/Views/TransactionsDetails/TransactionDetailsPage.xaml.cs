using System;
using AutoMapper;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.TransactionDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailsPage : ContentPage
    {
        private readonly IPageService pageService;
        private readonly IRepository repository;
        private UpdateTransactionDto newTransVM;
        private readonly TransactionsDetailsViewModel transDetailVM;

        public TransactionDetailsPage(IPageService pageService, IRepository repository, TransactionDto transaction)
        {
            InitializeComponent();

            this.pageService = pageService;
            this.repository = repository;

            transDetailVM = new TransactionsDetailsViewModel(pageService, repository, transaction);
            BindingContext = transDetailVM;
        }

        public TransactionsDetailsViewModel ViewModel
        {
            get { return BindingContext as TransactionsDetailsViewModel; }
            set { BindingContext = value; }
        }
    }
}