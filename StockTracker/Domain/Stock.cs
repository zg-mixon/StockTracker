using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockTracker.Domain
{
    public class Stock : IActivatable
    {

        [Key]
        public string Ticker { get; set; }

        public decimal Price { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<Transaction> Transaction { get; set; }

        public decimal Change { get; set; }

        public decimal OpenPrice { get; set; }

        public bool Active { get; set; } = true;
        
    }
}