# Topic 10: Domain Driven Design

## Elekta Relevance
Elekta's job description explicitly calls out DDD. This is one of the highest-priority topics.

## Concepts to Master
- **Aggregate / Aggregate Root** — transactional boundary, guards invariants
- **Entity** — identity that persists over time
- **Value Object** — identity by value, immutable, no lifecycle
- **Domain Event** — something that has happened; raised by aggregates
- **Repository** — abstracts persistence of aggregates (interface in Domain layer)
- **Bounded Context** — linguistic and model boundary
- **Anti-Corruption Layer** — translates between two bounded context models

## Your Tasks (implement in this folder)

### Task A — Aggregate Root
Model `Product` as an aggregate root. It should:
- Have a private constructor (creation goes through a factory method)
- Enforce at least one invariant (e.g. price cannot be negative)
- Raise a `ProductCreatedEvent` on creation

Before coding, answer:
- What state does `Product` encapsulate?
- What invariant must always hold?

### Task B — Value Object
Implement `Money` (amount + currency) as a value object:
- Immutable
- Equality by value
- Cannot represent negative money

Before coding, answer:
- Why does `Money` not need an `Id`?
- How does C# `record` help you here?

### Task C — Domain Event
Define `ProductCreatedEvent`:
- Immutable (record or sealed class)
- Contains: product ID, name, price, occurred-on timestamp
- Belongs in the Domain layer — no infrastructure references

Before coding, answer:
- Who creates the event — the aggregate or the application service?
- Where does it get published — and when?

### Task D — Repository Interface
Define `IProductRepository` in the Domain layer:
- Only returns and accepts domain aggregates, not DTOs
- Has: `GetByIdAsync`, `SaveAsync`
- Uses `CancellationToken`

Before coding, answer:
- Why does the interface live in Domain, not Infrastructure?
- What would the xUnit unit test for a service using this look like?

## Interview Questions Elekta May Ask
- "What is the difference between an entity and a value object?"
- "Why should domain logic not depend on the database?"
- "How do you test an aggregate's invariant enforcement?"
- "What happens when two aggregates need to collaborate?"


Domain Driven Design means we design the code around the business rules, not around the database, API, or framework.
The repository interface lives in the Domain layer because the domain defines what persistence operations it needs. The Infrastructure layer implements that interface using a database or another storage mechanism.
Task A — Aggregate Root

Before coding:

What state does Product encapsulate?

Product encapsulates:

Id
Name
Price
DomainEvents

Meaning:

The product owns its identity, name, price, and the events caused by changes to itself.
What invariant must always hold?

At minimum:

A product price cannot be negative.

Also useful:

Product name cannot be empty.
Product ID must not be empty.
Recommended folder structure

Use something like this:

Topic10.DomainDrivenDesign/
│
├── Domain/
│   ├── Products/
│   │   ├── Product.cs
│   │   ├── IProductRepository.cs
│   │
│   ├── ValueObjects/
│   │   └── Money.cs
│   │
│   ├── Events/
│   │   ├── IDomainEvent.cs
│   │   └── ProductCreatedEvent.cs
│
├── Application/
│   └── Products/
│       └── CreateProductService.cs
│
├── Infrastructure/
│   └── Repositories/
│       └── InMemoryProductRepository.cs
Task B — Value Object: Money
Why does Money not need an ID?

Because money is identified by its values.

This:

new Money(10, "GBP")

is equal to another:

new Money(10, "GBP")

There is no business reason to give each Money object a unique ID.

Interview answer:

Money is a value object because we care about its amount and currency, not its identity. Two Money objects with the same amount and currency should be considered equal.

How does C# record help?

A C# record gives you value-based equality automatically.

This means:

var a = new Money(10, "GBP");
var b = new Money(10, "GBP");

Console.WriteLine(a == b); // True

With a normal class, this would usually compare object references unless you override equality yourself.

Money.cs
namespace Topic10.DomainDrivenDesign.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Money cannot be negative.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public override string ToString()
    {
        return $"{Amount:0.00} {Currency}";
    }
}

Because this is a record, equality is by value.

Example:

var price1 = new Money(20, "GBP");
var price2 = new Money(20, "gbp");

Console.WriteLine(price1 == price2); // True

Why? Because the constructor normalises the currency to uppercase.

Task C — Domain Event

Before coding:

Who creates the event — the aggregate or the application service?

The aggregate creates the domain event.

Why?

Because the aggregate knows when a meaningful domain action has happened.

For example:

Product.Create(...)

should raise:

ProductCreatedEvent

The application service should not need to remember to create that event manually.

Interview answer:

The aggregate raises the domain event because the aggregate owns the business rule and knows when the business action has occurred.

Where does it get published — and when?

Usually:

The aggregate raises the event.
The application service saves the aggregate using a repository.
After successful save, events are published.
Event handlers react to the event.

Example reactions:

Send notification
Update search index
Write audit log
Start integration workflow

Important interview point:

The Domain layer raises events, but it should not depend on infrastructure event buses, message queues, or email services.

IDomainEvent.cs
namespace Topic10.DomainDrivenDesign.Domain.Events;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
ProductCreatedEvent.cs
using Topic10.DomainDrivenDesign.Domain.ValueObjects;

namespace Topic10.DomainDrivenDesign.Domain.Events;

public sealed record ProductCreatedEvent(
    Guid ProductId,
    string Name,
    Money Price,
    DateTimeOffset OccurredOn
) : IDomainEvent;

This is immutable because it is a record.

It contains:

Product ID
Name
Price
Occurred-on timestamp

It has no infrastructure references.

No database.

No email sender.

No message queue.

No logging framework.

That is correct.

Task A Implementation — Product Aggregate Root
Product.cs
using Topic10.DomainDrivenDesign.Domain.Events;
using Topic10.DomainDrivenDesign.Domain.ValueObjects;

namespace Topic10.DomainDrivenDesign.Domain.Products;

public sealed class Product
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id { get; }

    public string Name { get; private set; }

    public Money Price { get; private set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Product(Guid id, string name, Money price)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Product ID cannot be empty.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        Id = id;
        Name = name;
        Price = price;
    }

    public static Product Create(string name, Money price)
    {
        var product = new Product(Guid.NewGuid(), name, price);

        product.RaiseDomainEvent(
            new ProductCreatedEvent(
                product.Id,
                product.Name,
                product.Price,
                DateTimeOffset.UtcNow));

        return product;
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Product name is required.", nameof(newName));

        Name = newName;
    }

    public void ChangePrice(Money newPrice)
    {
        Price = newPrice;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    private void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

Notice this part:

private Product(Guid id, string name, Money price)

The constructor is private.

So nobody can do this:

var product = new Product(...);

They must use:

var product = Product.Create("Keyboard", new Money(50, "GBP"));

That means creation always goes through the factory method, and the factory method can raise the domain event.

Task D — Repository Interface

Before coding:

Why does the interface live in Domain, not Infrastructure?

Because the Domain layer defines what it needs.

The domain does not care how products are saved.

It only says:

I need a way to get a Product by ID.
I need a way to save a Product.

The infrastructure decides how:

SQL
Entity Framework
Dapper
In-memory collection
API

Interview answer:

The interface belongs in the Domain layer because it represents a dependency required by the domain/application. Infrastructure provides the implementation. This follows the Dependency Inversion Principle.

IProductRepository.cs
namespace Topic10.DomainDrivenDesign.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task SaveAsync(
        Product product,
        CancellationToken cancellationToken = default);
}

Notice:

Task<Product?>

It returns a domain aggregate, not a DTO.

Good:

Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

Bad:

Task<ProductDto?> GetByIdAsync(Guid id);

The repository is part of the domain model, so it should work with domain objects.

Example Infrastructure Implementation

This does not belong in Domain.

It belongs in Infrastructure.

InMemoryProductRepository.cs
using Topic10.DomainDrivenDesign.Domain.Products;

namespace Topic10.DomainDrivenDesign.Infrastructure.Repositories;

public sealed class InMemoryProductRepository : IProductRepository
{
    private readonly Dictionary<Guid, Product> _products = new();

    public Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        _products.TryGetValue(id, out var product);

        return Task.FromResult(product);
    }

    public Task SaveAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        _products[product.Id] = product;

        return Task.CompletedTask;
    }
}
Example Application Service

The Application layer coordinates the use case.

It does not contain the core business rules.

CreateProductService.cs
using Topic10.DomainDrivenDesign.Domain.Products;
using Topic10.DomainDrivenDesign.Domain.ValueObjects;

namespace Topic10.DomainDrivenDesign.Application.Products;

public sealed class CreateProductService
{
    private readonly IProductRepository _productRepository;

    public CreateProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> CreateAsync(
        string name,
        decimal amount,
        string currency,
        CancellationToken cancellationToken = default)
    {
        var price = new Money(amount, currency);

        var product = Product.Create(name, price);

        await _productRepository.SaveAsync(product, cancellationToken);

        // In a real application, domain events would usually be published after save.
        // Example:
        // await _domainEventPublisher.PublishAsync(product.DomainEvents, cancellationToken);
        // product.ClearDomainEvents();

        return product.Id;
    }
}

Important distinction:

The service coordinates:

Create Money
Create Product
Save Product
Return ID

The domain protects rules:

Money cannot be negative.
Product name cannot be empty.
ProductCreatedEvent is raised when product is created.
Unit Test Examples
Test aggregate invariant
using Topic10.DomainDrivenDesign.Domain.ValueObjects;

namespace Topic10.DomainDrivenDesign.Tests;

public sealed class MoneyTests
{
    [Fact]
    public void Cannot_create_money_with_negative_amount()
    {
        Assert.Throws<ArgumentException>(() =>
            new Money(-1, "GBP"));
    }

    [Fact]
    public void Money_with_same_amount_and_currency_is_equal()
    {
        var money1 = new Money(10, "GBP");
        var money2 = new Money(10, "gbp");

        Assert.Equal(money1, money2);
    }
}
Test product creation raises event
using Topic10.DomainDrivenDesign.Domain.Events;
using Topic10.DomainDrivenDesign.Domain.Products;
using Topic10.DomainDrivenDesign.Domain.ValueObjects;

namespace Topic10.DomainDrivenDesign.Tests;

public sealed class ProductTests
{
    [Fact]
    public void Creating_product_raises_product_created_event()
    {
        var product = Product.Create("Keyboard", new Money(50, "GBP"));

        var domainEvent = Assert.Single(product.DomainEvents);

        var productCreatedEvent = Assert.IsType<ProductCreatedEvent>(domainEvent);

        Assert.Equal(product.Id, productCreatedEvent.ProductId);
        Assert.Equal("Keyboard", productCreatedEvent.Name);
        Assert.Equal(new Money(50, "GBP"), productCreatedEvent.Price);
    }

    [Fact]
    public void Product_name_cannot_be_empty()
    {
        Assert.Throws<ArgumentException>(() =>
            Product.Create("", new Money(50, "GBP")));
    }
}
What would the xUnit unit test for a service using repository look like?

You can use a fake repository.

using Topic10.DomainDrivenDesign.Application.Products;
using Topic10.DomainDrivenDesign.Domain.Products;

namespace Topic10.DomainDrivenDesign.Tests;

public sealed class CreateProductServiceTests
{
    [Fact]
    public async Task CreateAsync_saves_product_and_returns_id()
    {
        var repository = new FakeProductRepository();
        var service = new CreateProductService(repository);

        var productId = await service.CreateAsync("Mouse", 25, "GBP");

        var savedProduct = await repository.GetByIdAsync(productId);

        Assert.NotNull(savedProduct);
        Assert.Equal("Mouse", savedProduct!.Name);
    }

    private sealed class FakeProductRepository : IProductRepository
    {
        private readonly Dictionary<Guid, Product> _products = new();

        public Task<Product?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            _products.TryGetValue(id, out var product);
            return Task.FromResult(product);
        }

        public Task SaveAsync(
            Product product,
            CancellationToken cancellationToken = default)
        {
            _products[product.Id] = product;
            return Task.CompletedTask;
        }
    }
}

This test does not need a real database.

That is one of the big benefits of DDD + Clean Architecture.

Bounded Context

A Bounded Context is a boundary around a model and its language.

The same word can mean different things in different parts of a business.

Example:

Product in Sales context:
- Name
- Price
- Discount
- Stock status

Product in Warehouse context:
- SKU
- Weight
- Storage location
- Reorder level

Product in Finance context:
- Cost
- Tax category
- Revenue code

All are called Product, but they do not necessarily mean the same thing.

Interview answer:

A bounded context defines where a model and its language are valid. The same term can have different meanings in different contexts.

Anti-Corruption Layer

An Anti-Corruption Layer, or ACL, protects your domain model from another system’s model.

Example:

An external system sends this:

{
  "item_code": "ABC123",
  "item_label": "Keyboard",
  "price_in_pence": 5000,
  "currency_code": "GBP"
}

But your domain wants this:

Product.Create("Keyboard", new Money(50, "GBP"));

So you create a translator:

public sealed class ExternalProductTranslator
{
    public Product ToDomainProduct(ExternalProductDto dto)
    {
        var amount = dto.PriceInPence / 100m;

        var price = new Money(amount, dto.CurrencyCode);

        return Product.Create(dto.ItemLabel, price);
    }
}

Interview answer:

An anti-corruption layer translates between models so that external systems do not leak their concepts, naming, or data structure into our domain model.

Elekta Interview Questions
What is the difference between an entity and a value object?

An entity has identity.

A value object does not.

Example:

Product is an entity.
Money is a value object.

A Product has an ID and can change over time.

A Money object is just its amount and currency.

Good interview answer:

An entity is compared by identity, while a value object is compared by its values. Entities have a lifecycle. Value objects are usually immutable and do not need an ID.

Why should domain logic not depend on the database?

Because the domain should represent business rules, not technical details.

Bad:

public void ChangePrice(decimal amount)
{
    using var db = new AppDbContext();

    if (amount < 0)
        throw new Exception();

    db.SaveChanges();
}

This mixes:

Business rule
Database access
Infrastructure

Better:

public void ChangePrice(Money newPrice)
{
    Price = newPrice;
}

Then the repository saves it later.

Good interview answer:

Domain logic should not depend on the database because business rules should be testable and independent of infrastructure. The database is an implementation detail.

How do you test an aggregate's invariant enforcement?

You create the aggregate directly and check that invalid state is rejected.

Example:

[Fact]
public void Cannot_create_product_with_empty_name()
{
    Assert.Throws<ArgumentException>(() =>
        Product.Create("", new Money(10, "GBP")));
}

And:

[Fact]
public void Cannot_create_negative_money()
{
    Assert.Throws<ArgumentException>(() =>
        new Money(-10, "GBP"));
}

Good interview answer:

I test aggregate invariants with unit tests against the domain model directly. I do not need a database or API because the business rules are inside the aggregate.

What happens when two aggregates need to collaborate?

This is very important.

In DDD, one aggregate should usually not directly modify another aggregate.

Instead, you coordinate through:

Application service
Domain service
Domain events
Eventually consistent workflow

Example:

When a Product is created, another aggregate might need to react.

Bad:

product.CreateAndUpdateInventory(inventory);

Better:

ProductCreatedEvent

Then an event handler updates another part of the system.

Good interview answer:

Aggregates should be modified through their own root. If two aggregates need to collaborate, I would usually coordinate that in an application service or use domain events. I avoid one aggregate directly changing another aggregate because that makes transactional boundaries unclear.

Clean Architecture connection

DDD works very well with Clean Architecture.

The dependency direction should look like this:

API
 ↓
Application
 ↓
Domain
 ↑
Infrastructure implements Domain interfaces

More accurately:

API depends on Application
Application depends on Domain
Infrastructure depends on Domain
Domain depends on nothing

The Domain layer should not reference:

Entity Framework
SQL Server
ASP.NET controllers
HTTP
JSON
Logging frameworks
Email services
Message queues

The Domain layer should contain:

Entities
Value Objects
Aggregates
Domain Events
Repository Interfaces
Domain Services
Business Rules
The most important interview summary

Say this:

In DDD, the domain model contains the business concepts and rules. Entities have identity, value objects are compared by value, and aggregates protect invariants. The aggregate root is the only object outside code should use to modify the aggregate. Domain events represent important things that happened. Repositories abstract persistence, with interfaces in the Domain layer and implementations in Infrastructure. This keeps the domain independent, testable, and focused on business behaviour.

That answer is strong for an Elekta-style interview.