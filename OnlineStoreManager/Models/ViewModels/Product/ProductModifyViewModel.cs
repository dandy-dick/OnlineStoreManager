using System;
using System.Collections.Generic;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels.Products
{
    public class ProductModifyViewModel
    {
        public CRUD Action { get; set; }
        public Product Product { get; set; }
    }
}
