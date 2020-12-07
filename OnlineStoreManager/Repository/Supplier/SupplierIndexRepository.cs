using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class SupplierIndexRepository: IRepository
    {
        public IEnumerable<Supplier> Suppliers { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.Suppliers = repo.Suppliers();

            return 0;
        }
    }
}
