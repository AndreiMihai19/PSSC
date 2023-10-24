using Lab3.Servicies;
using Lab3.Entities;

class Program
{
    static void Main()
    {
        ProductRepository productRepository = new ProductRepository();
        productRepository.AddProduct(new Product { ProductCode = "111111", Name = "Carte", Price = 10, StockQuantity = 100 });
        productRepository.AddProduct(new Product { ProductCode = "222222", Name = "Bicicleta", Price = 900, StockQuantity = 50 });

        OrderService orderService = new OrderService(productRepository);
        orderService.OrderStatusChanged += (sender, status) =>
        {
            Console.WriteLine(status);
        };

        while (true) 
        {

            Console.Write("Doriti sa continuati? (Da/Nu) : ");
            string question = Console.ReadLine();
            if (question.ToLower() == "nu")
                break;

            Console.Write("Cod produs: ");
            string productCode = Console.ReadLine();

            Console.Write("Cantitate: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Adresa de livrare - Strada: ");
            string street = Console.ReadLine();
            Console.Write("Oras: ");
            string city = Console.ReadLine();

            var order = new Order
            {
                ProductCode = productCode,
                Quantity = quantity,
                DeliveryAddress = new Address { Street = street, City = city }
            };

            orderService.PlaceOrder(order);
        }
    }
}

