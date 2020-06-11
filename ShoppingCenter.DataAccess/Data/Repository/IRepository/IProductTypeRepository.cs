using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        void Update(ProductType productType);
    }
}
