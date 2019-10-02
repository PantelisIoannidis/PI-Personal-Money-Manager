using PIMM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : ContentView
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        private void ChooseDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (e.NewDate != e.OldDate)
            {
                if (BindingContext is ChartsViewModel)
                    (BindingContext as ChartsViewModel).NavigationBar.SetDateCommand.Execute(e.NewDate);
                if (BindingContext is TransactionsViewModel)
                    (BindingContext as TransactionsViewModel).NavigationBar.SetDateCommand.Execute(e.NewDate);
            }
        }
    }
}