using PIMM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        private double width = 0;
        private double height = 0;

        public TransactionsPage()
        {
            InitializeComponent();
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