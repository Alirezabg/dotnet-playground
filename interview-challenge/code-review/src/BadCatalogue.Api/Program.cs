// ⚠️  CODE REVIEW CHALLENGE
// This file COMPILES and RUNS, but it is full of problems a reviewer should catch.
// Read it like a pull request. Note every issue you can find, THEN open REVIEW.md.
//
// Domain: the same Product Catalogue. Pretend a teammate submitted this for review.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductStore>();

var app = builder.Build();

// Swallows every error and reports success.
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch
    {
        context.Response.StatusCode = 200;
    }
});

// Returns the domain objects straight to the client.
app.MapGet("/products", (ProductStore store) => store.Products);

// 'id' is a loose string; a missing product returns null (HTTP 200 with empty body).
app.MapGet("/products/{id}", (string id, ProductStore store) =>
{
    var product = store.Products.FirstOrDefault(p => p.Id.ToString() == id);
    return product;
});

// No validation: negative prices, empty names, and null currency all pass.
app.MapPost("/products", (Product product, ProductStore store) =>
{
    product.Id = Guid.NewGuid();
    product.CreatedAt = DateTime.Now;
    store.Products.Add(product);
    return "ok";
});

// Blocks on async work (.Wait()) and throws if the product is missing.
app.MapPost("/products/{id}/reprice", (string id, decimal price, ProductStore store) =>
{
    var product = store.Products.First(p => p.Id.ToString() == id);
    product.Price = price;
    SaveToDiskAsync(product).Wait();
    return product;
});

app.Run();

static async Task SaveToDiskAsync(Product product)
{
    await Task.Delay(10);
    // pretend to persist
}

// Anaemic model with public setters — anyone can put it into an invalid state.
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}

// Singleton holding a plain List<T> — not safe for concurrent requests.
public class ProductStore
{
    public List<Product> Products { get; } = new();
}

public partial class Program { }
