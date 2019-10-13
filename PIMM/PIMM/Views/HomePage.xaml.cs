using Microcharts;
using PIMM.Helpers;
using PIMM.Models;
using PIMM.ViewModels;
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
            MessagingCenter.Subscribe<MainPage>(this, MessagingString.UpdateCharts, UpdateCharts);
        }

        private void UpdateCharts(MainPage obj)
        {
            ChartPierIncomeExpense.WidthRequest = ChartWidth * 0.5;
            ChartPierIncomeExpense.HeightRequest = ChartWidth * 0.5;
            ChartPierIncomeExpense.Chart = new RadialGaugeChart
            {
                Entries = ViewModel.PrepareIncomeExpense,
                BackgroundColor = "backgroundColor".SKFromResources(),
            };

            ChartPierCategoriesByExpenses.WidthRequest = ChartWidth * 0.5;
            ChartPierCategoriesByExpenses.HeightRequest = ChartWidth * 0.5;
            ChartPierCategoriesByExpenses.Chart = new DonutChart
            {
                Entries = ViewModel.PrepareCategories(TransactionType.Expense),
                BackgroundColor = "backgroundColor".SKFromResources(),
            };

            ChartPierCategoriesByIncome.WidthRequest = ChartWidth * 0.5;
            ChartPierCategoriesByIncome.HeightRequest = ChartWidth * 0.5;
            ChartPierCategoriesByIncome.Chart = new DonutChart
            {
                Entries = ViewModel.PrepareCategories(TransactionType.Income),
                BackgroundColor = "backgroundColor".SKFromResources(),
            };

            ChartPierAccountsByExpenses.WidthRequest = ChartWidth * 0.5;
            ChartPierAccountsByExpenses.HeightRequest = ChartWidth * 0.5;
            ChartPierAccountsByExpenses.Chart = new BarChart
            {
                Entries = ViewModel.PrepareAccounts(TransactionType.Expense),
                BackgroundColor = "backgroundColor".SKFromResources(),
            };

            ChartPierAccountsByIncome.WidthRequest = ChartWidth * 0.5;
            ChartPierAccountsByIncome.HeightRequest = ChartWidth * 0.5;
            ChartPierAccountsByIncome.Chart = new BarChart
            {
                Entries = ViewModel.PrepareAccounts(TransactionType.Income),
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