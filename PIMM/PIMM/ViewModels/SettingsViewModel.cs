using PIMM.Helpers;
using PIMM.Persistance;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class SettingsViewModel
    {
        private IPageService pageService;
        private IRepository repository;
        private IInitializeDatabase initializeDatabase;

        public ICommand ResetDatabaseCommand { get; set; }

        public SettingsViewModel(IPageService pageService, IRepository repository, IInitializeDatabase initializeDatabase)
        {
            this.pageService = pageService;
            this.repository = repository;
            this.initializeDatabase = initializeDatabase;
            ResetDatabaseCommand = new Command(async x => await ResetDatabase());
        }

        private async Task ResetDatabase()
        {
            var response = await pageService.DisplayAlert("Erase all data", "Are you sure, you want to delete all data ?", "Yes", "No");
            if (response)
            {
                await initializeDatabase.EraseDatabase();
                MessagingCenter.Send(this, MessagingString.UpdateTransactionsAfterReset);
                MessagingCenter.Send(this, MessagingString.UpdateCategoryAfterReset);
                MessagingCenter.Send(this, MessagingString.UpdateAccountsAfterReset);
                await pageService.PopAsync();
            }
        }
    }
}