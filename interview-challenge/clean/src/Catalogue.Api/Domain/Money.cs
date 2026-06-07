namespace Catalogue.Api.Domain;

/// <summary>
/// Value object representing a monetary amount. Immutable and compared by value.
/// </summary>
public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new InvalidMoneyException("Amount cannot be negative.");
        if (string.IsNullOrWhiteSpace(currency) || currency.Trim().Length != 3)
            throw new InvalidMoneyException("Currency must be a 3-letter ISO code.");

        Amount = amount;
        Currency = currency.Trim().ToUpperInvariant();
    }

    public override string ToString() => $"{Amount:0.00} {Currency}";
}
