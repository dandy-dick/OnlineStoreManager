using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class SupplierUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Update;
        public Supplier Supplier { get; set; }

        public dynamic Execute()
        {
            var repo = new SupplierCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
