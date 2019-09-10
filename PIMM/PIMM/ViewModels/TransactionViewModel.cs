using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        public string FontFamily { get; set; }
        public string Glyph { get; set; }
        public string Color { get; set; }



        public void MapToViewModel(Transaction transaction,Category category,FontIcon fontIcon)
        {
            MapTransaction(transaction);
            MapCategory(category);
            MapFontIcon(fontIcon);
        }

        public void MapTransaction(Transaction transaction)
        {
            this.Id = transaction.Id;
            this.Description = transaction.Description;
            this.Type = transaction.Type;
            this.TransactionDate = transaction.TransactionDate;
            this.Amount = transaction.Amount;
        }

        public void MapFontIcon(FontIcon icon)
        {
            this.Glyph = icon.Glyph;
        }

        public void MapCategory(Category category)
        {
            this.Color = category.Color;
        }

    }
}
