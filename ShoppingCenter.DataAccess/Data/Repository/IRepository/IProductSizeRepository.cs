using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface IProductSizeRepository : IRepository<ProductSize>
    {
        IEnumerable<SelectListItem> GetProductSizeForDropDownList();

        void update(ProductSize productSize);
    }
}
