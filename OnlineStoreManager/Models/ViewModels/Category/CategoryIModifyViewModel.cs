using System;
using System.Collections.Generic;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class CategoryModifyViewModel
    {
        public CRUD Action { get; set; }
        public Category Category { get; set; }
    }
}
