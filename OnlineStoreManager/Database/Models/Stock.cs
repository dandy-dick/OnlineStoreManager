using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class Stock
    {
        [Display(Name="Id")]
        public int Id { get; set; }

        [Display(Name = "Tồn kho")]
        public int Quantity { get; set; }

        // một khi đã vào kho
        // thi thông tin về đơn hàng không được phép thay đổi nữa
        // => clone lại thông tin product của đơn hàng
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Chi phí")]
        public double Cost { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string Description { get; set; } = "";

        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [ForeignKey("WarehouseId")]
        [Display(Name = "Thuộc kho hàng")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
    
}