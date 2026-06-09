# Topic 6 — Delegates, `Func`/`Action` & Events

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is a delegate? How is an `event` different from a plain delegate field?)_

## Your Task
Add a price-change notification to the `Product` aggregate.
- Declare an `event` that fires when the price changes (a `PriceChangedEvent` payload)
- Raise it from a `ChangePrice` method, but only when the value actually changes
- Subscribe a handler that logs the old → new price
- Show the same idea expressed with a `Func<>`/`Action<>` instead of a named delegate

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- Why does `event` exist when a public delegate field would compile? What does it protect?
- What's the difference between `Func<>`, `Action<>`, and `Predicate<>`?
- How can event subscriptions cause memory leaks, and how do you unsubscribe safely?

## Interview Tip
> This is the bridge to **domain events** and messaging (RabbitMQ). Mention that in-process events and out-of-process messages solve the same decoupling problem at different scales.


A delegate is a type-safe reference to a method. It lets you pass behaviour around like data.


An event is a restricted delegate used for notifications. Other classes can subscribe or unsubscribe, but they cannot directly fire the event or replace all handlers. That protection is why event exists.


public delegate void PriceChangedHandler(PriceChangedEvent e);

Any method matching void MethodName(PriceChangedEvent e) can be stored in this delegate.

```
namespace OopPractice.DelegatesEvents;

public sealed record PriceChangedEvent(
    Guid ProductId,
    string ProductName,
    decimal OldPrice,
    decimal NewPrice,
    DateTime ChangedAtUtc
);

public sealed class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; private set; }

    // Event using built-in generic EventHandler<T>
    public event EventHandler<PriceChangedEvent>? PriceChanged;

    public Product(Guid id, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));

        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        Id = id;
        Name = name;
        Price = price;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Price cannot be negative.");

        // Only raise event when price actually changes
        if (Price == newPrice)
            return;

        decimal oldPrice = Price;
        Price = newPrice;

        var payload = new PriceChangedEvent(
            ProductId: Id,
            ProductName: Name,
            OldPrice: oldPrice,
            NewPrice: newPrice,
            ChangedAtUtc: DateTime.UtcNow
        );

        OnPriceChanged(payload);
    }

    private void OnPriceChanged(PriceChangedEvent payload)
    {
        PriceChanged?.Invoke(this, payload);
    }
}

public static class ProductEventsDemo
{
    public static void Run()
    {
        var product = new Product(
            id: Guid.NewGuid(),
            name: "Keyboard",
            price: 50m
        );

        // Subscribe a handler
        product.PriceChanged += LogPriceChange;

        product.ChangePrice(60m);
        product.ChangePrice(60m); // No event, because price did not change
        product.ChangePrice(75m);

        // Unsubscribe safely when no longer needed
        product.PriceChanged -= LogPriceChange;
    }

    private static void LogPriceChange(object? sender, PriceChangedEvent e)
    {
        Console.WriteLine(
            $"Product '{e.ProductName}' price changed: {e.OldPrice:C} -> {e.NewPrice:C}"
        );
    }
}
```


Same idea using Action<>

You do not always need to declare your own delegate type.

Action<T> means:

A method that takes T and returns nothing.



# Topic 6 — Delegates, Func/Action & Events

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is a delegate? How is an event different from a plain delegate field?)_

## Your Task
Add a price-change notification to the Product aggregate.
- Declare an event that fires when the price changes (a PriceChangedEvent payload)
- Raise it from a ChangePrice method, but only when the value actually changes
- Subscribe a handler that logs the old → new price
- Show the same idea expressed with a Func<>/Action<> instead of a named delegate

Write your code below in a new .cs file in this folder.

## Questions Your Instructor Will Ask
- Why does event exist when a public delegate field would compile? What does it protect?
- What's the difference between Func<>, Action<>, and Predicate<>?
- How can event subscriptions cause memory leaks, and how do you unsubscribe safely?

## Interview Tip
> This is the bridge to **domain events** and messaging (RabbitMQ). Mention that in-process events and out-of-process messages solve the same decoupling problem at different scales.

We’ll treat this like an interview-prep session: first the mental model, then a complete .cs example you can paste, then the questions your instructor is likely to ask.

Thought for a couple of seconds
Interview definition
A delegate is a type-safe reference to a method. It lets you pass behaviour around like data.

An event is a restricted delegate used for notifications. Other classes can subscribe or unsubscribe, but they cannot directly fire the event or replace all handlers. That protection is why event exists.

Example:

public delegate void PriceChangedHandler(PriceChangedEvent e);
Means:

Any method matching void MethodName(PriceChangedEvent e) can be stored in this delegate.

Complete example: ProductEventsDemo.cs
```
namespace OopPractice.DelegatesEvents;

public sealed record PriceChangedEvent(
    Guid ProductId,
    string ProductName,
    decimal OldPrice,
    decimal NewPrice,
    DateTime ChangedAtUtc
);

public sealed class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; private set; }

    // Event using built-in generic EventHandler<T>
    public event EventHandler<PriceChangedEvent>? PriceChanged;

    public Product(Guid id, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));

        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        Id = id;
        Name = name;
        Price = price;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Price cannot be negative.");

        // Only raise event when price actually changes
        if (Price == newPrice)
            return;

        decimal oldPrice = Price;
        Price = newPrice;

        var payload = new PriceChangedEvent(
            ProductId: Id,
            ProductName: Name,
            OldPrice: oldPrice,
            NewPrice: newPrice,
            ChangedAtUtc: DateTime.UtcNow
        );

        OnPriceChanged(payload);
    }

    private void OnPriceChanged(PriceChangedEvent payload)
    {
        PriceChanged?.Invoke(this, payload);
    }
}

public static class ProductEventsDemo
{
    public static void Run()
    {
        var product = new Product(
            id: Guid.NewGuid(),
            name: "Keyboard",
            price: 50m
        );

        // Subscribe a handler
        product.PriceChanged += LogPriceChange;

        product.ChangePrice(60m);
        product.ChangePrice(60m); // No event, because price did not change
        product.ChangePrice(75m);

        // Unsubscribe safely when no longer needed
        product.PriceChanged -= LogPriceChange;
    }

    private static void LogPriceChange(object? sender, PriceChangedEvent e)
    {
        Console.WriteLine(
            $"Product '{e.ProductName}' price changed: {e.OldPrice:C} -> {e.NewPrice:C}"
        );
    }
}

```
Same idea using Action<>
You do not always need to declare your own delegate type.

Action<T> means:

A method that takes T and returns nothing.

Action<PriceChangedEvent> logAction = e =>
{
    Console.WriteLine($"{e.ProductName}: {e.OldPrice:C} -> {e.NewPrice:C}");
};

var priceEvent = new PriceChangedEvent(
    ProductId: Guid.NewGuid(),
    ProductName: "Mouse",
    OldPrice: 20m,
    NewPrice: 25m,
    ChangedAtUtc: DateTime.UtcNow
);

logAction(priceEvent);
So this:

Action<PriceChangedEvent>
means:

void SomeMethod(PriceChangedEvent e)
Same idea using Func<>
Func<TInput, TResult> means:

A method that takes input and returns a value.

Example: decide whether a price change is significant.

Func<PriceChangedEvent, bool> isBigIncrease = e =>
{
    decimal difference = e.NewPrice - e.OldPrice;
    return difference >= 10m;
};

bool result = isBigIncrease(priceEvent);
So this:

Func<PriceChangedEvent, bool>
means:

bool SomeMethod(PriceChangedEvent e)


An event in C# is an in-process notification. It lets one object notify other objects without tightly coupling to them. RabbitMQ messages solve a similar decoupling problem, but across process boundaries. A C# event happens inside one running application. A RabbitMQ message can be sent between different services and can survive process boundaries.

RabbitMQ is usually used as a message broker for queues and routing work to consumers. Kafka is usually used as a distributed event streaming platform where events are appended to topics and can be replayed by consumers. RabbitMQ is better when I want reliable task delivery and flexible routing. Kafka is better when I want high-throughput event streams, history, replay, analytics, or multiple services reading the same event log.