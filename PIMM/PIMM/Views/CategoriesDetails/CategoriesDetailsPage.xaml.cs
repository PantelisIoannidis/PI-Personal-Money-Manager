﻿using PIMM.Helpers;
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
        private CategoryDetailsViewModel detailsVM;
        public CategoriesDetailsPage(IPageService pageService, IRepository repository, CategoryDto category)
        {
            InitializeComponent();

            this.pageService = pageService;
            this.repository = repository;
            if (category.Id <= 0)
            {
                var temp_category = repository.GetFirstCategory(category.Type);
                category.Color = temp_category.Color;
                category.FontGlyph = temp_category.FontGlyph;
                category.FontFamily = temp_category.FontFamily;
            }
            detailsVM = new CategoryDetailsViewModel(pageService, repository, category);


            BindingContext = detailsVM;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string color;
            myWebView.GetValueFromPickerAsync().ContinueWith(r => {
                color = r.Result;
                detailsVM.Category.Color = color;
                //detailsVM.Unsubscribe();
                MessagingCenter.Send(this, "UpdateCategory", detailsVM.Category);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}