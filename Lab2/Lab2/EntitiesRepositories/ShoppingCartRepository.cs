using Lab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Repositories
{
    internal class ShoppingCartRepository
    {
        private List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

        public ShoppingCart GetShoppingCart(Guid id)
        {
            return shoppingCarts.Find(cart => cart.Id == id);
        }

        public void AddShoppingCart(ShoppingCart cart)
        {
            shoppingCarts.Add(cart);
        }
    }
}
