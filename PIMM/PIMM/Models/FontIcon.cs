using SQLite;

namespace PIMM.Models
{
    public class FontIcon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Description { get; set; }
        public string Glyph { get; set; }
        public string FontFamily { get; set; }
    }
}