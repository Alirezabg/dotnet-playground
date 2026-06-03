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
