using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();

        void Update(Category category);
    }
}
