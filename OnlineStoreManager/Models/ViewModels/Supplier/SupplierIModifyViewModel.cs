using System;
using System.Collections.Generic;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class SupplierModifyViewModel
    {
        public CRUD Action { get; set; }
        public Supplier Supplier { get; set; }
    }
}
