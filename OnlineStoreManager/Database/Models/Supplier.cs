using OnlineStoreManager.Infracstructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreManager.Database.Models
{
   
    public class Supplier
    {
        [Display(Name = "Mã nhà cung cấp")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [Display(Name = "Tên nhà cung cấp")]
        public string Name { get; set; }

        [Display(Name = "Mô tả thêm")]
        public string Description { get; set; } = "";

        [Exclude("AssignProperties")]
        public virtual ICollection<Product> Products { get; set; }
    }
}