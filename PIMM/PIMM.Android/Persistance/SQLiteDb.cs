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
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "PIMMv001.db3");

            return new SQLiteAsyncConnection(path);
        }

    }
}