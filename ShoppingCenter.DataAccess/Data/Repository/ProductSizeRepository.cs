using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository
{
    public class ProductSizeRepository : Repository<ProductSize>, IProductSizeRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductSizeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        IEnumerable<SelectListItem> IProductSizeRepository.GetProductSizeForDropDownList()
        {
            return _db.ProductSize.Select(s => new SelectListItem()
            {
                Text = s.Size,
                Value = s.Id.ToString()
            });
        }

        public void update(ProductSize productSize)
        {
            var sizeFromDb = _db.ProductSize.FirstOrDefault(s => s.Id == productSize.Id);

            sizeFromDb.Size = productSize.Size;

            _db.SaveChanges();
        }
    }
}
