using PIMM.Models;
using PIMM.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class FontIconViewModel : INotifyPropertyChanged
    {
        public List<FontIcon> FontIcons { get; set; }

        public FontIcon SelectedIcon { get; set; }

        private IPageService pageService;
        private IRepository repository;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SelectedFontCommand { get; set; }

        public FontIconViewModel(IPageService pageService, IRepository repository, List<FontIcon> fonts)
        {
            this.pageService = pageService;
            this.repository = repository;
            this.FontIcons = fonts;

            SelectedFontCommand = new Command(x => SelectedFont(x));
        }

        private void SelectedFont(object fonticon)
        {
            SelectedIcon = (fonticon as FontIcon);
             
            MessagingCenter.Send<FontIconViewModel,FontIcon>(this, "UpdateIcon", SelectedIcon);

        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
