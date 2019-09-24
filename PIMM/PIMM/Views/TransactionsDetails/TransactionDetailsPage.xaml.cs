using AutoMapper;
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
        private UpdateTransactionDto newTransVM;
        private readonly TransactionsDetailsViewModel transDetailVM;

        public TransactionDetailsPage(IPageService pageService,IRepository repository, TransactionDto transaction)
        {
            InitializeComponent();
            
            this.pageService = pageService;
            this.repository = repository;

            prepareViewModel(transaction);
            transDetailVM = new TransactionsDetailsViewModel(pageService, repository, newTransVM);
            BindingContext = transDetailVM;
        }

        private void prepareViewModel(TransactionDto vm)
        {

            this.newTransVM = Mapper.Map<TransactionDto,UpdateTransactionDto>(vm);
            newTransVM = repository.PopulateTransactionWithConnectedLists(newTransVM);
            if (newTransVM.CategoryId == 0)
                newTransVM.CategoryId = newTransVM.CurrentCategory.Id;
            if (newTransVM.AccountId <= 0)
                newTransVM.AccountId = newTransVM.CurrentAccount.Id;
            if (string.IsNullOrEmpty(newTransVM.Description))
                newTransVM.Description = newTransVM.CurrentCategory.Description;
        }

        public TransactionsDetailsViewModel ViewModel
        {
            get { return BindingContext as TransactionsDetailsViewModel; }
            set { BindingContext = value; }
        }
    }
}