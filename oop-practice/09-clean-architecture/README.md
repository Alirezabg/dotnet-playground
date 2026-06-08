# Topic 9 — Clean Architecture

**In your own words:**
> _Your answer here..._

## Task
Reflect on the code you have built so far. Draw (or describe) the layer boundaries:
- What belongs in the API layer?
- What belongs in the Application/Service layer?
- What belongs in the Domain layer?
- What belongs in the Infrastructure/Repository layer?

Then identify any places in your existing code where layers are mixed.

## Questions Your Instructor Will Ask
- What is the Dependency Rule in Clean Architecture?
- Why should the Domain layer have no dependencies on infrastructure?
- What is the difference between Clean Architecture and N-Tier?

## Interview Tip
> You don't need to know the full "Clean Architecture" book.
> Know the dependency direction and why it matters for testability.


Clean Architecture means organising code so the important business rules are protected from details.

The database is a detail.
The API framework is a detail.
Dependency injection is a detail.
The domain/business rules should not depend on those things.

Clean Architecture separates the system into layers. The inner layers contain business rules, and the outer layers contain details like APIs, databases, and frameworks. Dependencies should point inward, so the domain and application logic can be tested without needing a database or web server.


The Big Picture

Think of your project like this:

API Layer
   ↓
Application / Service Layer
   ↓
Domain Layer

Infrastructure Layer
   ↑
implements interfaces defined by inner layers

API
 └── calls ProductService

Application
 └── ProductService depends on IProductRepository

Domain
 └── Product entity and business rules

Infrastructure
 └── InMemoryProductRepository implements IProductRepository


 Outer layers depend on inner layers.
Inner layers must not depend on outer layers.

1. API Layer

The API layer is the entry point into your application.

In an ASP.NET Core project, this is usually:

Controllers
Minimal API endpoints
Program.cs
Request/response models
HTTP concerns
Authentication setup
Validation setup
DI registration


Bad:

app.MapPost("/products", (Product product) =>
{
    if (product.Price < 0)
    {
        throw new Exception("Invalid price");
    }

    // save product directly here
});

Why is this bad?

Because the API endpoint is now doing business validation. If later you use the same product logic from a console app, background job, or unit test, the rule is trapped inside the API.


2. Application / Service Layer

The Application layer coordinates use cases.

This is where your ProductService belongs.

Example responsibilities:

Create a product
Get all products
Search products
Update a product price
Delete a product
Call repository interfaces
Apply application-level validation
Coordinate multiple domain objects

Because the service should not care whether products come from:

an in-memory list
SQL Server
PostgreSQL
a file
an API


3. Domain Layer

The Domain layer contains the core business model.

This is where your main business objects belong.

For your project, that may include:

Product
CatalogueItem
PhysicalProduct
DigitalProduct
Product rules
Domain exceptions
Value objects

The domain should be plain C# as much as possible.

A strong interview sentence:

The domain layer should contain business rules and should be independent of frameworks, databases, and UI concerns.

4. Infrastructure / Repository Layer

The Infrastructure layer contains technical details.

This is where your repository implementation belongs:

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }
}

This layer answers:

How do we store products?
How do we fetch data?
Are we using SQL, MongoDB, files, memory, or an external API?

Later, you could replace this:

InMemoryProductRepository



a clean structure could look like this:

MyApp.Api
│
├── Program.cs
├── Controllers/
│   └── ProductsController.cs
└── Requests/
    └── CreateProductRequest.cs
MyApp.Application
│
├── Services/
│   ├── IProductService.cs
│   └── ProductService.cs
└── Interfaces/
    └── IProductRepository.cs
MyApp.Domain
│
└── Entities/
    └── Product.cs
MyApp.Infrastructure
│
└── Repositories/
    └── InMemoryProductRepository.cs


    Important: Where should IProductRepository live?

This is a common interview discussion.

You might think:

Repository interface belongs with repository implementation.

But in Clean Architecture, that is usually not ideal.

Bad dependency direction:

Application depends on Infrastructure

That would mean ProductService must reference the infrastructure project just to know about IProductRepository.

Better:

Application owns IProductRepository
Infrastructure implements it

So:

Application:
    IProductRepository

Infrastructure:
    InMemoryProductRepository : IProductRepository

Why?

Because the Application layer says:

I need something that can save and load products.

The Infrastructure layer says:

I know how to do that using memory, SQL, files, etc.


The domain should not depend on infrastructure because infrastructure is a detail that changes often. Keeping the domain independent makes the business logic easier to test, reuse, and protect from framework or database changes.

Clean Architecture vs N-Tier

They look similar, but they are not the same idea.

N-Tier Architecture

Traditional N-Tier usually looks like this:

Presentation Layer
        ↓
Business Logic Layer
        ↓
Data Access Layer
        ↓
Database

Clean Architecture

Clean Architecture focuses on dependency direction.

Instead of the business logic depending on data access, both depend around abstractions.

API → Application → Domain
Infrastructure → Application


Clean Architecture means separating the system into layers where dependencies point inward. The Domain contains business rules and should not depend on frameworks, databases, or APIs. The Application layer contains use cases and depends on abstractions like IProductRepository. The Infrastructure layer implements those abstractions using details like SQL or in-memory storage. The API layer handles HTTP and calls the Application layer. This makes the code easier to test because I can test services with fake repositories without running a database or web server.


Very short memory version

Remember this:

API = HTTP
Application = use cases / services
Domain = business rules / entities
Infrastructure = database / repository implementations

And this:

Dependencies point inward.
Domain depends on nothing.
Application depends on Domain.
Infrastructure depends on Application abstractions.
API calls Application.