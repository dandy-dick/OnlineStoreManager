using OnlineStoreManager.Infracstructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreManager.Database.Models
{
    public class Product
    {
        [Display(Name="Mã sản phẩm")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [Display(Name="Chi phí")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống")]
        [Display(Name="Giá bán")]
        public double Price { get; set; }

        [Display(Name = "Mô tả thêm")]
        public string Description { get; set; } = "";

        [ForeignKey("CartegoryId")]
        [Display(Name = "Thuộc danh mục")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey("SupplierId")]
        [Display(Name = "Thuộc nhà cung cấp")]
        public int? SupplierId { get; set; }
        
        public virtual Supplier Supplier { get; set; }

        public string ImageUrl { get; set; } = "default.jpg";
    }
}