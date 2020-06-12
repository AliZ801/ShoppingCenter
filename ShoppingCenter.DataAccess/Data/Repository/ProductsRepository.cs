using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository
{
    public class ProductsRepository : Repository<Products>, IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Products products)
        {
            var pFromDb = _db.Products.FirstOrDefault(p => p.Id == products.Id);

            pFromDb.Name = products.Name;
            pFromDb.Description = products.Description;
            pFromDb.Price = products.Price;
            pFromDb.ImageURL = products.ImageURL;
            pFromDb.CategoryId = products.CategoryId;
            pFromDb.ProductTypeId = products.ProductTypeId;
            pFromDb.ProductSizeId = products.ProductSizeId;
            pFromDb.Status = products.Status;

            _db.SaveChanges();
        }
    }
}
