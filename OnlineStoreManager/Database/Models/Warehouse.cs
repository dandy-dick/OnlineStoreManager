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
    public class Warehouse
    {
        [Display(Name = "Mã kho hàng")]
        public int Id { get; set; }

        [Display(Name = "Tên kho hàng")]
        public string Name { get; set; }

        [Display(Name = "Mô tả thêm")]
        public string Description { get; set; } = "";

        [Exclude("AssignProperties")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}