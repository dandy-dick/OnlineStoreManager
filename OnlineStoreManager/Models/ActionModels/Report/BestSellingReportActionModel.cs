using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class OrderItemRevenue
    {
        public string ProductName { get; set; }
        public double Revenue { get; set; }
        public double Quantity { get; set; }
    }


    public class BestSellingReportActionModel : IControllerActionModel
    {

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public dynamic Execute()
        {
            /* Make sure client send correct data or fail (data from Index Action):
                + year are same for FromDate & ToDate
                + FromDate & ToDate in format 'yyyy-MM-dd' & not null
             */

            using var db = new EcomContext();
            var orderItems = db.Orders
                .Where(p =>
                    (p.CreatedDate.CompareTo(FromDate) >= 0)
                    && (p.CreatedDate.CompareTo(ToDate) <= 0)
                )
                .SelectMany(p => p.OrderItems, (o, item) => new OrderItemRevenue
                {
                    ProductName = item.Product.Name,
                    Revenue = item.Quantity * item.Product.Price,
                    Quantity = item.Quantity
                });


            var lookUp = orderItems.ToLookup(p => p.ProductName);

            var productRevenueSummary = new List<OrderItemRevenue>();
            foreach (var item in lookUp)
            {
                var _temp = item.First();
                productRevenueSummary.Add(new OrderItemRevenue
                {
                    ProductName = _temp.ProductName,
                    Quantity = item.Sum(p => p.Quantity),
                    Revenue = item.Sum(p => p.Revenue)
                });
            }

            return productRevenueSummary
                .OrderByDescending(p => p.Revenue)
                .Take(10)
                .ToArray();
        }
    }
}
