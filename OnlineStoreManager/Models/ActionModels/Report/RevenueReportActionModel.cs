using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class RevenueReportActionModel : IControllerActionModel
    {

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public dynamic Execute()
        {
            return new
            {
                revenue = 9999,
                profit = 9999,
                chartData = new int[4] {10,15,20,30}
            };
        }
    }
}
