using PIMM.Helpers;
using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Linq;
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
        private CategoryDetailsViewModel detailsVM;

        public CategoriesDetailsPage(IPageService pageService, IRepository repository, CategoryDto category)
        {
            InitializeComponent();

            this.pageService = pageService;
            this.repository = repository;
            detailsVM = new CategoryDetailsViewModel(pageService, repository, category);
        }

        public void SetCategoryDetailsViewModel(CategoryDto category)
        {
            if (category.Id <= 0)
            {
                var temp_category = repository.GetFirstCategory(category.Type);
                if (temp_category == null)
                {
                    var aFont = repository.GetAllFontIcons().FirstOrDefault();
                    temp_category = new CategoryDto
                    {
                        Color = "#153ed4",
                        FontFamily = aFont.FontFamily,
                        FontGlyph = aFont.Glyph,
                        FontIconId = aFont.Id
                    };
                }

                category.Color = temp_category.Color;
                category.FontGlyph = temp_category.FontGlyph;
                category.FontFamily = temp_category.FontFamily;
                category.FontIconId = temp_category.FontIconId;
            }
            detailsVM.Category = category;

            BindingContext = detailsVM;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string color;
            myWebView.GetValueFromPickerAsync().ContinueWith(r =>
            {
                color = r.Result;
                detailsVM.Category.Color = color;
                //detailsVM.Unsubscribe();
                MessagingCenter.Send(this, MessagingString.UpdateCategory, detailsVM.Category);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}