using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models.ViewModels;

namespace OnlineStoreManager.Controllers
{
    public class OrderController: AppController
    {
        readonly IPageMaster PageMaster;
        public OrderController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }

        [Route("/Order")]
        public IActionResult Index(OrderIndexActionModel model)
        {
            PageMaster.SetTabName(model.TabName);
            ViewBag.TabName = model.TabName;

            var viewModel = model.Execute();
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(OrderDeleteActionModel model)
        {
            return model.Execute();
        }


        //  for validations 
        //
        [HttpPost]
        public IActionResult Modify(OrderModifyActionModel model)
        {
            return View(model.Execute());
        }

        [HttpPost]
        public dynamic Add(OrderAddActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = GetModelStateDictionary<OrderAddActionModel>();
            return Result.Fail(null, modelError);
        }

        [HttpPost]
        public dynamic Update(OrderUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = GetModelStateDictionary<OrderAddActionModel>();
            return Result.Fail(null, modelError);
        }
    }
}