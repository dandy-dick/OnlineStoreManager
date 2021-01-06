using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreManager.Models.ViewModels
{
    public class OrderUpdateActionModel : IControllerActionModel
    {
        public CRUD Action { get; set; } = CRUD.Insert;
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public dynamic Execute()
        {
            if (Action == CRUD.Insert)
                InsertOrder();
            else
                UpdateOrder();
            return Result.Success();
        }

        private void InsertOrder()
        {
            using (var _db = new EcomContext())
            {
                _db.Orders.Add(Order);
                foreach (var item in OrderItems)
                {
                    var product = _db.Products.FirstOrDefault(
                        p => p.Name == item.Product.Name
                    );

                    if (product != null)
                    {
                        _db.OrderItems.Add(new OrderItem
                        {
                            OrderId = Order.Id,
                            ProductId = product.Id,
                            Quantity = item.Quantity
                        });
                    }
                }
                _db.SaveChanges();
            }
        }

        private void UpdateOrder()
        {
            using (var _db = new EcomContext())
            {
                // Remove all Old-Order-items
                //
                var _orderItems = _db.OrderItems.Where(
                    p => p.OrderId == Order.Id
                );
                _db.OrderItems.RemoveRange(_orderItems);
                _db.SaveChanges();

                // update Order
                //
                var order = _db.Orders.FirstOrDefault(
                    p => p.Id == Order.Id
                );
                order.ObjectAssign(Order);

                // Insert new items 
                //
                foreach (var item in OrderItems)
                {
                    var product = _db.Products.FirstOrDefault(
                        p => p.Name == item.Product.Name
                    );

                    if (product != null)
                    {
                        _db.OrderItems.Add(new OrderItem
                        {
                            OrderId = Order.Id,
                            ProductId = product.Id,
                            Quantity = item.Quantity
                        });
                    }
                }
                _db.SaveChanges();
            }
        }
    }
}
