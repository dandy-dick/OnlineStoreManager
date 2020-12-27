using OnlineStoreManager.Infracstructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreManager.Database.Models
{
    public class Category
    {
        [Display(Name="Mã danh mục")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Mô tả thêm")]
        public string Description { get; set; }

        [Exclude("AssignProperties")]
        public virtual ICollection<Product> Products { get; set; }
    }

}