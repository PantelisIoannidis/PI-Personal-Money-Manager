using PIMM.Helpers;
using PIMM.Persistance;
using PIMM.ViewModels;
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
    public partial class SettingsPage : ContentPage
    {
        public SettingsViewModel settingsVM;

        private readonly PageService pageService;
        private readonly Repository repository;

        public SettingsPage(PageService pageService, Repository repository)
        {
            InitializeComponent();

            this.pageService = pageService;
            this.repository = repository;

            settingsVM = new SettingsViewModel(pageService, repository);

            BindingContext = settingsVM;
        }

        private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var theme = themePicker.Items[themePicker.SelectedIndex];
            var app = (Application.Current as App);

            if (theme == Themes.Blue)
                app.SetBlueTheme();
               
            if (theme == Themes.Dark)
                app.SetDarkTheme();

            if (theme == Themes.Light)
                app.SetLightTheme();

            app.SetNavigationBarColor();

            app.Properties[Themes.Theme] = theme;
            app.SavePropertiesAsync();

            ExitPage();
        }

        private async Task ExitPage()
        {
            MessagingCenter.Send(this, "UpdateTransactions");
            await pageService.PopAsync();
        }

    }
}