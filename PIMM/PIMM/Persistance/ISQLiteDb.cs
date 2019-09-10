using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Persistance
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetAsyncConnection();
        SQLiteConnection GetConnection();
    }
}
