using Microsoft.AspNet.Identity;
using StockTracker.Domain;
using StockTracker.Infrastructure;
using StockTracker.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracker.Services
{
    public class StockService
    {

        private StockRepository _stockRepo;
        private TransactionRepository _transactionRepo;
        private ApplicationUserManager _userRepo;


        public StockService(StockRepository stockrepo, TransactionRepository transRepo, ApplicationUserManager userRepo)
        {
            _stockRepo = stockrepo;
            _transactionRepo = transRepo;
            _userRepo = userRepo;
        }

        public IList<StockDTO> Search(string searchTerms)
        {
            return (from s in _stockRepo.FindStocksLike(searchTerms)
                    select new StockDTO()
                    {
                        Ticker = s.Ticker,
                        Name = s.Name,
                        Price = s.Price,
                        OpenPrice = s.OpenPrice
                    }).ToList();
        }

        public ExpandedStockDTO GetStockWithTransactions(string ticker, string username)
        {
            return (from s in _stockRepo.FindStock(ticker)
                    select new ExpandedStockDTO()
                    {
                        Price = s.Price,
                        Name = s.Name,
                        Ticker = s.Ticker,
                        OpenPrice = s.OpenPrice,
                        LowPrice = s.LowPrice,
                        HighPrice = s.HighPrice,
                        Description = s.Description,
                        Transactions = (from t in s.Transaction
                                        where t.AU.UserName == username
                                        orderby t.DateTime descending
                                        select new TransactionDTO()
                                        {
                                            Price = t.Price,
                                            Quantity = t.Quantity,
                                            DateCreated = t.DateTime
                                        }).ToList()
                    }).FirstOrDefault();
        }

        public TransactionDTO Buy(string ticker, int quantity, string username)
        {

            var stock = _stockRepo.FindStock(ticker).FirstOrDefault();

            var user = _userRepo.FindByName(username);

            var transaction = new Transaction()
            {
                AU = user,
                Stock = stock,
                Price = stock.Price,
                Quantity = quantity,
                Type = "buy"
            };

            _transactionRepo.Add(transaction);
            _transactionRepo.SaveChanges();

            return new TransactionDTO()
            {
                Ticker = transaction.Stock.Ticker,
                Price = transaction.Price,
                Quantity = transaction.Quantity,
                DateCreated = transaction.DateTime
            };
            }

            public TransactionDTO Sell(string ticker, int quantity, string username)
        {

            var stock = _stockRepo.FindStock(ticker).FirstOrDefault();

            var user = _userRepo.FindByName(username);

            var transaction = new Transaction()
            {
                AU = user,
                Stock = stock,
                Price = stock.Price,
                Quantity = -quantity,
                Type = "sell"
            };

            _transactionRepo.Add(transaction);
            _transactionRepo.SaveChanges();

            return new TransactionDTO()
            {
                Ticker = transaction.Stock.Ticker,
                Price = transaction.Price,
                Quantity = transaction.Quantity,
                DateCreated = transaction.DateTime
            };


        }

        public bool CheckExists(string ticker) {
            return _stockRepo.CheckExists(ticker);
        }
    }
}