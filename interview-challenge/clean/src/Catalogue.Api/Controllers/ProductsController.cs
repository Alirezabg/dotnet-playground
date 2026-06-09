using Catalogue.Api.Application;
using Catalogue.Api.Contracts;
using Catalogue.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Catalogue.Api.Controllers;

[ApiController]
[Route("products")]
[Tags("Products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository) => _repository = repository;

    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        return Ok(products.Select(p => p.ToResponse()));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<ProductResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(id, cancellationToken);
        return product is null ? NotFound() : Ok(product.ToResponse());
    }

    [HttpPost]
    [ProducesResponseType<ProductResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductResponse>> Create(
        CreateProductRequest request, CancellationToken cancellationToken)
    {
        // Shape/format validation at the boundary. Business invariants still live in the aggregate.
        var errors = Validate(request);
        if (errors.Count > 0)
            return ValidationProblem(new ValidationProblemDetails(errors));

        var product = Product.Create(
            new ProductName(request.Name),
            new Money(request.Price, request.Currency));

        await _repository.AddAsync(product, cancellationToken);

        var response = product.ToResponse();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
    }

    private static Dictionary<string, string[]> Validate(CreateProductRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Name))
            errors[nameof(request.Name)] = new[] { "Name is required." };

        if (request.Price < 0)
            errors[nameof(request.Price)] = new[] { "Price cannot be negative." };

        if (string.IsNullOrWhiteSpace(request.Currency) || request.Currency.Trim().Length != 3)
            errors[nameof(request.Currency)] = new[] { "Currency must be a 3-letter ISO code." };

        return errors;
    }
}
