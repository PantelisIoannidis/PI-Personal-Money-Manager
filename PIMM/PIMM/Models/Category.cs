﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIMM.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int FontIconId { get; set; }
        public string Color { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }

        public static explicit operator Category(List<Category> v)
        {
            throw new NotImplementedException();
        }
    }
}