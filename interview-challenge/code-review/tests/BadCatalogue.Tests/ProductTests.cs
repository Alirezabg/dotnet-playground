// ⚠️  These tests are ALSO part of the challenge. What's wrong with them?
// (Hint: shared state, ordering dependency, weak assertions, no error-path coverage.)

using Xunit;

namespace BadCatalogue.Tests;

public class ProductTests
{
    // Shared, mutable, static state — every test sees the same store.
    private static readonly ProductStore Store = new();

    [Fact]
    public void AddingProduct_IncreasesCount()
    {
        Store.Products.Add(new Product { Name = "A", Price = 10 });

        Assert.True(Store.Products.Count > 0); // weak: passes no matter what
    }

    [Fact]
    public void AddingAnotherProduct()
    {
        Store.Products.Add(new Product { Name = "B", Price = -5 }); // negative price never asserted

        Assert.Equal(2, Store.Products.Count); // depends on the other test running first
    }
}
