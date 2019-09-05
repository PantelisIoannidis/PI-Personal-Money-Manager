using PIMM.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PIMM.Helpers
{
    public static class FontAwesome
    {

        public static string FontName()
        {
            string fontName = "";
            switch (Device.RuntimePlatform)
            {
                
                case Device.Android:
                    fontName = FontAwesomeRegular.FontNameAndroid;
                    break;
                case Device.iOS:
                    fontName = FontAwesomeRegular.FontNameiOS;
                    break;
                case Device.UWP:
                    fontName = FontAwesomeRegular.FontNameUWP;
                    break;
            }
            return fontName;
        }

        public static string FontNameBrand()
        {
            string fontName = "";
            switch (Device.RuntimePlatform)
            {

                case Device.Android:
                    fontName = FontAwesomeRegular.FontNameAndroid;
                    break;
                case Device.iOS:
                    fontName = FontAwesomeRegular.FontNameiOS;
                    break;
                case Device.UWP:
                    fontName = FontAwesomeRegular.FontNameUWP;
                    break;
            }
            return fontName;
        }

    }
}
