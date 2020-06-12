using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            ProductType = new ProductTypeRepository(_db);
            ProductSize = new ProductSizeRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IProductTypeRepository ProductType { get; private set; }

        public IProductSizeRepository ProductSize { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
