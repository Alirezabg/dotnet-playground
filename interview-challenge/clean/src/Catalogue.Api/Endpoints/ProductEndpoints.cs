using Catalogue.Api.Application;
using Catalogue.Api.Contracts;
using Catalogue.Api.Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalogue.Api.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProducts(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/products").WithTags("Products");

        group.MapGet("/", async (IProductRepository repository, CancellationToken cancellationToken) =>
        {
            var products = await repository.GetAllAsync(cancellationToken);
            return TypedResults.Ok(products.Select(p => p.ToResponse()));
        })
        .WithName("GetProducts")
        .Produces<IEnumerable<ProductResponse>>();

        group.MapGet("/{id:guid}", async Task<Results<Ok<ProductResponse>, NotFound>> (
            Guid id, IProductRepository repository, CancellationToken cancellationToken) =>
        {
            var product = await repository.GetByIdAsync(id, cancellationToken);
            return product is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(product.ToResponse());
        })
        .WithName("GetProductById");

        group.MapPost("/", async Task<Results<Created<ProductResponse>, ValidationProblem>> (
            CreateProductRequest request, IProductRepository repository, CancellationToken cancellationToken) =>
        {
            var errors = Validate(request);
            if (errors.Count > 0)
                return TypedResults.ValidationProblem(errors);

            var product = Product.Create(
                new ProductName(request.Name),
                new Money(request.Price, request.Currency));

            await repository.AddAsync(product, cancellationToken);

            var response = product.ToResponse();
            return TypedResults.Created($"/products/{product.Id}", response);
        })
        .WithName("CreateProduct");

        return routes;
    }

    // Shape/format validation at the boundary. Business invariants still live in the aggregate.
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
