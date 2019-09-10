using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Windows.Storage;
using PIMM.UWP;
using PIMM.UWP.Persistance;
using PIMM.Persistance;

[assembly: Dependency(typeof(SQLiteDb))]

namespace PIMM.UWP.Persistance
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

        private string GetPath()
        {
            var documentsPath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentsPath, "PIMMv001.db3");
            return path;
        }

    }
}
