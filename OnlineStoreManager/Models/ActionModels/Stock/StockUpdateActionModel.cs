using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class StockUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Update;
        public Stock Stock { get; set; }

        public dynamic Execute()
        {
            var repo = new StockCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
