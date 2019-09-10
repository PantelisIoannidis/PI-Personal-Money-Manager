﻿using System;
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
            var path = Path.Combine(documentsPath, "PIMMv001.db3");

            return path;
        }

    }
}