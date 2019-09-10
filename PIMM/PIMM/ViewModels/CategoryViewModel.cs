using PIMM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public int FontIconId { get; set; }
        public string Color { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
    }
}
