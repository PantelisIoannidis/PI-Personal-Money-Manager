using PIMM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPicker : ContentView
    {
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(string), typeof(ColorPicker),null, 
                defaultBindingMode:BindingMode.TwoWay, propertyChanged: OnColorChanged);

        private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public string Color
        {
            get {
                return (string)GetValue(ColorProperty); }
            set {
                SetValue(ColorProperty, value);
            }
        }

        public static readonly BindableProperty BackcolorProperty =
            BindableProperty.Create("Backcolor", typeof(string), typeof(ColorPicker), null,
                defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnBackcolorChanged);

        private static void OnBackcolorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public string Backcolor
        {
            get {
                return (string)GetValue(BackcolorProperty); }
            set {
                SetValue(BackcolorProperty, value);
            }
        }



        public ColorPicker()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            PrepareWebViewColorPicker();
        }
   
        private void PrepareWebViewColorPicker()
        {
            var bgcolor = (Color)Application.Current.Resources["backgroundColor"];
            var bgcolorStr = bgcolor.GetHexString();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = $@"
                                <html>
                                <body>
                                    <input type='color' id='colorPicker' name='colorPicker' value='{Color}'>
                                    <script>
                                        document.body.style.background = '{bgcolorStr}';
                                    </script>
                                </ body >
                                </ html >
                                ";
            webView.Source = htmlSource;
        }

        public async Task<string> GetValueFromPickerAsync(string controlId= "colorPicker")
        {
            return await webView.EvaluateJavaScriptAsync($"document.getElementById('{controlId}').value;");
        }
    }
}