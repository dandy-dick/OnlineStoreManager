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

        public Result DeleteAll<TTable>(int[] ids) where TTable: class, new()
        {
            Type tableType = typeof(TTable);
            using (var db = new EcomContext())
            {
                // delete all entity matched id
                foreach (var id in ids)
                {
                    var entity = db.Set<TTable>().AsEnumerable()
                        .FirstOrDefault(p => (int)(p as TTable).GetPropertyValue("Id") == id);
                    if (entity != null)
                    {
                        try
                        {
                            db.Set<TTable>().Remove(entity);
                        }
                        catch (Exception e)
                        {
                            return Result.Fail(e.Message);
                        }
                    }
                }

                bool isCreated = db.SaveChanges() > 0;
                if (isCreated)
                    return Result.Success();
                return Result.Fail("Đã có lỗi xảy ra");
            }
        }

        public Result InsertInto<TTable>(TTable model) where TTable : class
        {
            using (var db = new EcomContext())
            {
                try
                {
                    db.Set<TTable>().Add(model);
                }
                catch (Exception e)
                {
                    return Result.Fail(e.Message);
                }

                bool isCreated = db.SaveChanges() > 0;
                if (isCreated)
                    return Result.Success();
                return Result.Fail("Đã có lỗi xảy ra");
            }
        }

        public Result UpdateFrom<TTable>(TTable model) where TTable : class, new()
        {
            Type tableType = typeof(TTable);
            using (var db = new EcomContext())
            {
                // first try to find element
                int id = (int)tableType.GetProperty("Id").GetValue(model, null);

                var entity = db.Set<TTable>().AsEnumerable()
                    .FirstOrDefault(p => (int)(p as TTable).GetPropertyValue("Id") == id);

                // try to update
                try
                {
                    if (entity != null)
                        entity.ObjectAssign(model);
                }
                catch (Exception e)
                {
                    return Result.Fail(e.Message);
                }
                
                bool isCreated = db.SaveChanges() > 0;
                if (isCreated)
                    return Result.Success();
                return Result.Fail("Đã có lỗi xảy ra");
            }
        }

    }
}
