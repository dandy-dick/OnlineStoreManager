using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class WarehouseIndexRepository: IRepository
    {
        public IEnumerable<Warehouse> Warehouses { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.Warehouses = repo.Warehouses();

            return 0;
        }
    }
}
