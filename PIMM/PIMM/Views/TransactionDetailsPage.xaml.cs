using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailsPage : ContentPage
    {
        private readonly TransactionViewModel transaction;

        public TransactionDetailsPage(TransactionViewModel transaction)
        {
            InitializeComponent();
            this.transaction = transaction;
        }
    }
}