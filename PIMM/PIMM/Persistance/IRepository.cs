using System.Collections.Generic;
using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;

namespace PIMM.Persistance
{
    public interface IRepository
    {
        int DeleteAccount(Account account);
        int DeleteTransaction(Transaction transaction);
        int DeleteTransaction(UpdateTransactionViewModel tranVM);
        List<AccountViewModel> GetAccountsAsViewModels();
        List<Account> GetAllAccounts();
        List<Category> GetAllCategories();
        List<FontIcon> GetAllFontIcons();
        List<TransactionViewModel> GetTransactions();
        List<TransactionViewModel> GetTransactions(Period period);
        UpdateTransactionViewModel PopulateTransactionWithConnectedLists(UpdateTransactionViewModel tran);
        int UpdateAccount(Account account);
        int UpdateAccount(AccountViewModel vm);
        int UpdateTransaction(Transaction transaction);
        int UpdateTransaction(UpdateTransactionViewModel tranVM);
    }
}