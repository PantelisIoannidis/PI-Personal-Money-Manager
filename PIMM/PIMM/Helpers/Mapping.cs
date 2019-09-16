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
        public NewEditTransactionViewModel TransactionViewModel_2_NewEditTransactionViewModel(TransactionViewModel vm)
        {
            var newVM = new NewEditTransactionViewModel()
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

        public Transaction TransactionViewModel_2_Transaction(TransactionViewModel vm)
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

        public TransactionViewModel Transaction_2_TransactionViewModel(Transaction transaction, 
            Category category, FontIcon fontIcon, Account account)
        {
            var vm = new TransactionViewModel()
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

        public Transaction NewEditTransactionViewModel_2_Transaction(NewEditTransactionViewModel tranVM)
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
    }
}
