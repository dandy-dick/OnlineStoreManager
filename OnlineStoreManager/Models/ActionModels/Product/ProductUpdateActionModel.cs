using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ProductUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Update;
        public Product Product { get; set; }

        public dynamic Execute()
        {
            var repo = new ProductCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
