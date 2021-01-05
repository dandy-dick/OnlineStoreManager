using System;
using System.Collections.Generic;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class OrderModifyViewModel
    {
        public CRUD Action { get; set; }
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
