using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class SupplierCRUDRepository: IRepository
    {
        public CRUD Action { get; set; }
        public int[] DeleteIds { get; set; }
        public Supplier Supplier { get; set; }

        public dynamic Execute()
        {
            return Action switch
            {
                CRUD.Insert => InsertSupplier(),
                CRUD.Delete => DeleteSuppliers(),
                _ => UpdateSupplier(),
            };
        }

        public Result InsertSupplier()
        {
            var repo = new AppRepository();
            return repo.InsertInto(Supplier);
        }

        public Result UpdateSupplier()
        {
            var repo = new AppRepository();
            return repo.UpdateFrom(Supplier);
        }

        public Result DeleteSuppliers()
        {
            var repo = new AppRepository();
            return repo.DeleteAll<Supplier>(DeleteIds);
        }
    }
}
