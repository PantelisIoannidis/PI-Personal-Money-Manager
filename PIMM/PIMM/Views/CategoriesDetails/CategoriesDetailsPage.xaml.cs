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
        private CategoryDto vm;
        public CategoriesDetailsPage(IPageService pageService, IRepository repository, CategoryDto vm)
        {
            InitializeComponent();

            this.pageService = pageService;
            this.repository = repository;
            this.vm = vm;

            BindingContext = vm;
        }
    }
}