using AutoMapper;
using PIMM.Helpers;
using PIMM.Models;
using PIMM.Models.ViewModels;
using PIMM.ViewModels;
using SQLite;
using System.Collections.Generic;
using System.Linq;
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

        public List<AmountPerCategoryViewModel> GetAmountByCategory(Period period)
        {
            var result = db.Query<AmountPerCategoryViewModel>(
                @"select CategoryId,CA.Description,CA.Color,CA.Type, sum(Amount) as Amount from 'Transaction' TR
                    inner join Category CA on CA.Id = TR.CategoryId
                    where TR.TransactionDate between ? and ?
                    group by CategoryId
                    order by Amount DESC "
                , period.FromDate, period.ToDate);
            return result;
        }

        public List<AmountPerAccountViewModel> GetAmountByAccount(Period period)
        {
            var result = db.Query<AmountPerAccountViewModel>(
                @"select AccountId,AC.Description,AC.Color,TR.Type, sum(Amount) as Amount from 'Transaction' TR
                    inner join Account AC on AC.Id = TR.AccountId
                    where TR.TransactionDate between ? and ?
                    group by TR.AccountId,TR.Type
                    order by Amount DESC"
                , period.FromDate, period.ToDate);
            return result;
        }

        public List<TransactionDto> GetTransactions()
        {
            var transactions = db.Query<TransactionDto>(
                @"SELECT TR.*,CA.Color as GlyphColor,FO.Glyph,FO.FontFamily, AC.Color as AccountColor, CA.Description as CategoryDescription
                FROM 'Transaction' TR
                inner join Category CA on TR.CategoryId = CA.Id
                inner join FontIcon FO on FO.Id = CA.FontIconId
                inner join Account AC on AC.Id = TR.AccountId ");
            return transactions;
        }

        internal List<CategoryDto> GetCategoriesAsViewModels()
        {
            var categories = db.Query<CategoryDto>(
                @"SELECT CA.*, FO.Glyph as FontGlyph, FO.FontFamily, FO.Description as FontDescription
                FROM Category CA
                inner join FontIcon FO on FO.Id = CA.FontIconId ");
            return categories;
        }

        public List<TransactionDto> GetTransactions(Period period)
        {
            var transactions = db.Query<TransactionDto>(
                @"SELECT TR.*,CA.Color as GlyphColor,FO.Glyph,FO.FontFamily, AC.Color as AccountColor, CA.Description as CategoryDescription
                FROM 'Transaction' TR
                inner join Category CA on TR.CategoryId = CA.Id
                inner join FontIcon FO on FO.Id = CA.FontIconId
                inner join Account AC on AC.Id = TR.AccountId
                where TR.TransactionDate between ? and ? ", period.FromDate, period.ToDate);
            return transactions;
        }

        public List<AccountDto> GetAccountsAsViewModels()
        {
            var accounts = db.Query<AccountDto>(
                @"SELECT * FROM Account ");
            return accounts;
        }

        public List<Account> GetAllAccounts()
        {
            return db.Table<Account>().ToList();
        }

        public FontIcon GetFontIcon(int id)
        {
            return db.Table<FontIcon>().FirstOrDefault(x => x.Id == id);
        }

        public List<FontIcon> GetAllFontIcons()
        {
            return db.Table<FontIcon>().ToList();
        }

        public List<Category> GetAllCategories()
        {
            return db.Table<Category>().ToList();
        }

        public CategoryDto GetFirstCategory(TransactionType type = TransactionType.Expense)
        {
            var category = db.Query<CategoryDto>(
                @"SELECT CA.*, FO.Glyph as FontGlyph, FO.FontFamily, FO.Description as FontDescription
                FROM Category CA
                inner join FontIcon FO on FO.Id = CA.FontIconId
                Where CA.Type = ?
                Order By CA.Id Limit 1
                ", type).FirstOrDefault();
            return category;
        }

        public UpdateTransactionDto PopulateTransactionWithConnectedLists(UpdateTransactionDto tran)
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

        public int UpdateTransaction(UpdateTransactionDto tranVM)
        {
            var transaction = Mapper.Map<UpdateTransactionDto, Transaction>(tranVM);

            return UpdateTransaction(transaction);
        }

        public int UpdateTransaction(Transaction transaction)
        {
            int rows = 0;
            if (transaction.Id <= 0)
            {
                rows = db.Insert(transaction);
                return rows;
            }
            else
            {
                rows = db.Update(transaction);
                return rows;
            }
        }

        public string DeleteTransaction(UpdateTransactionDto tranVM)
        {
            var transaction = Mapper.Map<UpdateTransactionDto, Transaction>(tranVM);

            return DeleteTransaction(transaction);
        }

        public string DeleteTransaction(Transaction transaction)
        {
            db.Delete(transaction);
            return "OK";
        }

        public int UpdateAccount(AccountDto vm)
        {
            var account = Mapper.Map<AccountDto, Account>(vm);

            return UpdateAccount(account);
        }

        public int UpdateAccount(Account account)
        {
            int rows = 0;
            if (account.Id <= 0)
            {
                rows = db.Insert(account);
                return rows;
            }
            else
            {
                rows = db.Update(account);
                return rows;
            }
        }

        public string DeleteAccount(Account account)
        {
            var transactionsWithThatAccountInUse =
                db.ExecuteScalar<int>(@"SELECT COUNT(*) FROM 'Transaction' WHERE AccountId = ? ", account.Id);
            if (transactionsWithThatAccountInUse > 0)
                return $"This account is used in {transactionsWithThatAccountInUse} transactions and it cannot be erased.";

            db.Delete(account);
            return "OK";
        }

        public string DeleteCategory(Category category)
        {
            var transactionsWithThatCategoryInUse =
                db.ExecuteScalar<int>(@"SELECT COUNT(*) FROM 'Transaction' WHERE CategoryId = ? ", category.Id);
            if (transactionsWithThatCategoryInUse > 0)
                return $"This category is used in {transactionsWithThatCategoryInUse}  transactions and it cannot be erased.";

            db.Delete(category);
            return "OK";
        }

        public int UpdateCategory(CategoryDto vm)
        {
            var category = Mapper.Map<CategoryDto, Category>(vm);
            return UpdateCategory(category);
        }

        public int UpdateCategory(Category category)
        {
            int rows = 0;
            if (category.Id <= 0)
            {
                rows = db.Insert(category);
                return rows;
            }
            else
            {
                rows = db.Update(category);
                return rows;
            }
        }
    }
}