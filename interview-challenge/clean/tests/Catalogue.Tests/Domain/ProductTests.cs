using Catalogue.Api.Domain;
using Catalogue.Api.Domain.Events;
using Xunit;

namespace Catalogue.Tests.Domain;

// UNIT tests (V Model): pure domain logic, no infrastructure, no web host.
public class ProductTests
{
    [Fact]
    public void Create_WithValidData_StartsAsDraft()
    {
        var product = Product.Create(new ProductName("Linac Cooling Unit"), new Money(1200m, "EUR"));

        Assert.Equal(ProductStatus.Draft, product.Status);
        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Empty(product.DomainEvents);
    }

    [Theory]
    [InlineData(1200, 1500)]
    [InlineData(99.99, 79.99)]
    public void ChangePrice_RaisesEvent_WhenPriceDiffers(decimal initial, decimal updated)
    {
        var product = Product.Create(new ProductName("Treatment Couch"), new Money(initial, "EUR"));

        product.ChangePrice(new Money(updated, "EUR"));

        var domainEvent = Assert.Single(product.DomainEvents);
        var priceChanged = Assert.IsType<PriceChangedEvent>(domainEvent);
        Assert.Equal(updated, priceChanged.NewPrice.Amount);
        Assert.Equal(initial, priceChanged.OldPrice.Amount);
    }

    [Fact]
    public void ChangePrice_DoesNothing_WhenPriceIsUnchanged()
    {
        var product = Product.Create(new ProductName("Imaging Panel"), new Money(500m, "EUR"));

        product.ChangePrice(new Money(500m, "EUR"));

        Assert.Empty(product.DomainEvents);
    }

    [Fact]
    public void TwoProducts_WithSameId_AreEqual()
    {
        var id = Guid.NewGuid();
        var a = new Product(id, new ProductName("A"), new Money(1m, "EUR"));
        var b = new Product(id, new ProductName("B"), new Money(2m, "EUR"));

        Assert.Equal(a, b); // identity equality — same Id, even though name/price differ
    }
}
