using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Entities
{
    internal class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return Product.Price * Quantity;
        }
    }
}
