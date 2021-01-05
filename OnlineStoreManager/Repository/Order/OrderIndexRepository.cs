using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreManager.Repository
{
    public class OrderIndexRepository: IRepository
    {
        public IEnumerable<Order> Orders { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public dynamic Execute()
        {
            using (var db = new EcomContext())
            {
                this.Orders = db.Orders.Where(p =>
                    (FromDate == null || p.CreatedDate.CompareTo(FromDate) >= 0)
                    && (ToDate == null || p.CreatedDate.CompareTo(ToDate) <= 0)
                ).ToList();
            }

            return 0;
        }
    }
}
