using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class OrderModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public Order Order { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Update)
                this.Order = GetOrder();

            var viewModel = new OrderModifyViewModel();
            viewModel.ObjectAssign(this);
            return viewModel;
        }

        public Order GetOrder()
        {
            var repo = new AppRepository();
            return repo.Orders()
                .FirstOrDefault(p => p.Id == this.Order.Id);
        }
    }
}
