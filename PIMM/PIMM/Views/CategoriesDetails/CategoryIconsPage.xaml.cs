using PIMM.Models;
using PIMM.Persistance;
using PIMM.ViewModels;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views.CategoriesDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryIconsPage : ContentPage
    {
        private readonly IPageService pageService;
        private readonly IRepository repository;
        private List<FontIcon> fonts;
        private FontIconViewModel fontIconViewModel;

        public CategoryIconsPage(IPageService pageService, IRepository repository, int FontIconId)
        {
            InitializeComponent();
            this.pageService = pageService;
            this.repository = repository;
            fonts = repository.GetAllFontIcons();
            fontIconViewModel = new FontIconViewModel(pageService, repository, fonts);

            BindingContext = fontIconViewModel;
        }
    }
}