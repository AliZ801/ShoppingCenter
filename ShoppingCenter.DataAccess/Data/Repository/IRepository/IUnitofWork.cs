using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ICategoryRepository Category { get; }

        void Save();
    }
}
