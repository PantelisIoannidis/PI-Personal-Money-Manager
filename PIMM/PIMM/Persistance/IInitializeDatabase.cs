using System.Threading.Tasks;

namespace PIMM.Persistance
{
    public interface IInitializeDatabase
    {
        void CreateCategoriesAndAccounts();
        Task EraseDatabase();
        Task PopulateDatabase();
    }
}