using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.DataTypes
{
    public class Invoice
    {
        public record ValidatedProduct(ClientID ClientID, Quantity Quantity, Price Price);
        public Invoice(Client client, List<ValidatedProduct> validatedProducts)
        {
            InvoiceID = Guid.NewGuid();
            Client = client;
            ValidatedProducts = validatedProducts;
            InvoiceDate = DateTime.Now;
            TotalPrice = CalculateTotalPrice(validatedProducts);
        }

        public Guid InvoiceID { get; }
        public Client Client { get; }
        public List<ValidatedProduct> ValidatedProducts { get; }
        public DateTime InvoiceDate { get; }
        public Price TotalPrice { get; }

        private Price CalculateTotalPrice(List<ValidatedProduct> products)
        {
            Price totalPrice = Price.Zero;

            foreach (var product in products)
            {
                totalPrice = totalPrice.Add(product.Price);
            }

            return totalPrice;
        }
    }
}
