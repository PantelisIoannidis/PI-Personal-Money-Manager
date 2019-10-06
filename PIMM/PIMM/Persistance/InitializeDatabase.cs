using PIMM.Helpers;
using PIMM.Models;
using PIMM.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PIMM.Persistance
{
    public class InitializeDatabase : IInitializeDatabase
    {
        private SQLiteConnection db;
        private List<FontIcon> fontList = new List<FontIcon>();
        private IPageService pageService;

        public InitializeDatabase(IPageService pageService)
        {
            this.pageService = pageService;
            db = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        public async Task PopulateDatabase()
        {
            if (IsAnEmptyDatabase())
            {
                var response = await pageService.DisplayAlert("New Database", "The database is empty. Do you want some sample transactions?", "Yes", "No");
                if (response)
                {
                    InsertSampleData();
                }
            }
        }

        public void CreateCategoriesAndAccounts()
        {
            CreateTables();
            if (IsAnewDatabase())
            {
                CreateCetegories();
                CreateAcounts();
                PrepareIcons();
                CreateIcons();
            }
        }

        private void CreateTables()
        {
            db.CreateTable<FontIcon>();
            db.CreateTable<Account>();
            db.CreateTable<Category>();
            db.CreateTable<Transaction>();
        }

        public async Task EraseDatabase()
        {
            db.Execute("DELETE FROM 'Transaction'");
            db.Execute("DELETE FROM Account");
            db.Execute("DELETE FROM Category");
            db.Execute("DELETE FROM FontIcon");
            CreateCategoriesAndAccounts();
        }

        private bool IsAnewDatabase()
        {
            if ((db.Table<Account>().Count() <= 0)
                && (db.Table<Category>().Count() <= 0)
                && (db.Table<FontIcon>().Count() <= 0))
                return true;
            else
                return false;
        }

        private bool IsAnEmptyDatabase()
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
            string[] randomColors;
            randomColors = new string[] {
                    "#0a4d05","#39054d",
                    "#540000","#541600","#543900","#4a5400","#235400","#085400","#00540f","#00542b",
                    "#005054","#003254","#260054","#320054","#4d0054","#8a8411","#54003c","#540016" };
            var app = (Application.Current as App);
            if (app.Properties.ContainsKey(Themes.ThemeKey))
                if (app.Properties[Themes.ThemeKey].ToString() != Themes.Light)
                    randomColors = new string[] {
                    "#22db0d","#dbc70d",
                    "#db100d","#db630d","#dbab0d","#bcdb0d","#6ddb0d","#0ddb8f","#0dd1db","#0d85db",
                    "#0d36db","#5c0ddb","#ab0ddb","#db0db2","#db0d7b","#db0d47","#db0d0d","#db3a0d"
            };
            int i = 0;

            CreateCategory("Salary", randomColors[i++], TransactionType.Income, nameof(FontAwesomeSolid.dollar_sign), FontAwesomeSolid.dollar_sign, FontAwesome.FontName);
            CreateCategory("Cash", randomColors[i++], TransactionType.Income, nameof(FontAwesomeSolid.piggy_bank), FontAwesomeSolid.piggy_bank, FontAwesome.FontName);

            CreateCategory("Home", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.home), FontAwesomeSolid.home, FontAwesome.FontName);
            CreateCategory("Shopping", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.shopping_cart), FontAwesomeSolid.shopping_cart, FontAwesome.FontName);
            CreateCategory("Groceries", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.shopping_basket), FontAwesomeSolid.shopping_basket, FontAwesome.FontName);
            CreateCategory("Loan", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.hand_holding_usd), FontAwesomeSolid.hand_holding_usd, FontAwesome.FontName);
            CreateCategory("Utilities", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.tint), FontAwesomeSolid.tint, FontAwesome.FontName);
            CreateCategory("Clothes", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.tshirt), FontAwesomeSolid.tshirt, FontAwesome.FontName);
            CreateCategory("Eating Out", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.utensils), FontAwesomeSolid.utensils, FontAwesome.FontName);
            CreateCategory("Entertainment", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.gamepad), FontAwesomeSolid.gamepad, FontAwesome.FontName);
            CreateCategory("Fuel", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.gas_pump), FontAwesomeSolid.gas_pump, FontAwesome.FontName);
            CreateCategory("General", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.asterisk), FontAwesomeSolid.shopping_basket, FontAwesome.FontName);
            CreateCategory("Gifts", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.gift), FontAwesomeSolid.gift, FontAwesome.FontName);
            CreateCategory("Vacations", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.suitcase), FontAwesomeSolid.suitcase, FontAwesome.FontName);
            CreateCategory("Kids", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.child), FontAwesomeSolid.child, FontAwesome.FontName);
            CreateCategory("Pets", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.cat), FontAwesomeSolid.cat, FontAwesome.FontName);
            CreateCategory("Sports", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.dumbbell), FontAwesomeSolid.dumbbell, FontAwesome.FontName);
            CreateCategory("Travel", randomColors[i], TransactionType.Expense, nameof(FontAwesomeSolid.subway), FontAwesomeSolid.subway, FontAwesome.FontName);
        }

        private void CreateCategory(string categoryName, string color, TransactionType type, string fontName, string fontGlyph, string fontFamily)
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
                Color = color,
                Type = type,
                Description = categoryName
            };
            db.Insert(category);
        }

        private void CreateAcounts()
        {
            var account = new Account
            {
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

        private void PrepareIcons()
        {
            FieldInfo[] field_infos = typeof(FontAwesomeSolid).GetFields(
                    BindingFlags.FlattenHierarchy |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Static);
            foreach (FieldInfo info in field_infos)
            {
                var infoName = info.ToString().Replace("System.String ", "");
                if (!infoName.Contains("FontName")
                    && !fontList.Exists(c => c.Description == infoName))
                {
                    fontList.Add(new FontIcon
                    {
                        Description = infoName,
                        Glyph = info.GetValue(null).ToString(),
                        FontFamily = FontAwesome.FontName,
                    });
                }
            }
        }

        private void CreateIcons()
        {
            foreach (var item in fontList)
            {
                if (db.Table<FontIcon>().Count(x => x.Id == item.Id) <= 0)
                    db.Insert(item);
            }
        }

        private void InsertSampleData()
        {
            if (db.Table<Transaction>().Count() > 0)
                return;

            var now = DateTime.Now;

            AddTransaction("Salary", 2500m, 0, now);
            AddTransaction("Home", 1200m, 0, now);
            AddTransaction("Shopping", 400m, 0, now.AddDays(-1));
            AddTransaction("Groceries", 350m, 0, now.AddDays(-2));
            AddTransaction("Loan", 100m, 1, now.AddDays(-1));
            AddTransaction("Cash", 1250m, 1, now);
            AddTransaction("Utilities", 80m, 0, now.AddDays(-10));
            AddTransaction("Eating Out", 120m, 0, now.AddDays(-11));
            AddTransaction("Entertainment", 60m, 0, now.AddDays(-12));
            AddTransaction("Fuel", 110m, 0, now.AddYears(-2));
            AddTransaction("General", 100m, 0, now.AddYears(-2));
            AddTransaction("Vacations", 50m, 1);
            AddTransaction("Kids", 400m, 0);
            AddTransaction("Pets", 120m, 0);
            AddTransaction("Sports", 40m, 0);
            AddTransaction("Travel", 30m, 0);
        }

        private void AddTransaction(string categoryName, decimal amount, int accountNo, DateTime? dt = null)
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
                Description = category.Description,
                Type = category.Type,
                TransactionDate = dt.Value,
                Amount = amount
            };
            db.Insert(transaction);
        }
    }
}