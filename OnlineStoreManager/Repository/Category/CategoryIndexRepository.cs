using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;

namespace OnlineStoreManager.Repository
{
    public class CategoryIndexRepository: IRepository
    {
        public IEnumerable<Category> Categories { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            this.Categories = repo.Categories();

            return 0;
        }
    }
}
