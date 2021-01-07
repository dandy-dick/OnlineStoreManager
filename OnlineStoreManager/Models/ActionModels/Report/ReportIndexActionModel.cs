using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class ReportIndexActionModel : IControllerActionModel
    {

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public dynamic Execute()
        {
            if (FromDate == null)
            {
                FromDate = DateTime.Now.Year + "-01-01";
                ToDate = DateTime.Now.Year + "-12-31";
            }

            return Result.Success();
        }
    }
}
