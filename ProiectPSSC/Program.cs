using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proiectpssc.Commands;
using proiectpssc.DataTypes;
using proiectpssc.Db_Data.Db_DataRep;
using proiectpssc.Db_Data;
using proiectpssc.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static proiectpssc.Events.PayCartEvent;
using Microsoft.Data.SqlClient;

namespace proiectpssc
{
    class Program
    {
        private static string ConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";
        static async Task Main(string[] args)
        {

           
            using ILoggerFactory loggerFactory = ConfigureLoggerFactory();
            ILogger<AddProductsToCartWorkflow> logger = loggerFactory.CreateLogger<AddProductsToCartWorkflow>();
            ILogger<InvoiceWorkflow> logger1 = loggerFactory.CreateLogger<InvoiceWorkflow>();

            var listOfGrades = ReadListOfProducts().ToArray();
            PaymentCartCommand command = new PaymentCartCommand(listOfGrades);

            var dbContextBuilder = new DbContextOptionsBuilder<ProductsContext>()
                                                .UseSqlServer(ConnectionString)
                                                .UseLoggerFactory(loggerFactory);

            var dbContextBuilder1 = new DbContextOptionsBuilder<InvoiceContext>()
                                                .UseSqlServer(ConnectionString)
                                                .UseLoggerFactory(loggerFactory);

            ProductsContext productsContext = new ProductsContext(dbContextBuilder.Options);
            ClientsRepository clientsRepository = new ClientsRepository(productsContext);
            ProductsRepository productsRepository = new ProductsRepository(productsContext);
            InvoiceContext invoiceContext = new InvoiceContext(dbContextBuilder1.Options);
            InvoiceRepository invoiceRepository = new InvoiceRepository(invoiceContext);


            AddProductsToCartWorkflow workflow = new AddProductsToCartWorkflow(clientsRepository, productsRepository, logger);
            var result = await workflow.ExecuteAsync(command);

            InvoiceWorkflow workflow1 = new InvoiceWorkflow(invoiceRepository, logger1);
            DeliveryWorkflow workflow2 = new DeliveryWorkflow(productsContext, logger);

            result.Match(
                    whenPayCartFailedEvent: @event =>
                    {
                        Console.WriteLine($"Payment failed: {@event.Reason}");
                        return @event;
                    },
                    whenPayCartSucceededEvent: @event =>
                    {
                        Console.WriteLine($"Payment succeeded.");
                        //workflow1.GenerateInvoice(new CalculatedCart(@event.PaidCart.ProductList));
                        workflow2.Validate(1, @event.PaidCart);
                        Console.WriteLine(workflow2.Validate(1, @event.PaidCart));
                        Console.WriteLine(@event.Csv);
                        return @event;
                    }
                );
        }

        private static ILoggerFactory ConfigureLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
                                builder.AddSimpleConsole(options =>
                                {
                                    options.IncludeScopes = true;
                                    options.SingleLine = true;
                                    options.TimestampFormat = "hh:mm:ss ";
                                })
                                .AddProvider(new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()));
        }

        private static List<UnvalidatedProduct> ReadListOfProducts()
        {
            List<UnvalidatedProduct> listOfProducts = new();
            do
            {
                var codeNumber = ReadValue("Client Code Number: ");
                if (string.IsNullOrEmpty(codeNumber))
                {
                    break;
                }

                var quantity = ReadValue("Quantity: ");
                if (string.IsNullOrEmpty(quantity))
                {
                    break;
                }

                var price = ReadValue("Price: ");
                if (string.IsNullOrEmpty(price))
                {
                    break;
                }

                listOfProducts.Add(new(codeNumber, quantity, price));
            } while (true);
            return listOfProducts;
        }



        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }


    }
}
