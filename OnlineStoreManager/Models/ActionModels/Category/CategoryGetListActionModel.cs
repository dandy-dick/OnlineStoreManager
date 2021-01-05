using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class CategoryGetListActionModel : IControllerActionModel
    {
        public dynamic Execute()
        {
            var repo = new AppRepository();
            var categories = repo.Categories();
            return categories.Select(p => p.Name).ToArray();
        }
    }
}
