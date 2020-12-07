using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class StockModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public Stock Stock { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Update)
                this.Stock = GetStock();

            var viewModel = new StockModifyViewModel();
            viewModel.ObjectAssign(this);
            return viewModel;
        }

        public Stock GetStock()
        {
            var repo = new AppRepository();
            return repo.Stocks()
                .FirstOrDefault(p => p.Id == this.Stock.Id);
        }
    }
}
