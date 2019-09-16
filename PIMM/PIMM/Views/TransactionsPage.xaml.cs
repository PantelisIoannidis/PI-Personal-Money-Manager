using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.ViewModels;
using PIMM.Views.TransactionDetails;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        private double width = 0;
        private double height = 0;

        List<TransactionViewModel> transactions;
        TransactionsViewModel transactionsViewModel;
        Repository repository;
        Period period;
        public TransactionsPage()
        {
            InitializeComponent();

            repository = new Repository();
            period = new Period();
            period.Init(DateTime.Now, PeriodType.Month);
            transactions = repository.GetTransactions(period);
            transactionsViewModel = new TransactionsViewModel(transactions, new PageService(), repository, period);

            MessagingCenter.Subscribe<TransactionsDetailsViewModel>(this, "NewEditTransactions", OnNewEditTransactions);
            MessagingCenter.Subscribe<TransactionsViewModel>(this, "DeleteTransactions", OnDeleteTransactions);
            BindingContext = transactionsViewModel;

        }

        private void OnDeleteTransactions(TransactionsViewModel obj)
        {
            transactions = repository.GetTransactions(period);
            transactionsViewModel.Transactions = transactions;
        }

        private void OnNewEditTransactions(TransactionsDetailsViewModel obj)
        {
            transactions = repository.GetTransactions(period);
            transactionsViewModel.Transactions = transactions;
        }


        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);

        //    if (this.width != width || this.height != height)
        //    {
        //        this.width = width;
        //        this.height = height;

        //        UpdateLayout();
        //    }
        //}

        //private void UpdateLayout()
        //{
        //    if (width > height)
        //    {
        //        Content = PortraitView;
        //    }
        //    else
        //    {
        //        Content = PortraitView;
        //    }
        //}

        private void Listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)

        {
            ViewModel.SelectTransactionCommand.Execute(e.SelectedItem);
        }
        public TransactionsViewModel ViewModel
        {
            get { return BindingContext as TransactionsViewModel; }
            set { BindingContext = value; }
        }
    }
}