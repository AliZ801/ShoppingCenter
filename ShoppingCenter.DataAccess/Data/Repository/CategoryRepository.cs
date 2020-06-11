using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var catFromDb = _db.Category.FirstOrDefault(c => c.Id == category.Id);

            catFromDb.CatName = category.CatName;

            _db.SaveChanges();
        }
    }
}
