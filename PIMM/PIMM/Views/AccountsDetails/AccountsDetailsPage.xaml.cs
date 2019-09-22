using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMM.Persistance;
using PIMM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.AccountsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountsDetailsPage : ContentPage
    {
        private IPageService pageService;
        private IRepository repository;
        private AccountViewModel vm;

        public AccountsDetailsPage()
        {
            InitializeComponent();
        }

        public AccountsDetailsPage(IPageService pageService, IRepository repository, AccountViewModel vm)
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
            myWebView.GetValueFromPickerAsync().ContinueWith( r=> {
                color = r.Result;
                vm.Color = color;
                MessagingCenter.Send(this, "UpdateAccount",vm);
            },TaskScheduler.FromCurrentSynchronizationContext());

        }

    }
}