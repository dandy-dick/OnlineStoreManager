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
    public class ImportOrder
    {
        [Display(Name = "Mã đơn hàng")]
        public int Id { get; set; }
        
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Ngày giao dự kiến")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [Display(Name = "Chiết khấu")]
        public double Discount { get; set; }

        [Display(Name = "Mô tả đơn hàng")]
        public string Description { get; set; } = "";

        [Display(Name = "Lí do hủy đơn")]
        public string CancelDescription { get; set; } = "";

        [ForeignKey("ImportStatusId")]
        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int ImportStatusId { get; set; }
        public virtual ImportStatus ImportStatus { get; set; }

        [ForeignKey("WarehouseId")]
        [Display(Name = "Thuộc kho hàng")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Exclude("AssignProperties")]
        public virtual ICollection<ImportItem> ImportProducts { get; set; }
    }
}