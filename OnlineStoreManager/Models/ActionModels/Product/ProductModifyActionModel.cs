using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels.Products
{
    public class ProductModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public Product Product { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Update)
                this.Product = GetProduct();

            var viewModel = new ProductModifyViewModel();
            viewModel.ObjectAssign(this);
            return viewModel;
        }

        public Product GetProduct()
        {
            var repo = new AppRepository();
            return repo.Products()
                .FirstOrDefault(p => p.Id == this.Product.Id);
        }
    }
}
