# Topic 8 — Dependency Injection

**In your own words:**
> _Your answer here..._

## Task
Wire up `IProductService` → `ProductService` → `IProductRepository` → `InMemoryProductRepository`
using the .NET 8 built-in DI container in `Program.cs`.

Then explain the lifetime you chose for each registration and why.

## Questions Your Instructor Will Ask
- What is the difference between `AddScoped`, `AddSingleton`, and `AddTransient`?
- What is a "captive dependency" and why is it a problem?
- How would you test a class that depends on a scoped service?

## Interview Tip
> DI lifetime questions are extremely common. Know the "Scoped inside Singleton" anti-pattern.


Program.cs registration in .NET 8
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
Why I chose AddScoped
One instance per HTTP request.

I would usually register services and repositories as scoped in a web app, especially when using database contexts. For an in-memory demo repository, singleton can preserve data during the app lifetime, but it requires thread-safety considerations.


AddTransient vs AddScoped vs AddSingleton
AddSingleton
Creates one instance for the entire application lifetime.
Use it for things that are safe to share globally.

AddScoped
Creates one instance per HTTP request.
Use it for business services, repositories, and database-related services.


Transient
Every time requested
Shared for Nothing
Lightweight stateless services

What is Dependency Injection?

Dependency Injection is a technique where dependencies are provided from outside a class instead of being created inside the class.

Better answer:

DI is a way to implement Dependency Inversion. High-level classes depend on abstractions, and the DI container provides the concrete implementations at runtime.

What is a captive dependency?

A captive dependency is when a long-lived service captures a shorter-lived dependency.
The singleton service holds onto a scoped repository for too long.


Testing a class that depends on a scoped service

The good news is: your class does not know whether the dependency is scoped, transient, or singleton.

Best registration for this task

For interview/demo:

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();

Explanation:

I chose scoped because this is a web application-style dependency chain. A service and repository usually represent work for one request. Scoped prevents unnecessary object creation like transient, but also avoids sharing mutable request-related state globally like singleton.

Then add nuance:

Since this repository is in-memory, if I wanted the data to persist across requests for a demo, I could make the repository singleton. But in a real app, I would usually use a database and keep repositories and DbContext scoped.