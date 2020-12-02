using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineStoreManager.Database.Models;

namespace OnlineStoreManager.Database
{
    public class EcomContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImportProduct> ImportProducts { get; set; }
        public DbSet<ImportReceipt> ImportReceipts { get; set; }
        public DbSet<ImportStatus> ImportStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public EcomContext() { }
        public EcomContext(DbContextOptions<EcomContext> options):base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = @"data source=.\SQLEXPRESS;";
            connectionString += "initial catalog=OnlineStoreManager;";
            connectionString += "integrated security=true;";

            builder.UseSqlServer(connectionString);
            builder.UseLazyLoadingProxies();
            base.OnConfiguring(builder);
        }
    }
}
