using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineStoreManager.Database;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models.ViewModels;
using System.Linq;

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
            ViewBag.TabName = TabName.Order;

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
        public dynamic Update(OrderUpdateActionModel model)
        {
            if (ModelState.IsValid)
                return model.Execute();

            var modelError = ModelStateDictionary<OrderUpdateActionModel>();
            return Result.Fail(null, modelError);
        }

        public dynamic GetOrderItems(string orderid)
        {
            using (var _db = new EcomContext())
            {
                var items = _db.OrderItems
                    .Include(p => p.Product)
                    .Where(p => p.OrderId == orderid)
                    .Select(p => new
                    {
                        id = p.Id,
                        quantity = p.Quantity,
                        productname = p.Product.Name
                    })
                    .ToArray();

                return items;
            }
        }
    }
}