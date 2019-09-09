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

            MainPage = new NavigationPage(new MainPage()
            {
                //BarBackgroundColor = Color.FromHex("#2e2e2e"),
                //SelectedTabColor = Color.FromHex("#454545"),
                //UnselectedTabColor = Color.FromHex("#292929"),

                BackgroundColor = (Color)Application.Current.Resources["backgroundColor"],
                BarBackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"],
                BarTextColor = (Color)Application.Current.Resources["barTextColor"],

            })
            {
                BackgroundColor = (Color)Application.Current.Resources["backgroundColor"],
                BarBackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"],
                BarTextColor = (Color)Application.Current.Resources["barTextColor"],
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
