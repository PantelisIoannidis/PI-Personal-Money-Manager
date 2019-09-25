using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                (BindingContext as NavigationBarViewModel).SetDateCommand.Execute(e.NewDate);
        }
    }
}