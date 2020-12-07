using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class SupplierAddActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Insert;
        public Supplier Supplier { get; set; }

        public dynamic Execute()
        {
            var repo = new SupplierCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
