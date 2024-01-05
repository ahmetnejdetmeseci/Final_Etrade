using Business.Models;
using Business.Results;
using Business.Results.Bases;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly Db _db;


        public ProductService(Db db)
        {
            _db = db;
        }
        public Result Add(ProductModel model)
        {
            var entity = new Product()
            {
                ExpirationDate = model.ExpirationDate,
                Name = model.Name,
                UnitPrice = model.UnitPrice,
                IsDiscontinued = model.IsDiscontinued,
                CategoryId = model.CategoryId,
                
                ProductStores = model.StoreIdsInput?.Select(storeId => new ProductStore()
                {
                    StoreId = storeId   
                }).ToList()
            };

            _db.Products.Add(entity);
            _db.SaveChanges();

            return new SuccessResult("Product added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Products.Include(p => p.ProductStores).SingleOrDefault(p => p.Id == id);
            if(entity is null)
            {
                return new ErrorResult("Product not found!");
            }
            _db.ProductStores.RemoveRange(entity.ProductStores);
            _db.Products.Remove(entity);

            _db.SaveChanges();
            return new SuccessResult("Product Deleted");
        }

        public IQueryable<ProductModel> Query()
        {
            return _db.Products.Include(p => p.ProductStores).Select(p => new ProductModel()
            {
                CategoryId = p.CategoryId,
                Id = p.Id,
                Name = p.Name,
                ExpirationDate = p.ExpirationDate,
                IsDiscontinued = p.IsDiscontinued,
                UnitPrice = p.UnitPrice,
                

                CategoryOutput = p.Category.Name,
                ExpirationDateOutput = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
                IsContinuedOutput = p.IsDiscontinued ? "Yes" : "No",
                UnitPriceOutput = p.UnitPrice.ToString("c"),


                StoresOutput = string.Join("<br />", p.ProductStores.Select(ps => ps.Store.Name)),
                StoreIdsInput = p.ProductStores.Select(ps => ps.StoreId).ToList(),
            }).OrderByDescending(p => p.ExpirationDate);
        }

        public Result Update(ProductModel model)
        {
            var existingEntity = _db.Products.Include(r => r.ProductStores).SingleOrDefault(r => r.Id == model.Id);
            if (existingEntity is not null && existingEntity.ProductStores is not null)
                _db.ProductStores.RemoveRange(existingEntity.ProductStores);

            existingEntity.ExpirationDate = model.ExpirationDate;
            existingEntity.Name = model.Name;
            existingEntity.UnitPrice = model.UnitPrice;
            existingEntity.IsDiscontinued = model.IsDiscontinued;
            existingEntity.CategoryId = model.CategoryId;

            existingEntity.ProductStores = model.StoreIdsInput?.Select(storeId => new ProductStore()
            {
                StoreId = storeId,
            }).ToList();

            _db.Products.Update(existingEntity);
            _db.SaveChanges(); // changes in all DbSets are commited to the database by Unit of Work

            return new SuccessResult("Product updated successfully.");
        }
    }
}
