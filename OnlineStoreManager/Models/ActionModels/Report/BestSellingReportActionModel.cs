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
    public class ReportOrderItem: OrderItem
    {
        public double Revenue { get; set; }
        public string ProductName { get; set; }
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
            var lookUp = db.Orders.Include(p => p.OrderItems)
                .Where(p =>
                    (FromDate == null || p.CreatedDate.CompareTo(FromDate) >= 0)
                    && (ToDate == null || p.CreatedDate.CompareTo(ToDate) <= 0)
                )
                .SelectMany(p => p.OrderItems)
                .Select(p => new ReportOrderItem
                {
                    Quantity = p.Quantity,
                    Revenue = p.Quantity * p.Product.Price,
                    ProductId = p.ProductId,
                    ProductName = p.Product.Name
                })
                .ToLookup(p => p.ProductId);

            var productRevenueSummary = new List<ReportOrderItem>();
            foreach (var item in lookUp)
            {
                var _temp = item.First();
                productRevenueSummary.Add(new ReportOrderItem
                { 
                    ProductId = _temp.ProductId,
                    ProductName = _temp.ProductName,
                    Quantity = item.Sum(p => p.Quantity),
                    Revenue = item.Sum(p => p.Revenue)
                });
            }

            var result = productRevenueSummary
                .OrderByDescending(p => p.Revenue)
                .Take(10)
                .ToArray();

            //if (productRevenueSummary.Count() > 10)
            //{
            //    var left = productRevenueSummary
            //        .OrderBy(p => p.Revenue)
            //        .Skip(10);
            //    result[10] = new ReportOrderItem
            //    {
            //        ProductId = null,
            //        ProductName = "Các sản phẩm khác",
            //        Quantity = left.Sum(p => p.Quantity),
            //        Revenue = left.Sum(p => p.Revenue)
            //    };
            //}

            return result;
        }
    }
}
