using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class SupplierModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public Supplier Supplier { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Update)
                this.Supplier = GetSupplier();

            var viewModel = new SupplierModifyViewModel();
            viewModel.ObjectAssign(this);
            return viewModel;
        }

        public Supplier GetSupplier()
        {
            var repo = new AppRepository();
            return repo.Suppliers()
                .FirstOrDefault(p => p.Id == this.Supplier.Id);
        }
    }
}
