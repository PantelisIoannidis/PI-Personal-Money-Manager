using PIMM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public IconViewModel Icon { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }    }
}
