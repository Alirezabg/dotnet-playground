using Catalogue.Api.Domain.Events;

namespace Catalogue.Api.Domain;

/// <summary>
/// Aggregate root. State changes go through methods (never public setters) so invariants always hold.
/// Identity-based equality: two products are equal iff their Id matches.
/// </summary>
public sealed class Product
{
    private readonly List<object> _domainEvents = new();

    public Guid Id { get; }
    public ProductName Name { get; private set; }
    public Money Price { get; private set; }
    public ProductStatus Status { get; private set; }

    public IReadOnlyList<object> DomainEvents => _domainEvents.AsReadOnly();

    public Product(Guid id, ProductName name, Money price)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id is required.", nameof(id));

        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Status = ProductStatus.Draft;
    }

    public static Product Create(ProductName name, Money price) => new(Guid.NewGuid(), name, price);

    public void ChangePrice(Money newPrice)
    {
        ArgumentNullException.ThrowIfNull(newPrice);
        if (newPrice == Price)
            return;

        var oldPrice = Price;
        Price = newPrice;
        _domainEvents.Add(new PriceChangedEvent(Id, oldPrice, newPrice, DateTimeOffset.UtcNow));
    }

    public void Activate() => Status = ProductStatus.Active;

    public void ClearDomainEvents() => _domainEvents.Clear();

    public override bool Equals(object? obj) => obj is Product other && other.Id == Id;

    public override int GetHashCode() => Id.GetHashCode();
}
