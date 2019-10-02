using SQLite;

namespace PIMM.Models
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Description { get; set; }
        public string Color { get; set; }
    }
}