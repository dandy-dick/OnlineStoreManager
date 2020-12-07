using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class OrderCRUDRepository: IRepository
    {
        public CRUD Action { get; set; }
        public int[] DeleteIds { get; set; }
        public Order Order { get; set; }

        public dynamic Execute()
        {
            return Action switch
            {
                CRUD.Insert => InsertOrder(),
                CRUD.Delete => DeleteOrders(),
                _ => UpdateOrder(),
            };
        }

        public Result InsertOrder()
        {
            var repo = new AppRepository();
            return repo.InsertInto(Order);
        }

        public Result UpdateOrder()
        {
            var repo = new AppRepository();
            return repo.UpdateFrom(Order);

        }

        public Result DeleteOrders()
        {
            var repo = new AppRepository();
            return repo.DeleteAll<Order>(DeleteIds);
        }
    }
}
