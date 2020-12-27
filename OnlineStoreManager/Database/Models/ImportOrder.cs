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
        public int? Id { get; set; }
        
        [Display(Name = "Ngày tạo")]
        public string CreatedDate { get; set; }

        [Display(Name = "Ngày hoàn thành")]
        public string CompletedDate { get; set; }

        [Display(Name = "Ngày giao dự kiến")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ExpectedDeliveryDate { get; set; }

        [Display(Name = "Mô tả đơn hàng")]
        public string Description { get; set; } = "";

        [ForeignKey("ImportStatusId")]
        [Display(Name = "Trạng thái")]
        public int? ImportStatusId { get; set; }
        public virtual ImportStatus ImportStatus { get; set; }

        [Exclude("AssignProperties")]
        public virtual ICollection<ImportItem> ImportItems { get; set; }
    }
}