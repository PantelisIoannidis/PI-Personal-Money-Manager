using SQLite;
using System;

namespace PIMM.Models
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