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
using OnlineStoreManager.Models.ViewModels.Products;

namespace OnlineStoreManager.Controllers
{
    public class SupplierController: AppController
    {
        private IPageMaster PageMaster;
        public SupplierController(IPageMaster _pageMaster)
        {
            this.PageMaster = _pageMaster;

        }

        public dynamic GetList(string request)
        {
            var obj = JsonConvert.DeserializeObject<GetListRequest>(request);
            var model = new SupplierGetListActionModel();
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