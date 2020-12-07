using System;
using System.Collections.Generic;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;

namespace OnlineStoreManager.Models.ViewModels
{
    public class WarehouseIndexViewModel
    {
        public TabName TabName { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public IEnumerable<Warehouse> Warehouses { get; set; }
    }
}
