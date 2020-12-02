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
    public class ImportReceipt
    {
        public int Id { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public string CancelDescription { get; set; }

        [ForeignKey("ImportStatusId")]
        public int ImportStatusId { get; set; }
        public virtual ImportStatus ImportStatus { get; set; }

        [ForeignKey("WarehouseId")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        public virtual ICollection<ImportProduct> ImportProducts { get; set; }
    }
}