using PIMM.Helpers;
using PIMM.Models;
using PIMM.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class FontIconViewModel : INotifyPropertyChanged
    {
        private Func<FontIcon, bool> filter;

        public FontIcon SelectedIcon { get; set; }

        private IPageService pageService;
        private IRepository repository;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SelectedFontCommand { get; set; }
        public ICommand SearchCommand { get; private set; }

        private List<FontIcon> fontIcons;

        public List<FontIcon> FontIcons
        {
            get { return fontIcons.Where(filter).ToList(); }
            set { fontIcons = value; OnPropertyChanged(nameof(FontIcons)); }
        }

        public FontIconViewModel(IPageService pageService, IRepository repository, List<FontIcon> fonts)
        {
            this.filter = (x) => { return true; };
            this.pageService = pageService;
            this.repository = repository;
            this.FontIcons = fonts;

            SearchCommand = new Command<string>(s => Search(s));
            SelectedFontCommand = new Command(x => SelectedFont(x));
        }

        private void SelectedFont(object fonticon)
        {
            SelectedIcon = (fonticon as FontIcon);

            MessagingCenter.Send<FontIconViewModel, FontIcon>(this, MessagingString.UpdateIcon, SelectedIcon);
        }

        private void Search(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                this.filter = (x) => { return true; };
            }
            else
            {
                this.filter = (x) =>
                {
                    return (x.Description.ToLower().Contains(s.ToLower()));
                };
            }
            OnPropertyChanged(nameof(FontIcons));
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}