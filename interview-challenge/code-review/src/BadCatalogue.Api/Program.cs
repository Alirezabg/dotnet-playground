// ⚠️  CODE REVIEW CHALLENGE
// This file COMPILES and RUNS, but it is full of problems a reviewer should catch.
// Read it (with Controllers/ProductsController.cs) like a pull request.
// Note every issue you can find, THEN open REVIEW.md.
//
// Domain: the same Product Catalogue. Pretend a teammate submitted this for review.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductStore>();
builder.Services.AddControllers();

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

app.MapControllers();

app.Run();

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
