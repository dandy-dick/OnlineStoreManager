using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class SupplierGetListActionModel : IControllerActionModel
    {
        public dynamic Execute()
        {
            var repo = new AppRepository();
            var suppliers = repo.Suppliers();
            return suppliers.Select(p => p.Name).ToArray();
        }
    }
}
