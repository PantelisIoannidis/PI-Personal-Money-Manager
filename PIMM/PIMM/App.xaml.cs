using AutoMapper;
using DLToolkit.Forms.Controls;
using PIMM.Helpers;
using PIMM.Persistance;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    public partial class App : Application
    {
        //Properties consts
        private const string PeriodKey = "periodKey";
        private NavigationPage navigationPage;
        private MainPage mainPage;
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            var createDatabase = new InitializeDatabase();
            if (createDatabase.IsAnewDatabase())
            {
                createDatabase.CreateCategoriesAndAccounts();
            }
            mainPage = new MainPage();
            navigationPage = new NavigationPage(mainPage);
            SetNavigationBarColor();
            MainPage = navigationPage;
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

        //Properties
        public Period CurrentPeriod
        {
            get
            {
                if (Properties.ContainsKey(PeriodKey))
                    return Properties[PeriodKey] as Period;

                return new Period();
            }

            set
            {
                Properties[PeriodKey] = (Period)value;
            }
        }

        public void SetBlueTheme()
        {
            Resources["backgroundColor"] = Color.FromHex("#95afc0");
            Resources["textColor"] = Color.FromHex("#ffffff");

            Resources["barBackgroundColor"] = Color.FromHex("#192a56");
            Resources["barTextColor"] = Color.FromHex("#ffffff");

            Resources["controlSelectedBackgroundColor"] = Color.FromHex("#192a56");
            Resources["controlBackgroundColor"] = Color.FromHex("#273c75");
            Resources["controlTextColor"] = Color.FromHex("#ffffff");

            Resources["seperatorColor"] = Color.FromHex("#05002e");

            Resources["IncomeColor"] = Color.FromHex("#025e0b");
            Resources["ExpenseColor"] = Color.FromHex("#570303");

            Resources["amountPositiveColor"] = Color.FromHex("#025e0b");
            Resources["amountNegativeColor"] = Color.FromHex("#570303");
            Resources["amountZeroColor"] = Color.FromHex("#000000");
            Resources["TransferColor"] = Color.FromHex("#000000");
            Resources["AdjustmentColor"] = Color.FromHex("#000000");

        }

        public void SetDarkTheme()
        {
            Resources["backgroundColor"] = Color.FromHex("#3f3f46");
            Resources["textColor"] = Color.FromHex("#ffffff");

            Resources["barBackgroundColor"] = Color.FromHex("#252526");
            Resources["barTextColor"] = Color.FromHex("#ffffff");

            Resources["controlSelectedBackgroundColor"] = Color.FromHex("#252526");
            Resources["controlBackgroundColor"] = Color.FromHex("#252526");
            Resources["controlTextColor"] = Color.FromHex("#ffffff");

            Resources["seperatorColor"] = Color.FromHex("#252222");

            Resources["IncomeColor"] = Color.FromHex("#c0e58a");
            Resources["ExpenseColor"] = Color.FromHex("#f89b00");

            Resources["amountPositiveColor"] = Color.FromHex("#c0e58a");
            Resources["amountNegativeColor"] = Color.FromHex("#f89b00");
            Resources["amountZeroColor"] = Color.FromHex("#ffffff");
            Resources["TransferColor"] = Color.FromHex("#ffffff");
            Resources["AdjustmentColor"] = Color.FromHex("#ffffff");


        }

        public void SetLightTheme()
        {
            Resources["backgroundColor"] = Color.FromHex("#ffffff");
            Resources["textColor"] = Color.FromHex("#000000");

            Resources["barBackgroundColor"] = Color.FromHex("#146b0e");
            Resources["barTextColor"] = Color.FromHex("#ffffff");

            Resources["controlSelectedBackgroundColor"] = Color.FromHex("#146b0e");
            Resources["controlBackgroundColor"] = Color.FromHex("#146b0e");
            Resources["controlTextColor"] = Color.FromHex("#ffffff");

            Resources["seperatorColor"] = Color.FromHex("#6b6161");

            Resources["IncomeColor"] = Color.FromHex("#020252");
            Resources["ExpenseColor"] = Color.FromHex("#3d0202");

            Resources["amountPositiveColor"] = Color.FromHex("#020252");
            Resources["amountNegativeColor"] = Color.FromHex("#3d0202");
            Resources["amountZeroColor"] = Color.FromHex("#000000");
            Resources["TransferColor"] = Color.FromHex("#ffffff");
            Resources["AdjustmentColor"] = Color.FromHex("#ffffff");


        }

        public void SetNavigationBarColor()
        {
            navigationPage.BackgroundColor = (Color)Application.Current.Resources["backgroundColor"];
            navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"];
            navigationPage.BarTextColor = (Color)Application.Current.Resources["barTextColor"];

            mainPage.BackgroundColor = (Color)Application.Current.Resources["backgroundColor"];
            mainPage.BarBackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"];
            mainPage.BarTextColor = (Color)Application.Current.Resources["barTextColor"];
        }
    }
}
