using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class SupplierGetListActionModel : IControllerActionModel
    {
        public int max { get; set; }
        public string search { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            var suppliers = repo.Suppliers();
            return suppliers.Where(p => search == null ? true : p.Name.ToLower().TiengVietKhongDau()
                                    .Contains(search.ToLower().TiengVietKhongDau()))
                .Select(p => new { id = p.Id, text = p.Name })
                .Take(max);
        }
    }
}
