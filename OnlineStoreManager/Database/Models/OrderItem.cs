
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
    public class OrderItem
    {
        public int? Id { get; set; }
        public int Quantity { get; set; } = 0;

        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("OrderId")]
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

}