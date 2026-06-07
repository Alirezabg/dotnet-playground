namespace Catalogue.Api.Domain;

/// <summary>Base type for all domain rule violations. The middleware maps these to HTTP 400.</summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

public sealed class InvalidProductNameException : DomainException
{
    public InvalidProductNameException(string message) : base(message) { }
}

public sealed class InvalidMoneyException : DomainException
{
    public InvalidMoneyException(string message) : base(message) { }
}
