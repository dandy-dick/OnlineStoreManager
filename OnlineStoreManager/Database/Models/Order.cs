using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineStoreManager.Infracstructure;

namespace OnlineStoreManager.Database.Models
{
    public class Order
    {
        [Display(Name="Mã đơn hàng")]
        public int Id { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Mô tả đơn hàng")]
        public string Description { get; set; } = "";

        [Display(Name = "Địa chỉ giao")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Sđt người nhận")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ReceiverPhoneNumber { get; set; }

        [Display(Name = "Tên người nhận")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ReceiverName { get; set; }

        [Display(Name = "Lí do hủy đơn")]
        public string CancelDescription { get; set; } = "";

        [Display(Name = "Ngày thanh toán")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Ngày giao dự kiến")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [ForeignKey("OrderStatusId")]
        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        [Exclude("AssignProperties")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}