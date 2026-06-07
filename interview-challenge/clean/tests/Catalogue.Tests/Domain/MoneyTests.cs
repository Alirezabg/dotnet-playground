using Catalogue.Api.Domain;
using Xunit;

namespace Catalogue.Tests.Domain;

public class MoneyTests
{
    [Fact]
    public void TwoAmounts_WithSameValues_AreEqual()
    {
        // Value equality — and currency is normalised to upper-case.
        Assert.Equal(new Money(10m, "EUR"), new Money(10m, "eur"));
    }

    [Fact]
    public void NegativeAmount_Throws()
    {
        Assert.Throws<InvalidMoneyException>(() => new Money(-1m, "EUR"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("EURO")]
    [InlineData("E")]
    public void InvalidCurrency_Throws(string currency)
    {
        Assert.Throws<InvalidMoneyException>(() => new Money(10m, currency));
    }
}
