using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ICategoryRepository Category { get; }

        IProductTypeRepository ProductType { get; }

        IProductSizeRepository ProductSize { get; }

        IProductsRepository Products { get; }

        void Save();
    }
}
