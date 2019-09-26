using PIMM.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using PIMM.Models;

namespace PIMM.Persistance
{
    public class InitializeDatabase
    {
        private SQLiteConnection db;
        private List<FontIcon> fontList = new List<FontIcon>();
        public InitializeDatabase()
        {
            db = DependencyService.Get<ISQLiteDb>().GetConnection();
            CreateTables();
        }

        public void CreateCategoriesAndAccounts()
        {
           
            CreateCetegories();
            CreateAcounts();
            PrepareIcons();
        }

        private void CreateTables()
        {
            db.CreateTable<FontIcon>();
            db.CreateTable<Account>();
            db.CreateTable<Category>();
            db.CreateTable<Transaction>();
        }

        public bool IsAnewDatabase()
        {
            if ((db.Table<Account>().Count()<=0) 
                && (db.Table<Category>().Count()<= 0)
                && (db.Table<FontIcon>().Count()<= 0))
                return true;
            else
                return false;
        }

        public bool IsAnEmptyDatabase()
        {
            if ((db.Table<Account>().Count() >= 0)
                && (db.Table<Category>().Count() >= 0)
                && (db.Table<FontIcon>().Count() >= 0)
                && (db.Table<Transaction>().Count() == 0))
                return true;
            else
                return false;
        }

        private void CreateCetegories()
        {
            CreateCategory("Salary", "#5e4603", TransactionType.Income, nameof(FontAwesomeSolid.Dollarsign), FontAwesomeSolid.Dollarsign, FontAwesome.FontName);
            CreateCategory("Cash", "#024d10", TransactionType.Income, nameof(FontAwesomeSolid.PiggyBank), FontAwesomeSolid.PiggyBank, FontAwesome.FontName);

            CreateCategory("Home", "#4d0239", TransactionType.Expense, nameof(FontAwesomeSolid.Home), FontAwesomeSolid.Home, FontAwesome.FontName);
            CreateCategory("Shopping", "#244d02", TransactionType.Expense, nameof(FontAwesomeSolid.Shoppingcart), FontAwesomeSolid.Shoppingcart, FontAwesome.FontName);
            CreateCategory("Groceries", "#02044d", TransactionType.Expense, nameof(FontAwesomeSolid.Shoppingbasket), FontAwesomeSolid.Shoppingbasket, FontAwesome.FontName);
            CreateCategory("Loan", "#300b2d", TransactionType.Expense, nameof(FontAwesomeSolid.HandHoldingUsd), FontAwesomeSolid.HandHoldingUsd, FontAwesome.FontName);
            CreateCategory("Utilities", "#0b302a", TransactionType.Expense, nameof(FontAwesomeSolid.Tint), FontAwesomeSolid.Tint, FontAwesome.FontName);
            CreateCategory("Clothes", "#30210b", TransactionType.Expense, nameof(FontAwesomeSolid.TShirt), FontAwesomeSolid.TShirt, FontAwesome.FontName);
            CreateCategory("Eating Out", "#300d0b", TransactionType.Expense, nameof(FontAwesomeSolid.Utensils), FontAwesomeSolid.Utensils, FontAwesome.FontName);
            CreateCategory("Entertainment", "#300b2e", TransactionType.Expense, nameof(FontAwesomeSolid.Gamepad), FontAwesomeSolid.Gamepad, FontAwesome.FontName);
            CreateCategory("Fuel", "#300b15", TransactionType.Expense, nameof(FontAwesomeSolid.GasPump), FontAwesomeSolid.GasPump, FontAwesome.FontName);
            CreateCategory("General", "#0b0b30", TransactionType.Expense, nameof(FontAwesomeSolid.Asterisk), FontAwesomeSolid.Shoppingbasket, FontAwesome.FontName);
            CreateCategory("Gifts", "#118a5b", TransactionType.Expense, nameof(FontAwesomeSolid.Gift), FontAwesomeSolid.Gift, FontAwesome.FontName);
            CreateCategory("Vacations", "#8a1137", TransactionType.Expense, nameof(FontAwesomeSolid.Suitcase), FontAwesomeSolid.Suitcase, FontAwesome.FontName);
            CreateCategory("Kids", "#8a8411", TransactionType.Expense, nameof(FontAwesomeSolid.Child), FontAwesomeSolid.Child, FontAwesome.FontName);
            CreateCategory("Pets", "#381f1f", TransactionType.Expense, nameof(FontAwesomeSolid.Cat), FontAwesomeSolid.Cat, FontAwesome.FontName);
            CreateCategory("Sports", "#0b112e", TransactionType.Expense, nameof(FontAwesomeSolid.Dumbbell), FontAwesomeSolid.Dumbbell, FontAwesome.FontName);
            CreateCategory("Travel", "#2e0b29", TransactionType.Expense, nameof(FontAwesomeSolid.Subway), FontAwesomeSolid.Subway, FontAwesome.FontName);
        }

        private void CreateCategory(string categoryName,string color,TransactionType type, string fontName, string fontGlyph, string fontFamily)
        {
            var icon = new FontIcon()
            {
                FontFamily = fontFamily,
                Description = fontName,
                Glyph = fontGlyph
            };
            fontList.Add(icon);
            db.Insert(icon);

            var category = new Category
            {
                FontIconId = icon.Id,
                Color=color,
                Type=type,
                Description = categoryName
            };
            db.Insert(category);
        }

        private void CreateAcounts()
        {
            var account = new Account {
                Description = "Payroll Account",
                Color = "#006266"
            };
            db.Insert(account);
            account = new Account
            {
                Description = "Savings Account",
                Color = "#EE5A24"
            };
            db.Insert(account);
        }

        private List<FontIcon> PrepareIcons()
        {
            List<FontIcon> fontAwesomeSolidIcons = new List<FontIcon>();
            FieldInfo[] field_infos = typeof(FontAwesomeSolid).GetFields(
                    BindingFlags.FlattenHierarchy |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Static);
            foreach (FieldInfo info in field_infos)
            {
                var infoName = info.ToString().Replace("System.String ", "");
                if (info.IsStatic 
                    && !infoName.Contains("FontName")
                    && fontAwesomeSolidIcons.Exists(c=>c.Description == infoName))
                {
                    fontAwesomeSolidIcons.Add(new FontIcon
                    {
                        Description = infoName,
                        Glyph = info.GetValue(null).ToString(),
                        FontFamily = FontAwesome.FontName,
                    });
                }
            }
            return fontAwesomeSolidIcons;
        }

        public void InsertSampleData()
        {
            if (db.Table<Transaction>().Count() > 0)
                return;

            var now = DateTime.Now;

            AddTransaction("Salary", 2500m,0,now);
            AddTransaction("Home", 1200m,0,now);
            AddTransaction("Shopping", 400m,0,now.AddDays(-1));
            AddTransaction("Groceries", 350m,0,now.AddDays(-2));
            AddTransaction("Loan", 100m,1,now.AddDays(-1));
            AddTransaction("Cash", 1250m,1,now);
            AddTransaction("Utilities", 80m,0,now.AddDays(-10));
            AddTransaction("Eating Out", 120m,0,now.AddDays(-11));
            AddTransaction("Entertainment", 60m,0, now.AddDays(-12));
            AddTransaction("Fuel", 110m,0, now.AddYears(-2));
            AddTransaction("General", 100m,0, now.AddYears(-2));
            AddTransaction("Vacations", 50m, 1);
            AddTransaction("Kids", 400m,0);
            AddTransaction("Pets", 120m,0);
            AddTransaction("Sports", 40m,0);
            AddTransaction("Travel", 30m,0);
            

        }

        private void AddTransaction(string categoryName,decimal amount,int accountNo,DateTime? dt=null)
        {
            if (dt == null)
                dt = DateTime.Now;
            var category = db.Table<Category>().FirstOrDefault(c => c.Description == categoryName);
            var icon = db.Table<FontIcon>().FirstOrDefault(c => c.Id == category.FontIconId);
            var account = db.Table<Account>().Skip(accountNo).FirstOrDefault();
            var transaction = new Transaction
            {
                AccountId = account.Id,
                CategoryId = category.Id,
                Description= category.Description,
                Type = category.Type,
                TransactionDate = dt.Value,
                Amount = amount
            };
            db.Insert(transaction);
        }


    }
}
