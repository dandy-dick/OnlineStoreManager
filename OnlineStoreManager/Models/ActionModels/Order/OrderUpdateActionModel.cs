using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class OrderUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Update;
        public Order Order { get; set; }

        public dynamic Execute()
        {
            var repo = new OrderCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
