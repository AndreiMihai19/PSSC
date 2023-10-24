using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3.Entities;

namespace Lab3.Servicies
{
    internal class OrderService
    {
        private ProductRepository productRepository;

        public OrderService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public event EventHandler<string> OrderStatusChanged;

        public void PlaceOrder(Order order)
        {
            try
            {
                if (order == null || string.IsNullOrEmpty(order.ProductCode) || order.Quantity <= 0 || order.DeliveryAddress == null)
                {
                    OrderStatusChanged?.Invoke(null, "Comanda invalida. Va rugam sa completati corect formularul de comanda.");
                    return;
                }

                var product = productRepository.GetProductByCode(order.ProductCode);
                if (product == null)
                {
                    OrderStatusChanged?.Invoke(null, "Produsul nu exista in baza de date.");
                    return;
                }

                if (order.Quantity > product.StockQuantity)
                {
                    OrderStatusChanged?.Invoke(null, "Stoc insuficient.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(order.DeliveryAddress.Street) || string.IsNullOrWhiteSpace(order.DeliveryAddress.City))
                {
                    OrderStatusChanged?.Invoke(null, "Adresa de livrare este invalida.");
                    return;
                }

                decimal totalPrice = product.Price * order.Quantity;

                OrderStatusChanged?.Invoke(null, $"Comanda a fost plasata cu succes. Pret total: {totalPrice:C}");
            }
            catch (Exception ex)
            {
                OrderStatusChanged?.Invoke(null, "A aparut o eroare la plasarea comenzii: " + ex.Message);
            }
        }
    }
}
