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
    public class SupplierController : AppController
    {
        readonly IPageMaster PageMaster;
        public SupplierController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;
        }

        [Route("/Supplier")]
        public IActionResult Index(SupplierIndexActionModel model)
        {
            model.TabName = TabName.Supplier;
            PageMaster.SetTabName(model.TabName);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(SupplierDeleteActionModel model)
        {
            return model.Execute();
        }

        [HttpPost]
        public IActionResult Modify(SupplierModifyActionModel model)
        {
            return View(model.Execute());
        }

        [HttpPost]
        public dynamic Add(SupplierAddActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = ModelStateDictionary<SupplierAddActionModel>();
            return Result.Fail(null, modelError);
        }

        [HttpPost]
        public dynamic Update(SupplierUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = ModelStateDictionary<SupplierAddActionModel>();
            return Result.Fail(null, modelError);
        }

        public dynamic GetList()
        {
            var model = new SupplierGetListActionModel();
            var result = model.Execute();
            return new
            {
                records = result
            };
        }
    }
}