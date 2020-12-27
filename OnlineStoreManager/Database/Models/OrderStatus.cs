using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Models;

namespace OnlineStoreManager.Database.Models
{
    public enum OrderStatusEnum
    {
        Paid = 1,
        Exported = 2,
        Completed = 3,
        Canceled = 4,
        Demurrage = 5   // trễ hạn giao hàng dự kiến
    }

    public class OrderStatus
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

}