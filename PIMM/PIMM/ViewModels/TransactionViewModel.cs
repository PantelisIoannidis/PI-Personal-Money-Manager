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
        public FontIconViewModel Icon { get; set; } = new FontIconViewModel();
        public CategoryViewModel Category { get; set; } = new CategoryViewModel();
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }


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
            this.TransactionType = transaction.Type;
            this.TransactionDate = transaction.TransactionDate;
            this.Amount = transaction.Amount;
        }

        public void MapFontIcon(FontIcon icon)
        {
            this.Icon.Id = icon.Id;
            this.Icon.Name = icon.Name;
            this.Icon.Glyph = icon.Glyph;
            this.Icon.FontFamily = icon.FontFamily;
        }

        public void MapCategory(Category category)
        {
            this.Category.Id = category.Id;
            this.Category.FontIconId = category.FontIconId;
            this.Category.Description = category.Description;
            this.Category.Type = category.Type;
            this.Category.Color = category.Color;
        }

    }
}
