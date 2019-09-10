using PIMM.Models;
using PIMM.Models.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PIMM.Persistance
{
    public class Repository : IRepository
    {
        private SQLiteConnection db;
        public Repository()
        {
            db = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        public List<TransactionViewModel> GetTransactions()
        {
            List<TransactionViewModel> transactionsViewModel = new List<TransactionViewModel>() ;
            var transactions = db.Table<Transaction>().ToList();
            foreach (var tran in transactions)
            {
                var transactionViewModel = new TransactionViewModel();
                transactionViewModel.MapTransaction(tran);
                var category = db.Table<Category>().First(c => c.Id == tran.CategoryId);
                transactionViewModel.MapCategory(category);
                var icon = db.Table<FontIcon>().FirstOrDefault(c => c.Id == category.FontIconId);
                transactionViewModel.MapFontIcon(icon);
                transactionsViewModel.Add(transactionViewModel);
            }
            return transactionsViewModel;
        }

    }
}
