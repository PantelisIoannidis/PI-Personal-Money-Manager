using PIMM.Persistance;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        private List<CategoryDto> categories;
        private CategoriesViewModel categoriesViewModel;
        private Repository repository;

        public CategoriesPage()
        {
            InitializeComponent();

            repository = new Repository();
            categories = repository.GetCategoriesAsViewModels();
            categoriesViewModel = new CategoriesViewModel(categories, new PageService(), repository);

            MessagingCenter.Subscribe<CategoriesViewModel>(this, "DeleteCategory", RefreshCategories);
            MessagingCenter.Subscribe<CategoriesViewModel>(this, "RefreshCategory", RefreshCategories);
            MessagingCenter.Subscribe<SettingsViewModel>(this, "UpdateCategoryAfterReset", RefreshCategories);
            BindingContext = categoriesViewModel;
        }

        private void RefreshCategories(SettingsViewModel obj) => RefreshCategories();

        private void RefreshCategories(CategoriesViewModel obj) => RefreshCategories();

        private void RefreshCategories()
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