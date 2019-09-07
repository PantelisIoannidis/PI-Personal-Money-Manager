using PIMM.Common.Helpers;
using PIMM.Helpers;
using PIMM.Models.ViewModels;
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
        public TransactionsPage()
        {
            InitializeComponent();


            transactions = GetSampleData();

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

        private async void DataGrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)

        {
            if (transactionDatagrid.SelectedItem == null)
                return;

            var response = await DisplayActionSheet("What do you want to do?", "Cancel",null, "Delete",
                  "Edit");
            Debug.WriteLine("Action: " + response);
            switch (response)
            {
                case "Delete":
                    Debug.WriteLine("delete ");
                    break;
                case "Edit":
                    Debug.WriteLine("edit ");
                    (BindingContext as TransactionsViewModel).SelectTransactionCommand.Execute(e.SelectedItem);
                    break;
                default:
                    Debug.WriteLine("default ");
                    break;
            }
            (BindingContext as TransactionsViewModel).SelectedTransaction = null;

        }

        private List<TransactionViewModel> GetSampleData(){
            return  new List<TransactionViewModel>() {
                new TransactionViewModel{
                    Id=1,
                    Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Addressbook,
                        FontFamily=FontAwesome.FontName,
                        Color="Blue"
                    },
                    Description = "bla Description 111",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 111",
                    SubcategoryDescription = "bla SubCategory 111",
                    Amount= 100
                },
                new TransactionViewModel{
                    Id=2,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Addresscard,
                        FontFamily=FontAwesome.FontName,
                        Color="Khaki"
                    },
                    Description = "bla Description 222",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 222",
                    SubcategoryDescription = "bla SubCategory 222",
                    Amount= -50
                },
                new TransactionViewModel{
                    Id=3,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Bell,
                        FontFamily=FontAwesome.FontName,
                        Color="Cyan"
                    },
                    Description = "bla Description 333",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 333",
                    SubcategoryDescription = "bla SubCategory 333",
                    Amount= 0
                },
                new TransactionViewModel{
                    Id=4,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Bellslash,
                        FontFamily=FontAwesome.FontName,
                        Color="Green"
                    },
                    Description = "bla Description 444",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 444",
                    SubcategoryDescription = "bla SubCategory 444",
                    Amount= -25
                },
                new TransactionViewModel{
                    Id=5,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Building,
                        FontFamily=FontAwesome.FontName,
                        Color="Yellow"
                    },
                    Description = "bla Description 555",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 555",
                    SubcategoryDescription = "bla SubCategory 555",
                    Amount= 20
                },
                new TransactionViewModel{
                    Id=6,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Table,
                        FontFamily=FontAwesome.FontName,
                        Color="Blue"
                    },
                    Description = "bla Description 666",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 666",
                    SubcategoryDescription = "bla SubCategory 666",
                    Amount= -5
                },
                new TransactionViewModel{
                    Id=7,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Paintbrush,
                        FontFamily=FontAwesome.FontName,
                        Color="Yellow"
                    },
                    Description = "bla Description 777",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 777",
                    SubcategoryDescription = "bla SubCategory 777",
                    Amount= 0
                },
                new TransactionViewModel{
                    Id=8,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Building,
                        FontFamily=FontAwesome.FontName,
                        Color="Red"
                    },
                    Description = "bla Description 888",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 888",
                    SubcategoryDescription = "bla SubCategory 888",
                    Amount= -80
                },
                new TransactionViewModel{
                    Id=9,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Building,
                        FontFamily=FontAwesome.FontName,
                        Color="Blue"
                    },
                    Description = "bla Description 999",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category 999",
                    SubcategoryDescription = "bla SubCategory 999",
                    Amount= 50.0m
                },
                new TransactionViewModel{
                    Id=10,
                     Icon = new IconViewModel()
                    {
                        Name=FontAwesomeSolid.Building,
                        FontFamily=FontAwesome.FontName,
                        Color="Blue"
                    },
                    Description = "bla Description aaaa",
                    TransactionDate=DateTime.Now,
                    CategoryDescription = "bla Category aaa",
                    SubcategoryDescription = "bla SubCategory aaa",
                    Amount= 150.20m
                },
            };

        }

    }
}