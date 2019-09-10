using PIMM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIMM.Persistance
{
    public interface IRepository
    {
        List<TransactionViewModel> GetTransactions();
    }
}
