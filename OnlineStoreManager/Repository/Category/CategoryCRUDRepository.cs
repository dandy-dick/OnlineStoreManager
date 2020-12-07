using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class CategoryCRUDRepository: IRepository
    {
        public CRUD Action { get; set; }
        public int[] DeleteIds { get; set; }
        public Category Category { get; set; }

        public dynamic Execute()
        {
            return Action switch
            {
                CRUD.Insert => InsertCategory(),
                CRUD.Delete => DeleteCategorys(),
                _ => UpdateCategory(),
            };
        }

        public Result InsertCategory()
        {
            var repo = new AppRepository();
            return repo.InsertInto(Category);
        }

        public Result UpdateCategory()
        {
            var repo = new AppRepository();
            return repo.UpdateFrom(Category);
        }

        public Result DeleteCategorys()
        {
            var repo = new AppRepository();
            return repo.DeleteAll<Category>(DeleteIds);
        }
    }
}
