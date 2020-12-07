using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class WarehouseModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public Warehouse Warehouse { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Update)
                this.Warehouse = GetWarehouse();

            var viewModel = new WarehouseModifyViewModel();
            viewModel.ObjectAssign(this);
            return viewModel;
        }

        public Warehouse GetWarehouse()
        {
            var repo = new AppRepository();
            return repo.Warehouses()
                .FirstOrDefault(p => p.Id == this.Warehouse.Id);
        }
    }
}
