using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        List<CategoryDto> categories;
        CategoriesViewModel categoriesViewModel;
        Repository repository;
        public CategoriesPage()
        {
            InitializeComponent();

            repository = new Repository();
            categories = repository.GetCategoriesAsViewModels();
            categoriesViewModel = new CategoriesViewModel(categories, new PageService(), repository);
            MessagingCenter.Unsubscribe<CategoriesViewModel>(this, "DeleteCategory");
            MessagingCenter.Unsubscribe<CategoriesViewModel>(this, "RefreshCategory");
            MessagingCenter.Subscribe<CategoriesViewModel>(this, "DeleteCategory", RefreshCategories);
            MessagingCenter.Subscribe<CategoriesViewModel>(this, "RefreshCategory", RefreshCategories);
            BindingContext = categoriesViewModel;
        }

        private void RefreshCategories(CategoriesViewModel obj)
        {
            categories = repository.GetCategoriesAsViewModels();
            categoriesViewModel.Categories = categories;
        }

        public CategoriesViewModel ViewModel
        {
            get { return BindingContext as CategoriesViewModel; }
            set { BindingContext = value; }
        }

        private void CategoriesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectCategoryCommand.Execute(e.SelectedItem);
        }
    }
}