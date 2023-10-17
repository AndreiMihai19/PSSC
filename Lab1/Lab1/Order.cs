public class Order
{
    public Contact ContactInfo { get; }
    public List<Product> Products { get; }

    public Order(Contact contact, List<Product> products)
    {
        ContactInfo = contact;
        Products = products;
    }
}
