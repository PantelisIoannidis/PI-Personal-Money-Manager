using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Persistance
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
    }
}
