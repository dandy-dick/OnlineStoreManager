using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels.Products
{
    public class ProductAddActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Insert;
        public Product Product { get; set; }

        public dynamic Execute()
        {
            var repo = new ProductCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
