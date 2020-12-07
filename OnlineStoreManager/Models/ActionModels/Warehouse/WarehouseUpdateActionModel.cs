using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class WarehouseUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Update;
        public Warehouse Warehouse { get; set; }

        public dynamic Execute()
        {
            var repo = new WarehouseCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
