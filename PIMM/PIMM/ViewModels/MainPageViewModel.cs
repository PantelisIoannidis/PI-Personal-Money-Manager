using PIMM.Persistance;
using PIMM.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class MainPageViewModel
    {
        private readonly PageService pageService;
        private readonly Repository repository;
        private readonly InitializeDatabase initializeDatabase;

        public ICommand GoToSettingsPageCommand { get; set; }
        public ICommand GoToAboutPageCommand { get; set; }

        public MainPageViewModel(PageService pageService, Repository repository, InitializeDatabase initializeDatabase)
        {
            this.pageService = pageService;
            this.repository = repository;
            this.initializeDatabase = initializeDatabase;
            GoToSettingsPageCommand = new Command(async vm => await GoToSettingsPage());
            GoToAboutPageCommand = new Command(async vm => await GoToAboutPage());
        }

        private async Task GoToAboutPage()
        {
            await pageService.PushAsync(new AboutPage(pageService, repository));
        }

        private async Task GoToSettingsPage()
        {
            await pageService.PushAsync(new SettingsPage(pageService, repository, initializeDatabase));
        }
    }
}