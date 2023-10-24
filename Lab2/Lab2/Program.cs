using Lab2.CartRepositories;
using Lab2.Entities;
using Lab2.Repositories;

class Program
{
    static void Main()
    {
        ProductRepository productRepository = new ProductRepository();
        productRepository.AddProduct(new Product { ProductCode = "1234", Name = "Popice", Price = 50, StockQuantity = 100 });
        productRepository.AddProduct(new Product { ProductCode = "2345", Name = "TV Phillips", Price = 1500, StockQuantity = 30 });

        ShoppingCartRepository cartRepository = new ShoppingCartRepository();
        ShoppingCartService cartService = new ShoppingCartService(cartRepository, productRepository);

        Console.WriteLine("Bun venit în aplicatia de cos de cumparaturi!");

        while (true)
        {
            Console.WriteLine("\nMeniu:");
            Console.WriteLine("1. Creaza un cos de cumparaturi");
            Console.WriteLine("2. Adauga produs in cos");
            Console.WriteLine("3. Validează cosul");
            Console.WriteLine("4. Seteaza adresa de livrare");
            Console.WriteLine("5. Iesi");

            Console.Write("Alege o optiune: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var cart = cartService.CreateShoppingCart();
                    Console.WriteLine($"Cosul cu ID {cart.Id} a fost creat.");
                    break;
                case "2":
                    Console.Write("Introdu codul produsului: ");
                    string productCode = Console.ReadLine();
                    Console.Write("Introdu cantitatea: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Introdu ID-ul cosului: ");
                    Guid cartId = Guid.Parse(Console.ReadLine());

                    var cartToAddProduct = cartRepository.GetShoppingCart(cartId);
                    cartService.AddProductToCart(cartToAddProduct, productCode, quantity);

                    Console.WriteLine("Produsul a fost adaugat în cos.");
                    break;
                case "3":
                    Console.Write("Introdu ID-ul cosului de validat: ");
                    Guid cartIdToValidate = Guid.Parse(Console.ReadLine());

                    var cartToValidate = cartRepository.GetShoppingCart(cartIdToValidate);
                    cartService.ValidateCart(cartToValidate);

                    Console.WriteLine("Cosul a fost validat.");
                    break;
                case "4":
                    Console.Write("Introdu ID-ul cosului pentru setarea adresei de livrare: ");
                    Guid cartIdToSetAddress = Guid.Parse(Console.ReadLine());

                    var cartToSetAddress = cartRepository.GetShoppingCart(cartIdToSetAddress);
                    Console.Write("Introdu strada: ");
                    string street = Console.ReadLine();
                    Console.Write("Introdu orasul: ");
                    string city = Console.ReadLine();
                    Address address = new Address(street, city);

                    cartService.SetShippingAddress(cartToSetAddress, address);

                    Console.WriteLine("Adresa de livrare a fost setata.");
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }
        }
    }
}
