using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models.ViewModels;

namespace OnlineStoreManager.Controllers
{
    public class ImportOrderController: AppController
    {
        readonly IPageMaster PageMaster;
        public ImportOrderController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }

        [Route("/ImportOrder")]
        public IActionResult Index(ImportOrderIndexActionModel model)
        {
            model.TabName = TabName.ImportOrder;
            PageMaster.SetTabName(TabName.ImportOrder);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(ImportOrderDeleteActionModel model)
        {
            return model.Execute();
        }


        //  for validations 
        //
        [HttpPost]
        public IActionResult Modify(ImportOrderModifyActionModel model)
        {
            return View(model.Execute());
        }

        [HttpPost]
        public dynamic Add(ImportOrderAddActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = ModelStateDictionary<ImportOrderAddActionModel>();
            return Result.Fail(null, modelError);
        }

        [HttpPost]
        public dynamic Update(ImportOrderUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = ModelStateDictionary<ImportOrderAddActionModel>();
            return Result.Fail(null, modelError);
        }
    }
}