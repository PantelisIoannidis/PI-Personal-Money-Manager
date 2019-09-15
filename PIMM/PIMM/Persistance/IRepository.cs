using PIMM.Helpers;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIMM.Persistance
{
    public interface IRepository
    {
        List<TransactionViewModel> GetTransactions();
        List<TransactionViewModel> GetTransactions(Period period);
        NewEditTransactionViewModel PopulateTransactionWithConnectedLists(NewEditTransactionViewModel tran);
    }
}
