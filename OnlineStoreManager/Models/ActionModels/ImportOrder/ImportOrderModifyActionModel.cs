using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ImportOrderModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public ImportOrder ImportOrder { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Update)
                this.ImportOrder = GetImportOrder();

            var viewModel = new ImportOrderModifyViewModel();
            viewModel.ObjectAssign(this);
            return viewModel;
        }

        public ImportOrder GetImportOrder()
        {
            var repo = new AppRepository();
            return repo.ImportOrders()
                .FirstOrDefault(p => p.Id == this.ImportOrder.Id);
        }
    }
}
