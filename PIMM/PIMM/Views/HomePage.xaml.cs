using PIMM.Common.Helpers;
using PIMM.Helpers;
using PIMM.Model.ViewModels;
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
    public partial class HomePage : ContentPage
    {
        
        public HomePage()
        {
            InitializeComponent();

                    }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //label1.Text = FontAwesomeRegular.Stickynote;
            //label1.FontFamily = FontAwesome.FontName();
        }
    }
}