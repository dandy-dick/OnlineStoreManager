using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineStoreManager.Database;
using OnlineStoreManager.Database.Models;
using OnlineStoreManager.Infracstructure;
using OnlineStoreManager.Repository;

namespace OnlineStoreManager.Models.ViewModels
{
    public class RequestProduct: Product
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string SupplierName { get; set; }
    }

    public class ProductUpdateActionModel
    {
        public CRUD Action { get; set; } = CRUD.Insert;
        public RequestProduct RequestProduct { get; set; }
        public IFormFile FormFile { get; set; }

        public string RootPath { get; set; }

        public async Task<Result> Execute()
        {
            var errors = new Dictionary<string, ModelStateError>();
            using (var _db = new EcomContext())
            {
                var category = _db.Categories.FirstOrDefault(p => p.Name == this.RequestProduct.CategoryName);

                var supplier = _db.Suppliers.FirstOrDefault(p => p.Name == this.RequestProduct.SupplierName);

                #region kiem tra du lieu truoc khi execute  
                if (category == null)
                {
                    errors.Add("RequestProduct.CategoryName", new ModelStateError
                    {
                        ErrorMessages = "Không tồn tại danh mục: " 
                            + RequestProduct.CategoryName
                    });
                }
                if (supplier == null)
                {
                    errors.Add("RequestProduct.SupplierName", new ModelStateError
                    {
                        ErrorMessages = "Không tồn tại nhà cung cấp : " 
                            + RequestProduct.SupplierName
                    });
                }
                if (errors.Count() > 0)
                    return Result.Fail(null, errors);
                #endregion

                #region hop le: them san pham vao csdl

                RequestProduct.CategoryId = category.Id;
                RequestProduct.SupplierId = supplier.Id;

                var newProduct = new Product();
                newProduct.AssignProperties(RequestProduct);

                if (this.FormFile != null)
                    newProduct.ImageUrl = await this.SavePhoto();

                if (Action == CRUD.Insert)
                    this.InsertProduct(newProduct);
                else
                    this.UpdateProduct(newProduct);

                #endregion

                return Result.Success();
            }
        }

        private void InsertProduct(Product product)
        {
            var repo = new AppRepository();
            repo.InsertInto(product);
        }

        private void UpdateProduct(Product product)
        {
            var repo = new AppRepository();
            repo.UpdateFrom(product);
        }

        private async Task<string> SavePhoto()
        {
            string productImagesPath = Path.Combine(this.RootPath, "_product-images");
            if (!(Directory.Exists(productImagesPath)))
            {
                Directory.CreateDirectory(productImagesPath);
            }

            string photoName = Guid.NewGuid().ToString() + this.FormFile.FileName;
            string filePath = Path.Combine(productImagesPath,photoName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await this.FormFile.CopyToAsync(stream);
            }

            return photoName;
        }

    }
}
