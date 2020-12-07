using System;
using System.Collections.Generic;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ImportOrderModifyViewModel
    {
        public CRUD Action { get; set; }
        public ImportOrder ImportOrder { get; set; }
    }
}
