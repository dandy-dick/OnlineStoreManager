using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class BestSellingReportActionModel : IControllerActionModel
    {

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public dynamic Execute()
        {
            //res = [ {top, name, quantity, revenue } ]
            return new object[]
            {
                new {
                    top= 1,
                    name= "Quan Jean",
                    quantity= 100,
                    revenue= 1000
                }
            };
        }
    }
}
