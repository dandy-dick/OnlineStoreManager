using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class OrderAddActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Insert;
        public Order Order { get; set; }

        public dynamic Execute()
        {
            var repo = new OrderCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
