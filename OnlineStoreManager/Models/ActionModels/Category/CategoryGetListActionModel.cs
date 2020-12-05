using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class CategoryGetListActionModel : IControllerActionModel
    {
        public int max { get; set; }
        public string search { get; set; }

        public dynamic Execute()
        {
            var repo = new AppRepository();
            var categories = repo.Categories();
            return categories.Where(p => (search == null) ? true : p.Name.ToLower().TiengVietKhongDau()
                                    .Contains(search.ToLower().TiengVietKhongDau()))
                .Select(p => new { id = p.Id, text = p.Name })
                .Take(max);
        }
    }
}
