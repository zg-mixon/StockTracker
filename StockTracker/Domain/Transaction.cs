using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracker.Domain
{
    public class Transaction : IDbEntity, IActivatable
    {

        public int Id { get; set; }

        public ApplicationUser AU { get; set; }

        public Stock Stock { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public string Type { get; set; }

        public bool Active { get; set; } = true;

    }
}