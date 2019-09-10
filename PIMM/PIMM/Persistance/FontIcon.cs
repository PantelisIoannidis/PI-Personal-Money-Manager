using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Persistance
{
    public class FontIcon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Glyph { get; set; }
        public string FontFamily { get; set; }
    }
}
