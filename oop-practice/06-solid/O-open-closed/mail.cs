public class PriceCalculator
{
    public decimal Calculate(Product product)
    {
        switch (product.Type)
        {
            case ProductType.Physical:
                return product.BasePrice + 5;

            case ProductType.Digital:
                return product.BasePrice;

            default:
                throw new Exception("Unknown product type");
        }
    }
}


public abstract class Product
{
    public string Name { get; }
    public decimal BasePrice { get; }

    protected Product(string name, decimal basePrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        if (basePrice < 0)
            throw new ArgumentException("Base price cannot be negative.", nameof(basePrice));

        Name = name;
        BasePrice = basePrice;
    }

    public abstract decimal CalculatePrice();
}

public class PhysicalProduct : Product
{
    public decimal ShippingCost { get; }

    public PhysicalProduct(string name, decimal basePrice, decimal shippingCost)
        : base(name, basePrice)
    {
        ShippingCost = shippingCost;
    }

    public override decimal CalculatePrice()
    {
        return BasePrice + ShippingCost;
    }
}

public class DigitalProduct : Product
{
    public DigitalProduct(string name, decimal basePrice)
        : base(name, basePrice)
    {
    }

    public override decimal CalculatePrice()
    {
        return BasePrice;
    }
}

public class SubscriptionProduct : Product
{
    public int Months { get; }

    public SubscriptionProduct(string name, decimal monthlyPrice, int months)
        : base(name, monthlyPrice)
    {
        Months = months;
    }

    public override decimal CalculatePrice()
    {
        return BasePrice * Months;
    }
}


public class PriceCalculator
{
    public decimal Calculate(Product product)
    {
        return product.CalculatePrice();
    }
}



// var products = new List<Product>
// {
//     new PhysicalProduct("Keyboard", 50m, 5m),
//     new DigitalProduct("E-book", 20m),
//     new SubscriptionProduct("Netflix", 10m, 12)
// };

// var calculator = new PriceCalculator();

// foreach (var product in products)
// {
//     Console.WriteLine($"{product.Name}: {calculator.Calculate(product)}");
// }

