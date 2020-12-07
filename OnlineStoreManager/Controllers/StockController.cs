using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models.ViewModels;

namespace OnlineStoreManager.Controllers
{
    public class StockController: AppController
    {
        readonly IPageMaster PageMaster;
        public StockController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }

        [Route("/Stock")]
        public IActionResult Index(StockIndexActionModel model)
        {
            model.TabName = TabName.Stock;
            PageMaster.SetTabName(TabName.Stock);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(StockDeleteActionModel model)
        {
            return model.Execute();
        }


        //  for validations 
        //
        [HttpPost]
        public IActionResult Modify(StockModifyActionModel model)
        {
            return View(model.Execute());
        }

        [HttpPost]
        public dynamic Add(StockAddActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = GetModelStateDictionary<StockAddActionModel>();
            return Result.Fail(null, modelError);
        }

        [HttpPost]
        public dynamic Update(StockUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = GetModelStateDictionary<StockAddActionModel>();
            return Result.Fail(null, modelError);
        }
    }
}