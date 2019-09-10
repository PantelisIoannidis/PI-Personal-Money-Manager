using PIMM.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace PIMM.Persistance
{
    public class InitializeDatabase
    {
        private SQLiteConnection db;
        private List<FontIcon> fontList = new List<FontIcon>();
        public InitializeDatabase()
        {
            db = DependencyService.Get<ISQLiteDb>().GetConnection();

            CreateDatabaseIfItIsNotExists();

        }

        private void CreateDatabaseIfItIsNotExists()
        {
            CreateTables();
            if ((IsAnewDatabase()==false))
                return;
            CreateCetegories();
            CreateAcounts();
            InsertSampleData();
        }


        private void CreateTables()
        {
            db.CreateTable<FontIcon>();
            db.CreateTable<Account>();
            db.CreateTable<Category>();
            db.CreateTable<Transaction>();
        }

        private bool IsAnewDatabase()
        {
            if ((db.Table<Account>().Count()<=0) 
                && (db.Table<Category>().Count()<= 0)
                && (db.Table<FontIcon>().Count()<= 0))
                return true;
            else
                return false;
        }

        private void CreateCetegories()
        {
            CreateCategory("Salary", "Black", TransactionType.Income, nameof(FontAwesomeSolid.Dollarsign), FontAwesomeSolid.Dollarsign, FontAwesome.FontName);
            CreateCategory("Cash", "Black", TransactionType.Income, nameof(FontAwesomeSolid.PiggyBank), FontAwesomeSolid.PiggyBank, FontAwesome.FontName);

            CreateCategory("Home","Black", TransactionType.Expense, nameof(FontAwesomeSolid.Home), FontAwesomeSolid.Home, FontAwesome.FontName);
            CreateCategory("Shopping", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Shoppingcart), FontAwesomeSolid.Shoppingcart, FontAwesome.FontName);
            CreateCategory("Groceries", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Shoppingbasket), FontAwesomeSolid.Shoppingbasket, FontAwesome.FontName);
            CreateCategory("Loan", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.HandHoldingUsd), FontAwesomeSolid.HandHoldingUsd, FontAwesome.FontName);
            CreateCategory("Utilities", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Tint), FontAwesomeSolid.Tint, FontAwesome.FontName);
            CreateCategory("Clothes", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.TShirt), FontAwesomeSolid.TShirt, FontAwesome.FontName);
            CreateCategory("Eating Out", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Utensils), FontAwesomeSolid.Utensils, FontAwesome.FontName);
            CreateCategory("Entertaiment", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Gamepad), FontAwesomeSolid.Gamepad, FontAwesome.FontName);
            CreateCategory("Fuel", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.GasPump), FontAwesomeSolid.GasPump, FontAwesome.FontName);
            CreateCategory("General", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Asterisk), FontAwesomeSolid.Shoppingbasket, FontAwesome.FontName);
            CreateCategory("Gifts", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Gift), FontAwesomeSolid.Gift, FontAwesome.FontName);
            CreateCategory("Vacations", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Suitcase), FontAwesomeSolid.Suitcase, FontAwesome.FontName);
            CreateCategory("Kids", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Child), FontAwesomeSolid.Child, FontAwesome.FontName);
            CreateCategory("Pets", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Cat), FontAwesomeSolid.Cat, FontAwesome.FontName);
            CreateCategory("Sports", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Dumbbell), FontAwesomeSolid.Dumbbell, FontAwesome.FontName);
            CreateCategory("Travel", "Black", TransactionType.Expense, nameof(FontAwesomeSolid.Subway), FontAwesomeSolid.Subway, FontAwesome.FontName);
        }

        private void CreateCategory(string categoryName,string color,TransactionType type, string fontName, string fontGlyph, string fontFamily)
        {
            var icon = new FontIcon()
            {
                FontFamily = fontFamily,
                Name = fontName,
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
            var defaultAccount = new Account
            {
                Description = "Default"
            };
            db.Insert(defaultAccount);
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
                    && fontAwesomeSolidIcons.Exists(c=>c.Name == infoName))
                {
                    fontAwesomeSolidIcons.Add(new FontIcon
                    {
                        Name = infoName,
                        Glyph = info.GetValue(null).ToString(),
                        FontFamily = FontAwesome.FontName,
                    });
                }
            }
            return fontAwesomeSolidIcons;
        }

        private void InsertSampleData()
        {
            if (db.Table<Transaction>().Count() > 0)
                return;

            AddTransaction("Salary", 2500m);
            AddTransaction("Home", 1200m);
            AddTransaction("Shopping", 400m);
            AddTransaction("Groceries", 350m);
            AddTransaction("Loan", 100m);
            AddTransaction("Cash", 1250m);
            AddTransaction("Utilities", 80m);
            AddTransaction("Eating Out", 120m);
            AddTransaction("Entertaiment", 60m);
            AddTransaction("Fuel", 110m);
            AddTransaction("General", 100m);
            AddTransaction("Vacations", 50m);
            AddTransaction("Kids", 400m);
            AddTransaction("Pets", 120m);
            AddTransaction("Sports", 40m);
            AddTransaction("Travel", 30m);
            

        }

        private void AddTransaction(string categoryName,decimal amount)
        {
            var category = db.Table<Category>().FirstOrDefault(c => c.Description == categoryName);
            var icon = db.Table<FontIcon>().FirstOrDefault(c => c.Id == category.FontIconId);
            var account = db.Table<Account>().First();
            var transaction = new Transaction
            {
                AccountId = account.Id,
                CategoryId = category.Id,
                Description= category.Description,
                Type = category.Type,
                TransactionDate = DateTime.Now,
                Amount = amount
            };
            db.Insert(transaction);
        }


    }
}
