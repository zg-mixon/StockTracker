using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracker.Services.Models
{
    public class ExpandedStockDTO : StockDTO
    {
        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public string Description { get; set; }

        public IList<TransactionDTO> Transactions { get; set; }
    }
}