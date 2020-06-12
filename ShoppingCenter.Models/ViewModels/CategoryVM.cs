using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.Models.ViewModels
{
    public class CategoryVM
    {
        public Category Category { get; set; }

        public ProductType ProductType { get; set; }

        public ProductSize ProductSize { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> ProductTypeList { get; set; }

        public IEnumerable<SelectListItem> ProductSizeList { get; set; }
    }
}
