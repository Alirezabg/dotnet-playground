using Catalogue.Api.Domain;

namespace Catalogue.Api.Contracts;

/// <summary>API contract — deliberately decoupled from the domain model.</summary>
public sealed record CreateProductRequest(string Name, decimal Price, string Currency);

public sealed record ProductResponse(Guid Id, string Name, string Price, string Status);

public static class ProductMappings
{
    public static ProductResponse ToResponse(this Product product) => new(
        product.Id,
        product.Name.Value,
        product.Price.ToString(),
        product.Status.ToString());
}
