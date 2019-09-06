using PIMM.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PIMM.Helpers
{
    public static class FontAwesome
    {
        public static string FontName { get
            {

                switch (Device.RuntimePlatform)
                {

                    case Device.Android:
                        return FontAwesomeSolid.FontNameAndroid;
                        break;
                    case Device.iOS:
                        return FontAwesomeSolid.FontNameiOS;
                        break;
                    case Device.UWP:
                        return FontAwesomeSolid.FontNameUWP;
                        break;
                };
                return FontAwesomeSolid.FontNameAndroid;
            } }


        public static string FontNameBrand
        {
            get
            {

                switch (Device.RuntimePlatform)
                {

                    case Device.Android:
                        return FontAwesomeBrand.FontNameAndroid;
                        break;
                    case Device.iOS:
                        return FontAwesomeBrand.FontNameiOS;
                        break;
                    case Device.UWP:
                        return FontAwesomeBrand.FontNameUWP;
                        break;
                };
                return FontAwesomeBrand.FontNameAndroid;
            }
        }

    }
}
