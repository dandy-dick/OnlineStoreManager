using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class StockIndexActionModel: IControllerActionModel
    {
        public TabName TabName { get; set; } = TabName.Stock;
        public string SearchText { get; set; }
        
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; } = 0;

        public IEnumerable<Stock> Stocks { get; set; }

        public dynamic Execute()
        {
            // Truy xuất dữ liệu từ Database
            //
            var repo = new StockIndexRepository();
            repo.Execute();
            this.ObjectAssign(repo);

            this.SetModelData();    
            
            //  Trả về data cho ViewModel
            //
            var viewModel = new StockIndexViewModel();
            viewModel.ObjectAssign(this);
            
            return viewModel;
        }

        private void SetModelData()
        {
            this.TotalItems = this.Stocks.Count();
            // Apply Search
            if (this.SearchText != null)
                this.Stocks = this.ApplySearch(this.Stocks).Cast<Stock>();
            // Pagination
            this.Stocks = this.Pagination(this.Stocks).Cast<Stock>();
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
