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

namespace PIMM.Views.CategoriesDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesDetailsPage : ContentPage
    {
        private IPageService pageService;
        private IRepository repository;
        private CategoryDetailsViewModel detailsVM=null;
        public CategoriesDetailsPage(IPageService pageService, IRepository repository, CategoryDto categories)
        {
            InitializeComponent();

            var mapping = new Mapping();

            this.pageService = pageService;
            this.repository = repository;
            detailsVM = new CategoryDetailsViewModel(pageService, repository, categories);


            BindingContext = detailsVM;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string color;
            myWebView.GetValueFromPickerAsync().ContinueWith(r => {
                color = r.Result;
                detailsVM.Category.Color = color;
                detailsVM.Unsubscribe();
                MessagingCenter.Send(this, "UpdateCategory", detailsVM.Category);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}