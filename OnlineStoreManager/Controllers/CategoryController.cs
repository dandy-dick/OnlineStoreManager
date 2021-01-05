using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models.ViewModels;

namespace OnlineStoreManager.Controllers
{
    public class CategoryController: AppController
    {
        readonly IPageMaster PageMaster;
        public CategoryController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }

        [Route("/Category")]
        public IActionResult Index(CategoryIndexActionModel model)
        {
            model.TabName = TabName.Category;
            PageMaster.SetTabName(model.TabName);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(CategoryDeleteActionModel model)
        {
            return model.Execute();
        }


        //  for validations 
        //
        [HttpPost]
        public IActionResult Modify(CategoryModifyActionModel model)
        {
            return View(model.Execute());
        }

        [HttpPost]
        public dynamic Add(CategoryAddActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = ModelStateDictionary<CategoryAddActionModel>();
            return Result.Fail(null, modelError);
        }

        [HttpPost]
        public dynamic Update(CategoryUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = ModelStateDictionary<CategoryAddActionModel>();
            return Result.Fail(null, modelError);
        }

        public dynamic GetList()
        {
            var model = new CategoryGetListActionModel();
            var result = model.Execute();
            return new
            {
                records = result
            };
        }
    }
}