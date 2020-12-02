using Microsoft.EntityFrameworkCore;
using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreManager.Repository
{
    public class AppRepository
    {
        public List<Product> Products()
        {
            using (var db = new EcomContext())
            {
                return db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .ToList();
            }
        }

        public List<Category> Categories()
        {
            using (var db = new EcomContext())
            {
                return db.Categories.ToList();
            }
        }

        public List<Category> GetCategoriesWithProducts()
        {
            using (var db = new EcomContext())
            {
                return db.Categories.Include(p => p.Products).ToList();
            }
        }

        public List<Supplier> Suppliers()
        {
            using (var db = new EcomContext())
            {
                return db.Suppliers.ToList();
            }
        }
    }
}
