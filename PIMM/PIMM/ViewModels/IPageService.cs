using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public interface IPageService
    {
        Task PushAsync(Page page);
    }
}
