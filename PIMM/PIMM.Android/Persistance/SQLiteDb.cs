using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using PIMM.Droid.Persistance;
using PIMM.Persistance;

[assembly: Dependency(typeof(SQLiteDb))]

namespace PIMM.Droid.Persistance
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return new SQLiteAsyncConnection(GetPath());
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }

        public string GetPath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "PIMMv002.db3");

            return path;
        }

    }
}