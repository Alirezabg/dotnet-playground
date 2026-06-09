// ⚠️  CODE REVIEW CHALLENGE — controller half of the bad submission.
// Every flaw from the original Minimal API version is preserved here, in MVC form.

using Microsoft.AspNetCore.Mvc;

namespace BadCatalogue.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly ProductStore _store;

    public ProductsController(ProductStore store) => _store = store;

    // Returns the domain objects straight to the client.
    [HttpGet]
    public List<Product> GetAll() => _store.Products;

    // 'id' is a loose string; a missing product returns null (HTTP 200 with empty body).
    [HttpGet("{id}")]
    public Product GetById(string id)
    {
        return _store.Products.FirstOrDefault(p => p.Id.ToString() == id);
    }

    // No validation: negative prices, empty names, and null currency all pass.
    [HttpPost]
    public string Create(Product product)
    {
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.Now;
        _store.Products.Add(product);
        return "ok";
    }

    // Blocks on async work (.Wait()) and throws if the product is missing.
    [HttpPost("{id}/reprice")]
    public Product Reprice(string id, decimal price)
    {
        var product = _store.Products.First(p => p.Id.ToString() == id);
        product.Price = price;
        SaveToDiskAsync(product).Wait();
        return product;
    }

    private static async Task SaveToDiskAsync(Product product)
    {
        await Task.Delay(10);
        // pretend to persist
    }
}
