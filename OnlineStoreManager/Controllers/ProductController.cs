using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models;
using OnlineStoreManager.Models.ViewModels.Products;

namespace OnlineStoreManager.Controllers
{
    public class ProductController : AppController
    {
        private IPageMaster PageMaster;
        public ProductController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }
        public IActionResult Index(ProductIndexActionModel model)
        {
            PageMaster.SetTabName(model.TabName);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public bool Delete(ProductDeleteActionModel model)
        {
            return model.Execute();
        }


        //  for validations 
        //
        [HttpPost]
        public IActionResult Modify(ProductModifyActionModel model)
        {

            return View(model.Execute());
        }

        [HttpPost]
        public dynamic Add(ProductAddActionModel model)
        {
            if (ModelState.IsValid)
               return model.Execute();

            var modelError = GetModelStateDictionary<ProductAddActionModel>();
            return Result.Fail(null, modelError);
        }

        [HttpPost]
        public dynamic Update(ProductUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = GetModelStateDictionary<ProductAddActionModel>();
            return Result.Fail(null, modelError);
        }
    }
}