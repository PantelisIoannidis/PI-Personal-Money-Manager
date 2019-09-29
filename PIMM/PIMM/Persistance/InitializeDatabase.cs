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
using PIMM.ViewModels;

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
            if(app.Properties.ContainsKey(Themes.ThemeKey))
                if(app.Properties[Themes.ThemeKey].ToString()!= Themes.Light)
                 randomColors = new string[] {
                    "#22db0d","#dbc70d",
                    "#db100d","#db630d","#dbab0d","#bcdb0d","#6ddb0d","#0ddb8f","#0dd1db","#0d85db",
                    "#0d36db","#5c0ddb","#ab0ddb","#db0db2","#db0d7b","#db0d47","#db0d0d","#db3a0d"
            };
            int i = 0;

            CreateCategory("Salary", randomColors[i++], TransactionType.Income, nameof(FontAwesomeSolid.Dollarsign), FontAwesomeSolid.Dollarsign, FontAwesome.FontName);
            CreateCategory("Cash", randomColors[i++], TransactionType.Income, nameof(FontAwesomeSolid.PiggyBank), FontAwesomeSolid.PiggyBank, FontAwesome.FontName);

            CreateCategory("Home", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Home), FontAwesomeSolid.Home, FontAwesome.FontName);
            CreateCategory("Shopping", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Shoppingcart), FontAwesomeSolid.Shoppingcart, FontAwesome.FontName);
            CreateCategory("Groceries", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Shoppingbasket), FontAwesomeSolid.Shoppingbasket, FontAwesome.FontName);
            CreateCategory("Loan", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.HandHoldingUsd), FontAwesomeSolid.HandHoldingUsd, FontAwesome.FontName);
            CreateCategory("Utilities", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Tint), FontAwesomeSolid.Tint, FontAwesome.FontName);
            CreateCategory("Clothes", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.TShirt), FontAwesomeSolid.TShirt, FontAwesome.FontName);
            CreateCategory("Eating Out", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Utensils), FontAwesomeSolid.Utensils, FontAwesome.FontName);
            CreateCategory("Entertainment", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Gamepad), FontAwesomeSolid.Gamepad, FontAwesome.FontName);
            CreateCategory("Fuel", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.GasPump), FontAwesomeSolid.GasPump, FontAwesome.FontName);
            CreateCategory("General", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Asterisk), FontAwesomeSolid.Shoppingbasket, FontAwesome.FontName);
            CreateCategory("Gifts", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Gift), FontAwesomeSolid.Gift, FontAwesome.FontName);
            CreateCategory("Vacations", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Suitcase), FontAwesomeSolid.Suitcase, FontAwesome.FontName);
            CreateCategory("Kids", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Child), FontAwesomeSolid.Child, FontAwesome.FontName);
            CreateCategory("Pets", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Cat), FontAwesomeSolid.Cat, FontAwesome.FontName);
            CreateCategory("Sports", randomColors[i++], TransactionType.Expense, nameof(FontAwesomeSolid.Dumbbell), FontAwesomeSolid.Dumbbell, FontAwesome.FontName);
            CreateCategory("Travel", randomColors[i], TransactionType.Expense, nameof(FontAwesomeSolid.Subway), FontAwesomeSolid.Subway, FontAwesome.FontName);
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
                    && fontAwesomeSolidIcons.Exists(c => c.Description == infoName))
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
