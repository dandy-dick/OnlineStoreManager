using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ImportOrderUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Update;
        public ImportOrder ImportOrder { get; set; }

        public dynamic Execute()
        {
            var repo = new ImportOrderCRUDRepository();
            repo.ObjectAssign(this);
            return repo.Execute();
        }
    }
}
