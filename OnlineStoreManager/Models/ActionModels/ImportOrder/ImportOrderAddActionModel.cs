using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ImportOrderAddActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Insert;
        public ImportOrder ImportOrder { get; set; }

        public dynamic Execute()
        {
            var repo = new ImportOrderCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
