using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetProductTypeListForDropDown()
        {
            return _db.ProductType.Select(i => new SelectListItem()
            {
                Text = i.Type,
                Value = i.Id.ToString()
            });
        }

        public void Update(ProductType productType)
        {
            var pTypeFromDb = _db.ProductType.FirstOrDefault(t => t.Id == productType.Id);

            pTypeFromDb.Type = productType.Type;

            _db.SaveChanges();
        }
    }
}
