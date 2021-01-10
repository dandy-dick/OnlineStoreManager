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
    public class RevenueReportModel
    {
        public double Revenue { get; set; }
        public double Profit { get; set; }
    }
    public class RevenueReportActionModel : IControllerActionModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public dynamic Execute()
        {
            /* Make sure client send correct data or fail (data from Index Action):
                + year are same for FromDate & ToDate
                + FromDate & ToDate in format 'yyyy-MM-dd' & not null
             */

            using (var db = new EcomContext())
            {
                // build query to get month's orderitems
                //
                string _fromDate = null, _toDate = null;
                var query = db.Orders.Include(p => p.OrderItems)
                    .Where(p =>
                        (_fromDate == null || p.CreatedDate.CompareTo(_fromDate) >= 0)
                        && (_toDate == null || p.CreatedDate.CompareTo(_toDate) <= 0)
                    ).SelectMany(p => p.OrderItems);

                // loop 12 month and calculate revenue, profit
                //
                DateTime startDate = DateTime.Parse(FromDate),
                    endDate = DateTime.Parse(ToDate);
                int startMonth = startDate.Month, endMonth = endDate.Month, year = startDate.Year;
                var chartData = new double[14];
                double revenue = 0, profit = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (startMonth <= i && i <= endMonth)
                    {
                        int startDay = (i == startMonth) ? startDate.Day : 1,
                        endDay = (i == endMonth) ? endDate.Day : MaxDayOfMonth(i, year);

                        _fromDate = year + "-" + ((i < 10) ? "0" + i : i.ToString()) + "-" + ((startDay < 10) ? "0" + startDay : startDay.ToString());
                        _toDate = year + "-" + ((i < 10) ? "0" + i : i.ToString()) + "-" + ((endDay < 10) ? "0" + endDay : endDay.ToString());

                        var monthReport = query.Select(p => new RevenueReportModel
                        {
                            Revenue = query.Sum(p => p.Quantity * p.Product.Price),
                            Profit = query.Sum(p => p.Quantity * (p.Product.Price - p.Product.Cost))
                        })
                        .FirstOrDefault();

                        if (monthReport == null)
                            monthReport = new RevenueReportModel();

                        revenue += monthReport.Revenue;
                        profit += monthReport.Profit;

                        chartData[i] = monthReport.Revenue;
                    }
                }

                chartData[13] = chartData[12] + 20; // chỉ để vẽ đồ thị đẹp hơn
                return new
                {
                    revenue,
                    profit,
                    chartData
                };
            }
        }

        private int MaxDayOfMonth(int month, int year)
        {
            DateTime date = DateTime.Parse(year + "-" + month + "-01");
            date = date.AddMonths(1);
            date = date.AddDays(-(date.Day));

            return date.Day;
        }
    }
}
