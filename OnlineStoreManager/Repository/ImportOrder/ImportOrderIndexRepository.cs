using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class ImportOrderIndexRepository: IRepository
    {
        public IEnumerable<ImportOrder> ImportOrders { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.ImportOrders = repo.ImportOrders();
            return 0;
        }
    }
}
