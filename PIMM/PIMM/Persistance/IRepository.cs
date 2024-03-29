﻿using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System.Collections.Generic;

namespace PIMM.Persistance
{
    public interface IRepository
    {
        string DeleteAccount(Account account);

        string DeleteCategory(Category category);

        string DeleteTransaction(Transaction transaction);

        string DeleteTransaction(UpdateTransactionDto tranVM);

        List<AccountDto> GetAccountsAsViewModels();

        List<Account> GetAllAccounts();

        List<Category> GetAllCategories();

        List<FontIcon> GetAllFontIcons();

        List<AmountPerAccountViewModel> GetAmountByAccount(Period period);

        List<AmountPerCategoryViewModel> GetAmountByCategory(Period period);

        CategoryDto GetFirstCategory(TransactionType type = TransactionType.Expense);

        FontIcon GetFontIcon(int id);

        List<TransactionDto> GetTransactions();

        List<TransactionDto> GetTransactions(Period period);

        UpdateTransactionDto PopulateTransactionWithConnectedLists(UpdateTransactionDto tran);

        int UpdateAccount(Account account);

        int UpdateAccount(AccountDto vm);

        int UpdateCategory(Category category);

        int UpdateCategory(CategoryDto vm);

        int UpdateTransaction(Transaction transaction);

        int UpdateTransaction(UpdateTransactionDto tranVM);
    }
}