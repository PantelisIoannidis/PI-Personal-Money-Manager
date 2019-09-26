﻿using PIMM.Persistance;
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
    public partial class SettingsPage : ContentPage
    {
        private readonly PageService pageService;
        private readonly Repository repository;

        public SettingsPage(PageService pageService, Repository repository)
        {
            InitializeComponent();

            this.pageService = pageService;
            this.repository = repository;
        }
    }
}