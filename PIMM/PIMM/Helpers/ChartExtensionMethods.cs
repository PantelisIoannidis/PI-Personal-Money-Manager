using SkiaSharp;
using Xamarin.Forms;

namespace PIMM.Helpers
{
    public static class ChartExtensionMethods
    {
        public static SKColor SKFromResources(this string color)
        {
            return SKColor.Parse(((Color)Application.Current.Resources[color]).GetHexString());
        }

        public static Color ColorFromResources(this string color)
        {
            return (Color)Application.Current.Resources[color];
        }
    }
}