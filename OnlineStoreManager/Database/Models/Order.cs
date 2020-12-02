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
    public class Order
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public string DeliveryAddress { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverName { get; set; }
        public string CancelDescription { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

        [ForeignKey("OrderStatusId")]
        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }

}