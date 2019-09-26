using Microcharts;
using PIMM.Helpers;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private double width = 0;
        private double height = 0;

        public int ChartsPerRow { get; set; } = 1;
        public int ChartWidth { get; set; }

        public HomePage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<MainPage>(this, "UpdateCharts", UpdateCharts);
        }

        private void UpdateCharts(MainPage obj)
        {
            ChartPierIncomeExpense.WidthRequest = ChartWidth * 0.5;
            ChartPierIncomeExpense.HeightRequest = ChartWidth * 0.5;
            ChartPierIncomeExpense.Chart = new BarChart
            {
                Entries = ViewModel.PrepareIncomeExpense,
                BackgroundColor = "backgroundColor".SKFromResources(),
            };

            ChartPierCategories.WidthRequest = ChartWidth * 0.5;
            ChartPierCategories.HeightRequest = ChartWidth * 0.5;
            ChartPierCategories.Chart = new DonutChart
            {
                Entries = ViewModel.PrepareCategoriesExpenses,
                BackgroundColor = "backgroundColor".SKFromResources(),
            };

            ChartPierAccounts.WidthRequest = ChartWidth * 0.5;
            ChartPierAccounts.HeightRequest = ChartWidth * 0.5;
            ChartPierAccounts.Chart = new RadialGaugeChart
            {
                Entries = ViewModel.PrepareAccounts,
                BackgroundColor = "backgroundColor".SKFromResources(),
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateLayout();
            UpdateCharts(null);
        }

  
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            UpdateLayout();

        }

        private void UpdateLayout()
        {
            DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            DisplayOrientation orientation = mainDisplayInfo.Orientation;
            DisplayRotation rotation = mainDisplayInfo.Rotation;
            double width = mainDisplayInfo.Width;
            double height = mainDisplayInfo.Height;
            double density = mainDisplayInfo.Density;

            if (orientation == DisplayOrientation.Landscape)
            {
                ChartsPerRow = 2;
            }
            else
            {
                ChartsPerRow = 1;
            }
            ChartWidth = (int)((width / density) / ChartsPerRow);
        }


        public ChartsViewModel ViewModel
        {
            get { return BindingContext as ChartsViewModel; }
            set { BindingContext = value; }
        }
    }
}