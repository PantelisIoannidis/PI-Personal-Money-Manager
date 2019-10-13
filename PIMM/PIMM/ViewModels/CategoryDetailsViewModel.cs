using PIMM.Helpers;
using PIMM.Models;
using PIMM.Persistance;
using PIMM.Views.CategoriesDetails;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class CategoryDetailsViewModel : INotifyPropertyChanged
    {
        private CategoryDto _category;
        private IPageService pageService;
        private IRepository repository;

        public event PropertyChangedEventHandler PropertyChanged;

        public CategoryDto Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        public ICommand SelectIconCommand { get; set; }

        public CategoryDetailsViewModel(IPageService pageService, IRepository repository, CategoryDto category)
        {
            this.pageService = pageService;
            this.repository = repository;
            Category = category;
            MessagingCenter.Unsubscribe<FontIconViewModel, FontIcon>(this, MessagingString.UpdateIcon);
            MessagingCenter.Subscribe<FontIconViewModel, FontIcon>(this, MessagingString.UpdateIcon, async (vm, x) => await UpdateIcon(vm));
            SelectIconCommand = new Command(async x => await SelectIcon());
        }

        private async Task UpdateIcon(object obj)
        {
            var fontId = (obj as FontIconViewModel).SelectedIcon.Id;
            var fontIcon = repository.GetFontIcon(fontId);
            Category.FontIconId = fontIcon.Id;
            Category.FontDescription = fontIcon.Description;
            Category.FontGlyph = fontIcon.Glyph;
            Category.FontFamily = fontIcon.FontFamily;
            OnPropertyChanged(nameof(Category));
            await pageService.PopAsync();
        }

        public string FormPurposeNewOrEdit
        {
            get { return Category.Id == 0 ? "New Category" : "Edit Selected Category"; }
        }

        private async Task SelectIcon()
        {
            var page = new CategoryIconsPage(pageService, repository, Category.FontIconId);
            await pageService.PushAsync(page);
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}