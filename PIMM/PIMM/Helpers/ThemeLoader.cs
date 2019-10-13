using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PIMM.Helpers
{
    public class ThemeLoader
    {
        private App app;

        public ThemeLoader()
        {
            app = (Application.Current as App);
        }

        public void LoadTheme()
        {
            if (!app.Properties.ContainsKey(Themes.ThemeKey))
            {
                app.Properties[Themes.ThemeKey] = Themes.Dark;
            }

            var theme = app.Properties[Themes.ThemeKey].ToString();

            if (theme == Themes.Dark)
                app.SetDarkTheme();
            else if (theme == Themes.Light)
                app.SetLightTheme();
            else if (theme == Themes.Blue)
                app.SetBlueTheme();
        }

        public void SetNavigationBarColor()
        {
            app.SetNavigationBarColor();
        }
    }
}