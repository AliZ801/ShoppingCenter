using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        IEnumerable<SelectListItem> GetProductTypeListForDropDown();

        void Update(ProductType productType);
    }
}
