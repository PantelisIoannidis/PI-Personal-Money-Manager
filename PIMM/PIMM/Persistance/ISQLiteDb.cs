using SQLite;

namespace PIMM.Persistance
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetAsyncConnection();

        SQLiteConnection GetConnection();
    }
}