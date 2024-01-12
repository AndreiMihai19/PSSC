using Microsoft.Extensions.Logging;
using proiectpssc.DataTypes;
using proiectpssc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static proiectpssc.DataTypes.Cart;

namespace proiectpssc.Workflows
{
    public class InvoiceWorkflow
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly ILogger<InvoiceWorkflow> logger;

        public InvoiceWorkflow(IInvoiceRepository invoiceRepository, ILogger<InvoiceWorkflow> logger)
        {
            this.invoiceRepository = invoiceRepository;
            this.logger = logger;
        }

        public Invoice GenerateInvoice(CalculatedCart calculatedCart)
        {
            var validatedProducts = calculatedCart.ProductList.Select(calculatedProduct =>
                new Invoice.ValidatedProduct(
                    calculatedProduct.ClientId,
                    calculatedProduct.Quantity,
                    calculatedProduct.Price
                )).ToList();

            var invoice = new Invoice(calculatedCart.Client, validatedProducts);
            DisplayInvoiceDetails(invoice);
            return invoice;
        }


        public void DisplayInvoiceDetails(Invoice invoice)
        {
            Console.WriteLine($"Invoice ID: {invoice.InvoiceID}");
            Console.WriteLine($"Client: {invoice.Client}");
            Console.WriteLine($"Products: {string.Join(", ", invoice.ValidatedProducts.Select(p => p.ToString()))}");
            Console.WriteLine($"Invoice Date: {invoice.InvoiceDate}");
            Console.WriteLine($"Total Price: {invoice.TotalPrice}");
        }
    }
}
