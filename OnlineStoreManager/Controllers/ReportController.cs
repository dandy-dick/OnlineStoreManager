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
    public class ReportController : AppController
    {
        readonly IPageMaster _pageMaster;


        public ReportController(IPageMaster _pageMaster)
        {
            this._pageMaster = _pageMaster;
        }

        [Route("/Revenue")]
        public IActionResult Index()
        {
            return View();
        }
    }
}