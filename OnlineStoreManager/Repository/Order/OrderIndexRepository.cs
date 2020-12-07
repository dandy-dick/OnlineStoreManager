using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class OrderIndexRepository: IRepository
    {
        public IEnumerable<Order> Orders { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.Orders = repo.Orders();
            return 0;
        }
    }
}
