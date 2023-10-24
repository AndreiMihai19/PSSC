using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Entities
{
    internal class ShoppingCart
    {
        public Guid Id { get; private set; }
        public List<ShoppingCartItem> Items { get; private set; }
        public bool IsValidated { get; private set; }
        public Address ShippingAddress { get; private set; }

        private ShoppingCart()
        {
            Id = Guid.NewGuid();
            Items = new List<ShoppingCartItem>();
        }

        public static ShoppingCart Create()
        {
            return new ShoppingCart();
        }

        public void AddItem(Product product, int quantity)
        {
            if (!IsValidated)
            {
                var existingItem = Items.Find(item => item.Product.ProductCode == product.ProductCode);

                if (existingItem != null)
                    existingItem.Quantity += quantity;
                else
                    Items.Add(new ShoppingCartItem { Product = product, Quantity = quantity });
            }
        }

        public void Validate()
        {
            IsValidated = true;
        }

        public void SetShippingAddress(Address address)
        {
            if (IsValidated)
                ShippingAddress = address;
        }
    }
}
