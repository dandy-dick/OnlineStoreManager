using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class ImportOrderCRUDRepository: IRepository
    {
        public CRUD Action { get; set; }
        public int[] DeleteIds { get; set; }
        public ImportOrder ImportOrder { get; set; }

        public dynamic Execute()
        {
            return Action switch
            {
                CRUD.Insert => InsertImportOrder(),
                CRUD.Delete => DeleteImportOrders(),
                _ => UpdateImportOrder(),
            };
        }

        public Result InsertImportOrder()
        {
            var repo = new AppRepository();
            return repo.InsertInto(ImportOrder);
        }

        public Result UpdateImportOrder()
        {
            var repo = new AppRepository();
            return repo.UpdateFrom(ImportOrder);

        }

        public Result DeleteImportOrders()
        {
            var repo = new AppRepository();
            return repo.DeleteAll<ImportOrder>(DeleteIds);
        }
    }
}
