public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductValidator _validator;
    private readonly IProductFormatter _formatter;
    private readonly ILogger _logger;

    public ProductService(
        IProductRepository repository,
        IProductValidator validator,
        IProductFormatter formatter,
        ILogger logger)
    {
        _repository = repository;
        _validator = validator;
        _formatter = formatter;
        _logger = logger;
    }

    public IEnumerable<string> GetProductsForDisplay()
    {
        _logger.Log("Fetching products");

        var products = _repository.GetAll();

        return products
            .Where(product => _validator.IsValid(product))
            .Select(product => _formatter.Format(product));
    }
}


public interface IProductRepository
{
    IEnumerable<Product> GetAll();
}
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
}
public interface IProductFormatter
{
    string Format(Product product);
}

public interface ILogger
{
    void Log(string message);
}

public class Product
{
    public string Name { get; }
    public decimal Price { get; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

public class ProductValidator : IProductValidator
{
    public bool IsValid(Product product)
    {
        return !string.IsNullOrWhiteSpace(product.Name)
               && product.Price >= 0;
    }
}

public class ProductFormatter : IProductFormatter
{
    public string Format(Product product)
    {
        return $"{product.Name} - £{product.Price}";
    }
}

