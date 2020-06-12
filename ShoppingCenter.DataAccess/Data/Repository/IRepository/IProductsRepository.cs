using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface IProductsRepository : IRepository<Products>
    {
        void Update(Products products);
    }
}
