using PIMM.Helpers;
using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.AccountsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountsDetailsPage : ContentPage
    {
        private IPageService pageService;
        private IRepository repository;
        private AccountDto vm;

        public AccountsDetailsPage()
        {
            InitializeComponent();
        }

        public AccountsDetailsPage(IPageService pageService, IRepository repository, AccountDto vm)
        {
            InitializeComponent();

            this.pageService = pageService;
            this.repository = repository;
            this.vm = vm;

            BindingContext = vm;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string color;
            myWebView.GetValueFromPickerAsync().ContinueWith(r =>
            {
                color = r.Result;
                vm.Color = color;
                MessagingCenter.Send(this, MessagingString.UpdateAccount, vm);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}