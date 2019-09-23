using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;


namespace PIMM.Helpers
{
    public class Mapping
    {

        public Account AccountDto_2_Account(AccountDto vm)
        {
            return new Account {
                 Id=vm.Id,
                 Color=vm.Color,
                 Description=vm.Description
            };
        }
        public UpdateTransactionDto TransactionDto_2_UpdateTransactionDto(TransactionDto vm)
        {
            var newVM = new UpdateTransactionDto()
            {
                Id = vm.Id,
                AccountId = vm.AccountId,
                CategoryId = vm.CategoryId,
                Amount = vm.Amount,
                Description = vm.Description,
                TransactionDate = vm.TransactionDate,
                Type = vm.Type
            };

            return newVM;
        }

        public Transaction TransactionDto_2_Transaction(TransactionDto vm)
        {
            var trans = new Transaction()
            {
                Id = vm.Id,
                AccountId = vm.AccountId,
                CategoryId = vm.CategoryId,
                Amount = vm.Amount,
                Description = vm.Description,
                TransactionDate = vm.TransactionDate,
                Type = vm.Type
            };

            return trans;

        }

        public TransactionDto Transaction_2_TransactionDto(Transaction transaction, 
            Category category, FontIcon fontIcon, Account account)
        {
            var vm = new TransactionDto()
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                CategoryId = transaction.CategoryId,
                Description = transaction.Description,
                Type = transaction.Type,
                TransactionDate = transaction.TransactionDate,
                Amount = transaction.Amount,
                FontFamily = fontIcon.FontFamily,
                Glyph = fontIcon.Glyph,
                GlyphColor = category.Color,
                AccountColor = account.Color
            };
            return vm;
        }

        public Transaction UpdateTransactionDto_2_Transaction(UpdateTransactionDto tranVM)
        {
            var transaction = new Transaction
            {
                Id = tranVM.Id,
                AccountId = tranVM.AccountId,
                CategoryId = tranVM.CategoryId,
                Type = tranVM.Type,
                TransactionDate = tranVM.TransactionDate,
                Amount = tranVM.Amount,
                Description = tranVM.Description
            };
            return transaction;
        }

        public Category CategoryDto_2_Category(CategoryDto vm)
        {
            var category = new Category
            {
                Id = vm.Id,
                Color = vm.Color,
                Description = vm.Description,
                FontIconId = vm.FontIconId,
                Type = vm.Type
            };

            return category;
        }
    }
}
