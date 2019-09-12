using PIMM.Helpers;
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
            var transactions = db.Query<TransactionViewModel>(
                "SELECT TR.*,CA.Color as GlyphColor,FO.Glyph,FO.FontFamily FROM 'Transaction' TR " +
                "inner join Category CA on TR.CategoryId = CA.Id " +
                "inner join FontIcon FO on FO.Id = CA.FontIconId " );
            return transactions;
        }
        public List<TransactionViewModel> GetTransactions(Period period)
        {
            var transactions = db.Query<TransactionViewModel>(
                @"SELECT TR.*,CA.Color as GlyphColor,FO.Glyph,FO.FontFamily FROM 'Transaction' TR  
                inner join Category CA on TR.CategoryId = CA.Id  
                inner join FontIcon FO on FO.Id = CA.FontIconId 
                where TR.TransactionDate between ? and ? ",period.FromDate,period.ToDate);
            return transactions;
        }

    }
}
