using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracker.Services
{
    public class StockDTO
    {
        
        //Only include what we will display in the Angular on the DTO
        public string Ticker { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal OpenPrice { get; set; }
    }
}