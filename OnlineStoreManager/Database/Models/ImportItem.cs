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
    public class ImportItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;

        // một khi đã vào kho
        // thi thông tin về đơn hàng không được phép thay đổi nữa
        // => clone lại thông tin product của đơn hàng

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("ImportReceiptId")]
        public int ImportOrderId { get; set; }
        public virtual ImportOrder ImportOrder { get; set; }
    }
}