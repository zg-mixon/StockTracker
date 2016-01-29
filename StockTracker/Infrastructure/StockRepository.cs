using StockTracker.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockTracker.Infrastructure
{
    public class StockRepository
    {
        private ApplicationDbContext _db;

        public StockRepository(DbContext db)
        {
            _db = (ApplicationDbContext)db;
        }

        public IQueryable<Stock> FindStocksLike(string searchTerms)
        {
            return from s in _db.Stocks
                   where s.Active &&
                         (s.Name.Contains(searchTerms) ||
                         s.Ticker.StartsWith(searchTerms) ||
                         s.Ticker.StartsWith(searchTerms))
                   select s;
        }
        public IQueryable<Stock> FindStock(string ticker) {
            return from s in _db.Stocks
                   where s.Active && s.Ticker == ticker
                   select s;
        }
        public bool CheckExists(string ticker)
        {
            return _db.Stocks.Any(s => s.Active && s.Ticker == ticker);
        }
    }
}
