﻿using ShoppingCenter.DataAccess.Data.Repository.IRepository;
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
            Products = new ProductsRepository(_db);
            User = new UserRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IProductTypeRepository ProductType { get; private set; }

        public IProductSizeRepository ProductSize { get; private set; }

        public IProductsRepository Products { get; private set; }

        public IUserRepository User { get; private set; }

        public IOrderHeaderReposiotry OrderHeader { get; private set; }

        public IOrderDeatilsRepository OrderDetails { get; private set; }

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
