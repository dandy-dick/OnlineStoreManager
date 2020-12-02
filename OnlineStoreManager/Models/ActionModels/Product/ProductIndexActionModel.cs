using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels.Products
{
    public class ProductIndexActionModel: IControllerActionModel
    {
        public TabName TabName { get; set; } = TabName.Product;
        public string SearchText { get; set; }
        
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; } = 0;

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }

        public dynamic Execute()
        {
            // Truy xuất dữ liệu từ Database
            //
            var repo = new ProductIndexRepository();
            repo.Execute();
            this.ObjectAssign(repo);

            // Vì có tận 3 tab, cần set data tùy tabname
            //
            this.SetModelData();    
            
            //  Trả về data cho ViewModel
            //
            var viewModel = new ProductIndexViewModel();
            viewModel.ObjectAssign(this);
            
            return viewModel;
        }

        private void SetModelData()
        {
            if (TabName == TabName.Product)
                this.SetProductTabData();
            else if (TabName == TabName.Category)
                this.SetCategoryTabData();
            else if (TabName == TabName.Supplier)
                this.SetSupplierTabData();
        }

        private void SetProductTabData()
        {
            this.TotalItems = this.Products.Count();
            // Apply Search
            if (this.SearchText != null)
                this.Products = this.ApplySearch(this.Products).Cast<Product>();
            // Pagination
            this.Products = this.Pagination(this.Products).Cast<Product>();
        }

        private void SetCategoryTabData()
        {
            this.TotalItems = this.Categories.Count();
            // Apply Search
            if (this.SearchText != null)
                this.Categories = this.ApplySearch(this.Categories).Cast<Category>();
            // Pagination
            this.Categories = this.Pagination(this.Categories).Cast<Category>();
        }
        
        private void SetSupplierTabData()
        {
            this.TotalItems = this.Suppliers.Count();
            // Apply Search
            if (this.SearchText != null)
                this.Suppliers = this.ApplySearch(this.Suppliers).Cast<Supplier>();
            // Pagination
            this.Suppliers = this.Pagination(this.Suppliers).Cast<Supplier>();
        }

        private IEnumerable<dynamic> ApplySearch(IEnumerable<dynamic> src)
        {
            string search = this.SearchText.TiengVietKhongDau().ToLower();
            return src.Where(p =>
            {
                return ((string)p.Name).TiengVietKhongDau().ToLower().Contains(search)
                        || ((string)p.Description).TiengVietKhongDau().ToLower().Contains(search);
            });
        }

        private IEnumerable<dynamic> Pagination(IEnumerable<dynamic> src)
        {
            var beforeCast = src.Skip(this.PageSize * (this.CurrentPage - 1)).Take(this.PageSize);
            return src.Skip(this.PageSize * (this.CurrentPage - 1)).Take(this.PageSize);
        }
    }
}
