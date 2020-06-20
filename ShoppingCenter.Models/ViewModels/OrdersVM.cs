using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.Models.ViewModels
{
    public class OrdersVM
    {
        public OrderHeader OrderHeader { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
