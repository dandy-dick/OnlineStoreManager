using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models.ViewModels.Products;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ProductModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public Product Product { get; set; } = new Product();

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
            using var _db = new EcomContext();
            return _db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.Id == this.Product.Id);
        }
    }
}
