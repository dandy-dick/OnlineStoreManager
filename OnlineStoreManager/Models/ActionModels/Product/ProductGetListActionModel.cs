using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ProductGetListActionModel : IControllerActionModel
    {
        public dynamic Execute()
        {
            var repo = new AppRepository();
            return repo.Products().Select(p => p.Name).ToArray();
        }
    }
}
