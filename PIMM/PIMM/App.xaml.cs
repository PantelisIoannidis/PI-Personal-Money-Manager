using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage =  new NavigationPage(new MainPage()
            {
                //BarBackgroundColor = Color.FromHex("#2e2e2e"),
                //SelectedTabColor = Color.FromHex("#454545"),
                //UnselectedTabColor = Color.FromHex("#292929"),
                BarBackgroundColor = Color.FromHex("#2e2e2e"),
                BarTextColor = Color.White,
                BackgroundColor = Color.Black,
            })
            {
                BackgroundColor = Color.Black,
                BarBackgroundColor = Color.Black,
                BarTextColor=Color.White
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
