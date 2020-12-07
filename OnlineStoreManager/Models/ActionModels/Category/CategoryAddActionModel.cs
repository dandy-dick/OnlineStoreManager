using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class CategoryAddActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Insert;
        public Category Category { get; set; }

        public dynamic Execute()
        {
            var repo = new CategoryCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
