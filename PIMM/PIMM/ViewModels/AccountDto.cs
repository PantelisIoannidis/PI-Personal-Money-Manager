using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.ViewModels
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public string FormPurposeNewOrEdit { get { return Id == 0 ? "New Account" : "Edit Selected Account"; } }
    }
}
