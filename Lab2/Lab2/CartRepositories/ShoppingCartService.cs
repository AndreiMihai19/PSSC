using Lab2.Entities;
using Lab2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.CartRepositories
{
    internal class ShoppingCartService
    {
        private ShoppingCartRepository cartRepository;
        private ProductRepository productRepository;

        public ShoppingCartService(ShoppingCartRepository cartRepository, ProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
        }

        public ShoppingCart CreateShoppingCart()
        {
            var cart = ShoppingCart.Create();
            cartRepository.AddShoppingCart(cart);
            return cart;
        }

        public void AddProductToCart(ShoppingCart cart, string productCode, int quantity)
        {
            var product = productRepository.GetProductByCode(productCode);

            if (product == null)
                throw new InvalidOperationException("Produsul nu există.");

            cart.AddItem(product, quantity);
        }

        public void ValidateCart(ShoppingCart cart)
        {
            cart.Validate();
        }

        public void SetShippingAddress(ShoppingCart cart, Address address)
        {
            cart.SetShippingAddress(address);
        }
    }
}
