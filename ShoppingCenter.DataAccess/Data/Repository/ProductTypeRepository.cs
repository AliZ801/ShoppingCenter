using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductType productType)
        {
            var pTypeFromDb = _db.ProductType.FirstOrDefault(t => t.Id == productType.Id);

            pTypeFromDb.Type = productType.Type;
            pTypeFromDb.CategoryId = productType.CategoryId;

            _db.SaveChanges();
        }
    }
}
