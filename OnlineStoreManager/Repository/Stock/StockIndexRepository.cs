using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class StockIndexRepository: IRepository
    {
        public IEnumerable<Stock> Stocks { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.Stocks = repo.Stocks();
            return 0;
        }
    }
}
