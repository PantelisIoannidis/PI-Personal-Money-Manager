﻿using System.Threading.Tasks;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public interface IPageService
    {
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);

        Task DisplayAlert(string title, string message, string cancel);

        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);

        Task PopAsync();

        Task PushAsync(Page page);
    }
}