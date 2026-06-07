namespace Catalogue.Api.Domain;

/// <summary>
/// Value object for a product name. Enforces its own validity rules.
/// </summary>
public sealed record ProductName
{
    public string Value { get; }

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidProductNameException("Product name is required.");
        if (value.Trim().Length > 200)
            throw new InvalidProductNameException("Product name cannot exceed 200 characters.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
