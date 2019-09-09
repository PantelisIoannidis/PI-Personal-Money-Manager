using System;
using System.IO;
using SQLite;
using Xamarin.Forms;

using Foundation;
using UIKit;
using PIMM.iOS.Persistance;
using PIMM.Persistance;

[assembly: Dependency(typeof(SQLiteDb))]

namespace PIMM.iOS.Persistance
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