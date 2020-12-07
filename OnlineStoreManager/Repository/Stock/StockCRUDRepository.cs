using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class StockCRUDRepository: IRepository
    {
        public CRUD Action { get; set; }
        public int[] DeleteIds { get; set; }
        public Stock Stock { get; set; }

        public dynamic Execute()
        {
            return Action switch
            {
                CRUD.Insert => InsertStock(),
                CRUD.Delete => DeleteStocks(),
                _ => UpdateStock(),
            };
        }

        public Result InsertStock()
        {
            var repo = new AppRepository();
            return repo.InsertInto(Stock);
        }

        public Result UpdateStock()
        {
            var repo = new AppRepository();
            return repo.UpdateFrom(Stock);

        }

        public Result DeleteStocks()
        {
            var repo = new AppRepository();
            return repo.DeleteAll<Stock>(DeleteIds);
        }
    }
}
