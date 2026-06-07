# D — Dependency Inversion Principle

**In your own words:**
> _Your answer here..._

## Task
Refactor a `ProductService` that directly instantiates `SqlProductRepository`
to depend on `IProductRepository` instead — and wire it via the DI container in `Program.cs`.

## Interview Tip
> DIP ≠ dependency injection (DI). DIP is the principle; DI is a technique to implement it.
> Be ready to explain both independently.


High-level classes should not directly depend on low-level classes.
Both should depend on abstractions, usually interfaces.

ProductService should not know or care whether products come from SQL, a file, an API, or memory.
It should only depend on an IProductRepository.


Wire it in Program.cs

In ASP.NET Core, the DI container is configured in Program.cs.

```
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

app.Run();
```
This line is important:
```
builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
```
It means:

Whenever something asks for IProductRepository, give it a SqlProductRepository.

So when ProductService is created, the DI container sees this constructor:

public ProductService(IProductRepository repository)

and automatically injects a SqlProductRepository.

Full simple example
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
}
public class SqlProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAll()
    {
        Console.WriteLine("Getting products from SQL database");

        return new List<Product>
        {
            new Product { Id = 1, Name = "Laptop" },
            new Product { Id = 2, Name = "Phone" }
        };
    }

    public Product? GetById(int id)
    {
        return GetAll().FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        Console.WriteLine($"Adding {product.Name} to SQL database");
    }
}
public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _repository.GetAll();
    }

    public void AddProduct(Product product)
    {
        _repository.Add(product);
    }
}

Program.cs:

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

app.MapGet("/products", (ProductService service) =>
{
    return service.GetAllProducts();
});

app.Run();
DIP vs DI

This is very important for interviews.

DIP: Dependency Inversion Principle

DIP is a design principle.

It says:

High-level modules should not depend on low-level modules.
Both should depend on abstractions.

Example:

ProductService -> IProductRepository

instead of:

ProductService -> SqlProductRepository

DIP is about how you design your dependencies.

DI: Dependency Injection

DI is a technique.

It means:

Instead of a class creating its own dependency, the dependency is given to it from the outside.

Bad:

public ProductService()
{
    _repository = new SqlProductRepository();
}

Good:

public ProductService(IProductRepository repository)
{
    _repository = repository;
}

That is constructor injection.

Easy interview answer

You can say:

Dependency Inversion means high-level classes should depend on abstractions, not concrete implementations. For example, ProductService should depend on IProductRepository, not directly on SqlProductRepository. Dependency Injection is one way to achieve this by passing the dependency into the constructor, often using the built-in DI container in ASP.NET Core.