using System;
using System.Collections.Generic;
using System.Text;
using PIMM.Persistance;

namespace PIMM.ViewModels
{
    public class SettingsViewModel
    {
        private PageService pageService;
        private Repository repository;

        public SettingsViewModel(PageService pageService, Repository repository)
        {
            this.pageService = pageService;
            this.repository = repository;
        }
    }
}
