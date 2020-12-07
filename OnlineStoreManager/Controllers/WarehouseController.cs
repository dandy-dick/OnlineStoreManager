using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models.ViewModels;

namespace OnlineStoreManager.Controllers
{
    public class WarehouseController: AppController
    {
        readonly IPageMaster PageMaster;
        public WarehouseController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }

        [Route("/Warehouse")]
        public IActionResult Index(WarehouseIndexActionModel model)
        {
            model.TabName = TabName.Warehouse;
            PageMaster.SetTabName(model.TabName);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(WarehouseDeleteActionModel model)
        {
            return model.Execute();
        }


        //  for validations 
        //
        [HttpPost]
        public IActionResult Modify(WarehouseModifyActionModel model)
        {
            return View(model.Execute());
        }

        [HttpPost]
        public dynamic Add(WarehouseAddActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = GetModelStateDictionary<WarehouseAddActionModel>();
            return Result.Fail(null, modelError);
        }

        [HttpPost]
        public dynamic Update(WarehouseUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = GetModelStateDictionary<WarehouseAddActionModel>();
            return Result.Fail(null, modelError);
        }

        public dynamic GetList(string request)
        {
            var obj = JsonConvert.DeserializeObject<GetListRequestModel>(request);
            var model = new WarehouseGetListActionModel();
            model.ObjectAssign(obj);
            
            var result = model.Execute();
            return new
            {
                status = "success",
                records = result
            };
        }
    }
}