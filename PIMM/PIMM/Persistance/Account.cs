using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Persistance
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
