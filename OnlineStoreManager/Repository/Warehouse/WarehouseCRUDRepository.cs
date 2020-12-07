using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class WarehouseCRUDRepository: IRepository
    {
        public CRUD Action { get; set; }
        public int[] DeleteIds { get; set; }
        public Warehouse Warehouse { get; set; }

        public dynamic Execute()
        {
            return Action switch
            {
                CRUD.Insert => InsertWarehouse(),
                CRUD.Delete => DeleteWarehouses(),
                _ => UpdateWarehouse(),
            };
        }

        public Result InsertWarehouse()
        {
            var repo = new AppRepository();
            return repo.InsertInto(Warehouse);
        }

        public Result UpdateWarehouse()
        {
            var repo = new AppRepository();
            return repo.UpdateFrom(Warehouse);
        }

        public Result DeleteWarehouses()
        {
            var repo = new AppRepository();
            return repo.DeleteAll<Warehouse>(DeleteIds);
        }
    }
}
