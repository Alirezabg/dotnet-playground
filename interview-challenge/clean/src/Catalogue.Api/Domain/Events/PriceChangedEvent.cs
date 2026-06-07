namespace Catalogue.Api.Domain.Events;

/// <summary>Raised by the Product aggregate when its price changes. Would be published to a message bus.</summary>
public sealed record PriceChangedEvent(
    Guid ProductId,
    Money OldPrice,
    Money NewPrice,
    DateTimeOffset OccurredAt);
