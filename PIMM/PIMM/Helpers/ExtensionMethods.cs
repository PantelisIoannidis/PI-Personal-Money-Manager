﻿using System;

namespace PIMM.Helpers
{
    public static class ExtensionMethods
    {
        public static string GetHexStringAlpha(this Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }

        public static string GetHexString(this Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var hex = $"#{red:X2}{green:X2}{blue:X2}";

            return hex;
        }

        public static string FormatAmount(this decimal amount)
        {
            return String.Format("{0:C}", (decimal)amount);
        }
    }
}