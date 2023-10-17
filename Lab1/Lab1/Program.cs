using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Introduceti datele pentru comanda:");

            Console.Write("Nume: ");
            string firstName = Console.ReadLine();

            Console.Write("Prenume: ");
            string lastName = Console.ReadLine();

            Console.Write("Numar de telefon: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Adresa: ");
            string address = Console.ReadLine();

            var contact = new Contact(firstName, lastName, phoneNumber, address);

            var products = new List<Product>();


            do
            {
                Console.Write("Introduceti codul produsului (sau 'exit' pentru a termina): ");
                string productCode = Console.ReadLine();

                if (productCode.ToLower() == "exit")
                    break;

                Console.Write("Cantitate: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.Write("Unitate (unit/kg): ");
                string unitTypeInput = Console.ReadLine();
                UnitType unitType = (unitTypeInput.ToLower() == "kg") ? UnitType.Kilogram : UnitType.Unit;

                var product = new Product(productCode, quantity, unitType);
                products.Add(product);
            }while(true);

            var order = new Order(contact, products);

            Console.WriteLine("\nDetalii comanda:");
            Console.WriteLine($"Nume: {order.ContactInfo.FirstName} {order.ContactInfo.LastName}");
            Console.WriteLine($"Telefon: {order.ContactInfo.PhoneNumber}");
            Console.WriteLine($"Adresă: {order.ContactInfo.Address}");
            Console.WriteLine("\nProduse:");

            foreach (var product in order.Products)
            {
                Console.WriteLine($"\nCod produs: {product.ProductCode}");
                Console.WriteLine($"Cantitate: {product.Quantity} {(product.UnitType == UnitType.Unit ? "unitati" : "kg")}");
            }
        }

    }

}

