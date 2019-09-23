using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.TransactionsDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsDetailsCategories : ContentPage
    {
        private readonly UpdateTransactionDto transVM;

        public TransactionsDetailsCategories(UpdateTransactionDto transVM)
        {
            InitializeComponent();
            this.transVM = transVM;
            BindingContext = transVM;
        }

        public ListView CategoriesListView { get { return categoriesListView; } }
    }
}