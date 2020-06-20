using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.Models.ViewModels
{
    public class CartVM
    {
        public IList<Products> ProductsList { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
