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
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.Products = repo.Products();
            this.Categories = repo.Categories();
            this.Suppliers = repo.Suppliers();

            return 0;
        }
    }
}
