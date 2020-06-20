using ShoppingCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataAccess.Data.Repository.IRepository
{
    public interface IOrderHeaderReposiotry : IRepository<OrderHeader>
    {
        void ChangeOrderStatus(int OrderHeaderId, string OrderStatus);
    }
}
