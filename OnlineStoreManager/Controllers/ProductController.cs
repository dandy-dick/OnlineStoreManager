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
using OnlineStoreManager.Models.ViewModels;

namespace OnlineStoreManager.Controllers
{
    public class ProductController : AppController
    {
        readonly IPageMaster PageMaster;
        public ProductController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }

        [Route("")]
        [Route("/Product")]
        public IActionResult Index(ProductIndexActionModel model)
        {
            model.TabName = TabName.Product;
            PageMaster.SetTabName(model.TabName);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(ProductDeleteActionModel model)
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