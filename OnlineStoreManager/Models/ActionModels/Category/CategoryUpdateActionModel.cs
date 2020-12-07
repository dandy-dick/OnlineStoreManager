using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class CategoryUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Update;
        public Category Category { get; set; }

        public dynamic Execute()
        {
            var repo = new CategoryCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
