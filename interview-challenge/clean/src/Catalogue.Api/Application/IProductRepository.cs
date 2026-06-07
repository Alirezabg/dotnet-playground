using Catalogue.Api.Domain;

namespace Catalogue.Api.Application;

/// <summary>Repositories return domain aggregates, not raw data rows.</summary>
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
}
