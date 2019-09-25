using PIMM.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PIMM.ViewModels
{
    public class ChartsViewModel : INotifyPropertyChanged
    {
        private readonly IPageService pageService;
        private readonly IRepository repository;

        public ChartsViewModel(NavigationBarViewModel navigationBarViewModel, IPageService pageService, IRepository repository)
        {
            this.NavigationBar = navigationBarViewModel;
            this.pageService = pageService;
            this.repository = repository;
        }

        private NavigationBarViewModel navigationBar;

        public NavigationBarViewModel NavigationBar
        {
            get { return navigationBar; }
            set { navigationBar = value; OnPropertyChanged(nameof(NavigationBar)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
