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
    public enum ImportStatusEnum
    {
        Waiting = 1, // đang 
        Imported = 2,
        Canceled = 3,
        Demurrage = 4   // trễ hạn giao hàng dự kiến
    }

    public class ImportStatus
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
