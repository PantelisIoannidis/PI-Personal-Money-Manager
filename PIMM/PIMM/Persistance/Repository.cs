using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var transactions = db.Query<TransactionViewModel>(
                "SELECT TR.*,CA.Color as GlyphColor,FO.Glyph,FO.FontFamily FROM 'Transaction' TR " +
                "inner join Category CA on TR.CategoryId = CA.Id " +
                "inner join FontIcon FO on FO.Id = CA.FontIconId " );
            return transactions;
        }
        public List<TransactionViewModel> GetTransactions(Period period)
        {
            var transactions = db.Query<TransactionViewModel>(
                @"SELECT TR.*,CA.Color as GlyphColor,FO.Glyph,FO.FontFamily, AC.Color as AccountColor 
                FROM 'Transaction' TR  
                inner join Category CA on TR.CategoryId = CA.Id  
                inner join FontIcon FO on FO.Id = CA.FontIconId
                inner join Account AC on AC.Id = TR.AccountId 
                where TR.TransactionDate between ? and ? ", period.FromDate,period.ToDate);
            return transactions;
        }

        public List<Account> GetAllAccounts()
        {
            return db.Table<Account>().ToList();
        }

        public List<FontIcon> GetAllFontIcons()
        {
            return db.Table<FontIcon>().ToList();
        }

        public List<Category> GetAllCategories()
        {
            return db.Table<Category>().ToList();
        }

        public NewEditTransactionViewModel PopulateTransactionWithConnectedLists(NewEditTransactionViewModel tran)
        {
            var transactions = db.Query<TransactionDetailsCategoryViewModel>(
                @"SELECT CA.*, FO.Glyph, FO.FontFamily    
                FROM Category CA  
                inner join FontIcon FO on FO.Id = CA.FontIconId");

            tran.AccountsList = GetAllAccounts();
            tran.FontsList = GetAllFontIcons();
            var categoryList = GetAllCategories();
            tran.CategoriesList = transactions;
            return tran;
        }

    }
}
