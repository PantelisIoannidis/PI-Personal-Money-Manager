using PIMM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.ViewModels
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int FontIconId { get; set; }
        public string Color { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }

        public string FontDescription { get; set; }
        public string FontGlyph { get; set; }
        public string FontFamily { get; set; }
    }
}
