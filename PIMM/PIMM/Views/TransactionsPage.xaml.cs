using PIMM.Common.Helpers;
using PIMM.Helpers;
using PIMM.Model.ViewModels;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
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

            

            transactions = new List<TransactionViewModel>() {
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
            };

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
            (BindingContext as TransactionsViewModel).SelectTransactionCommand.Execute(e.SelectedItem);
        }
    }
}