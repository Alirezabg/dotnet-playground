using System.Collections.Concurrent;
using Catalogue.Api.Application;
using Catalogue.Api.Domain;

namespace Catalogue.Api.Infrastructure;

/// <summary>
/// Thread-safe in-memory store. Registered as a singleton, so the backing collection
/// must be safe for concurrent access — hence ConcurrentDictionary, not List.
/// </summary>
public sealed class InMemoryProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_products.TryGetValue(id, out var product) ? product : null);

    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Product>>(_products.Values.ToList());

    public Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        _products[product.Id] = product;
        return Task.CompletedTask;
    }
}
