﻿using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ProductDeleteActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Delete;
        public int[] DeleteIds { get; set; }

        public dynamic Execute()
        {
            var repo = new ProductCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
