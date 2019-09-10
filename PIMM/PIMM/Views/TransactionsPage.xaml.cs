using PIMM.Helpers;
using PIMM.Models.ViewModels;
using PIMM.Persistance;
using PIMM.ViewModels;
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
        Repository repository; 
        public TransactionsPage()
        {
            InitializeComponent();

            repository = new Repository();
            transactions = repository.GetTransactions();

            BindingContext = new TransactionsViewModel(transactions, new PageService());

        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;

                UpdateLayout();
            }
        }

        private void UpdateLayout()
        {
            if (width > height)
            {
                Content = PortraitView;
            }
            else
            {
                Content = PortraitView;
            }
        }

        private void DataGrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)

        {
            ViewModel.SelectTransactionCommand.Execute(e.SelectedItem);
        }
        public TransactionsViewModel ViewModel
        {
            get { return BindingContext as TransactionsViewModel; }
            set { BindingContext = value; }
        }

    //private List<TransactionViewModel> GetSampleData(){
    //        return  new List<TransactionViewModel>() {
    //            new TransactionViewModel{
    //                Id=1,
    //                Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Blue"
    //                },
    //                Description = "bla Description 111",
    //                TransactionDate=DateTime.Now,
    //                Amount= 100
    //            },
    //            new TransactionViewModel{
    //                Id=2,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Khaki"
    //                },
    //                Description = "bla Description 222",
    //                TransactionDate=DateTime.Now,
    //                Amount= -50
    //            },
    //            new TransactionViewModel{
    //                Id=3,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Cyan"
    //                },
    //                Description = "bla Description 333",
    //                TransactionDate=DateTime.Now,
    //                Amount= 0
    //            },
    //            new TransactionViewModel{
    //                Id=4,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Green"
    //                },
    //                Description = "bla Description 444",
    //                TransactionDate=DateTime.Now,
    //                Amount= -25
    //            },
    //            new TransactionViewModel{
    //                Id=5,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Yellow"
    //                },
    //                Description = "bla Description 555",
    //                TransactionDate=DateTime.Now,
    //                Amount= 20
    //            },
    //            new TransactionViewModel{
    //                Id=6,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Blue"
    //                },
    //                Description = "bla Description 666",
    //                TransactionDate=DateTime.Now,
    //                Amount= -5
    //            },
    //            new TransactionViewModel{
    //                Id=7,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Yellow"
    //                },
    //                Description = "bla Description 777",
    //                TransactionDate=DateTime.Now,
    //                Amount= 0
    //            },
    //            new TransactionViewModel{
    //                Id=8,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Red"
    //                },
    //                Description = "bla Description 888",
    //                TransactionDate=DateTime.Now,
    //                Amount= -80
    //            },
    //            new TransactionViewModel{
    //                Id=9,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Blue"
    //                },
    //                Description = "bla Description 999",
    //                TransactionDate=DateTime.Now,
    //                Amount= 50.0m
    //            },
    //            new TransactionViewModel{
    //                Id=10,
    //                 Icon = new FontIconViewModel()
    //                {
    //                    Name=FontAwesomeSolid.Asterisk,
    //                    FontFamily=FontAwesome.FontName,
    //                    Color="Blue"
    //                },
    //                Description = "bla Description aaaa",
    //                TransactionDate=DateTime.Now,
    //                Amount= 150.20m
    //            },
    //        };

    //    }
    }
}