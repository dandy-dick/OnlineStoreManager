using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class ProductIndexRepository: IRepository
    {
        public IEnumerable<Product> Products { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.Products = repo.Products();
            return 0;
        }
    }
}
