using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderReposiotry
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void ChangeOrderStatus(int OrderHeaderId, string OrderStatus)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(o => o.Id == OrderHeaderId);
            orderFromDb.Status = OrderStatus;

            _db.SaveChanges();
        }
    }
}
