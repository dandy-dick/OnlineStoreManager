using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        readonly IPageMaster _pageMaster;
        readonly IWebHostEnvironment _hostingEnvironment;


        public ProductController(IPageMaster _pageMaster, IWebHostEnvironment hostingEnvironment)
        {
            this._pageMaster = _pageMaster;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("")]
        [Route("/Product")]
        public IActionResult Index(ProductIndexActionModel model)
        {
            model.TabName = TabName.Product;
            _pageMaster.SetTabName(model.TabName);

            var viewModel = model.Execute();
            ViewBag.TabName = model.TabName;
            return View(viewModel);
        }

        [HttpPost]
        public Result Delete(ProductDeleteActionModel model)
        {
            return model.Execute();
        }

        [HttpPost]
        public IActionResult Modify(ProductModifyActionModel model)
        {
            ViewBag.ModelStateDictionary = this.ModelStateDictionary<ProductModifyActionModel>();
            return View(model.Execute());
        }

        [HttpPost]
        public async Task<Result> Update(ProductUpdateActionModel model)
        {
            if (ModelState.IsValid)
            {
                model.RootPath = this._hostingEnvironment.WebRootPath;
                return await model.Execute();
            }

            var modelError = ModelStateDictionary<ProductUpdateActionModel>();
            return Result.Fail(null, modelError);
        }
    }
}