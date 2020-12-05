using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public enum CRUD
    {
        Insert = 1,
        Delete = 2,
        Update = 3
    }

    public class ProductCRUDRepository: IRepository
    {
        public CRUD Action { get; set; }
        public int[] DeleteIds { get; set; }
        public Product Product { get; set; }

        public dynamic Execute()
        {
            switch (Action)
            {
                case CRUD.Insert:
                    return InsertProduct();
                case CRUD.Delete:
                    return DeleteProducts();
                case CRUD.Update:
                default:
                    return UpdateProduct();
            }
        }

        public Result InsertProduct()
        {
            var repo = new AppRepository();
            return repo.InsertInto(Product);
        }

        public Result UpdateProduct()
        {
            var repo = new AppRepository();
            return repo.UpdateFrom(Product);

        }

        public Result DeleteProducts()
        {
            var repo = new AppRepository();
            return repo.DeleteAll<Product>(DeleteIds);
        }
    }
}
