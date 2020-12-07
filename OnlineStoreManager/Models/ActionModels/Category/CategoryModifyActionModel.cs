using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class CategoryModifyActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; }
        public Category Category { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Update)
                this.Category = GetCategory();

            var viewModel = new CategoryModifyViewModel();
            viewModel.ObjectAssign(this);
            return viewModel;
        }

        public Category GetCategory()
        {
            var repo = new AppRepository();
            return repo.Categories()
                .FirstOrDefault(p => p.Id == this.Category.Id);
        }
    }
}
