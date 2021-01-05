using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class OrderIndexActionModel: IControllerActionModel
    {
        public TabName TabName { get; set; } = TabName.Order;
        public string SearchText { get; set; }
        
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; } = 0;

        public string FromDate { get; set; }
        public string ToDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");


        public IEnumerable<Order> Orders { get; set; }

        public dynamic Execute()
        {
            // Truy xuất dữ liệu từ Database
            //
            var repo = new OrderIndexRepository();
            repo.ObjectAssign(this);
            repo.Execute();
            this.Orders = repo.Orders;
            // Pagination & Search
            //
            this.SetModelData();    
            //  Trả về data cho ViewModel
            //
            var viewModel = new OrderIndexViewModel();
            viewModel.ObjectAssign(this);
            
            return viewModel;
        }

        private void SetModelData()
        {
            this.TotalItems = this.Orders.Count();
            // Apply Search
            if (this.SearchText != null)
                this.Orders = this.ApplySearch(this.Orders).Cast<Order>();
            // Pagination
            this.Orders = this.Pagination(this.Orders).Cast<Order>();
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
            return src.Skip(this.PageSize * (this.CurrentPage - 1)).Take(this.PageSize);
        }
    }
}
