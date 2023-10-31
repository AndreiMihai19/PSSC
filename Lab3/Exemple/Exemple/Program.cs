using Exemple.Domain.Models;
using System;
using System.Collections.Generic;
using static Exemple.Domain.Models.ProductValues;
using static Exemple.Domain.ProductValuesOperation;
using Exemple.Domain;

namespace Exemple
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            var listOfProducts = ReadListOfProducts().ToArray();
            PublishValuesCommand command = new(listOfProducts);
            PublishProductWorkflow workflow = new PublishProductWorkflow();
            var result = workflow.Execute(command, (registrationNumber) => true);

            result.Match(
                    whenProductValuesPublishFaildEvent: @event =>
                    {
                        Console.WriteLine($"Publish failed: {@event.Reason}");
                        return @event;
                    },
                    whenProductValuesPublishSucceededEvent: @event =>
                    {
                        Console.WriteLine($"Publish succeeded.");
                        Console.WriteLine(@event.Csv);
                        return @event;
                    }
                );

        }

        private static List<UnvalidatedShoppingCart> ReadListOfProducts()
        {
            List <UnvalidatedShoppingCart> listOfProducts = new();
            do
            {
                //read registration number and grade and create a list of greads
                var productCode = ReadValue("Product Code: ");
                if (string.IsNullOrEmpty(productCode))
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

                var address = ReadValue("Address: ");
                if (string.IsNullOrEmpty(address))
                {
                    break;
                }

                listOfProducts.Add(new (productCode, quantity, address,price));
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
