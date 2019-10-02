using PIMM.Persistance;
using PIMM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private readonly PageService pageService;
        private readonly Repository repository;

        public AboutPage(PageService pageService, Repository repository)
        {
            InitializeComponent();
            this.pageService = pageService;
            this.repository = repository;
        }
    }
}