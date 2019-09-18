using System.Collections.Generic;
using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;

namespace PIMM.Persistance
{
    public interface IRepository
    {
        int DeleteTransaction(NewEditTransactionViewModel tranVM);
        int DeleteTransaction(Transaction transaction);
        List<Account> GetAllAccounts();
        List<Category> GetAllCategories();
        List<FontIcon> GetAllFontIcons();
        List<TransactionViewModel> GetTransactions();
        List<TransactionViewModel> GetTransactions(Period period);
        NewEditTransactionViewModel PopulateTransactionWithConnectedLists(NewEditTransactionViewModel tran);
        int UpdateTransaction(NewEditTransactionViewModel tranVM);
        int UpdateTransaction(Transaction transaction);
    }
}