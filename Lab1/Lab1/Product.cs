public enum UnitType
{
    Unit,
    Kilogram
}

public class Product
{
    public string ProductCode { get; }
    public int Quantity { get; }
    public UnitType UnitType { get; }

    public Product(string productCode, int quantity, UnitType unitType)
    {
        ProductCode = productCode;
        Quantity = quantity;
        UnitType = unitType;
    }
}
