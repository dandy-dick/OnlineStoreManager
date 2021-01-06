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
                var year = DateTime.Now.Year;
                year = year - 1;

                FromDate = new DateTime(year, 1, 1).ToString("yyyy-MM-dd");
            }

            if (ToDate == null)
            {
                ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return Result.Success();
        }
    }
}
