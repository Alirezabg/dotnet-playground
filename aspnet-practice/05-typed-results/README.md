# Topic 5 — `ActionResult<T>` Typed Responses

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (How does `ActionResult<T>` improve over returning a raw object or `IActionResult`? What does it give OpenAPI?)_

## Your Task
Return precise status codes from a `ProductsController`.
- Make `GetById` return `ActionResult<ProductResponse>` — `Ok(...)` when found, `NotFound()` when not
- Make `Create` return `CreatedAtAction(nameof(GetById), new { id }, response)` for `201 Created`
- Annotate the actions with `[ProducesResponseType(...)]` for each status code
- Explain how these attributes feed the OpenAPI document

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the benefit of `ActionResult<T>` over returning a plain object or `IActionResult`?
- How do `[ProducesResponseType]` attributes help generate accurate Swagger/OpenAPI metadata?
- Which status code belongs to which outcome — and why does `CreatedAtAction` / `201 Created` need a `Location` header?

## Interview Tip
> `ActionResult<T>` makes the *contract* explicit: the action can return the typed body **or** a status result, and `[ProducesResponseType]` tells OpenAPI exactly which statuses are possible.


ActionResult<T> means: “this endpoint normally returns a typed response body, but it can also return HTTP status results like 404 NotFound or 201 Created.”

It is better than returning a raw object because you can return proper status codes. It is better than plain IActionResult because the success body type is still visible to ASP.NET and OpenAPI/Swagger.

```
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogue.Api.Controllers;

[ApiController]
[Route("products")]
[Tags("Products")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductRepository _products;

    public ProductsController(IProductRepository products)
    {
        _products = products;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductResponse> GetById(Guid id)
    {
        Product? product = _products.GetById(id);

        if (product is null)
        {
            return NotFound();
        }

        ProductResponse response = ProductResponse.FromDomain(product);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductResponse> Create(CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Product name is required.");
        }

        var product = new Product(
            Guid.NewGuid(),
            request.Name,
            request.Price,
            request.Currency,
            request.CategoryName
        );

        _products.Add(product);

        ProductResponse response = ProductResponse.FromDomain(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            response
        );
    }
}
````