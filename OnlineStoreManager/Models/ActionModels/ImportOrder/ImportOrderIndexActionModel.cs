using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ImportOrderIndexActionModel: IControllerActionModel
    {
        public TabName TabName { get; set; } = TabName.ImportOrder;
        public string SearchText { get; set; }
        
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; } = 0;

        public IEnumerable<ImportOrder> ImportOrders { get; set; }

        public dynamic Execute()
        {
            // Truy xuất dữ liệu từ Database
            //
            var repo = new ImportOrderIndexRepository();
            repo.Execute();
            this.ObjectAssign(repo);

            this.SetModelData();    
            
            //  Trả về data cho ViewModel
            //
            var viewModel = new ImportOrderIndexViewModel();
            viewModel.ObjectAssign(this);
            
            return viewModel;
        }

        private void SetModelData()
        {
            this.TotalItems = this.ImportOrders.Count();
            // Apply Search
            if (this.SearchText != null)
                this.ImportOrders = this.ApplySearch(this.ImportOrders).Cast<ImportOrder>();
            // Pagination
            this.ImportOrders = this.Pagination(this.ImportOrders).Cast<ImportOrder>();
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
