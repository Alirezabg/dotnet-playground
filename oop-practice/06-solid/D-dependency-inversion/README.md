# D — Dependency Inversion Principle

**In your own words:**
> _Your answer here..._

## Task
Refactor a `ProductService` that directly instantiates `SqlProductRepository`
to depend on `IProductRepository` instead — and wire it via the DI container in `Program.cs`.

## Interview Tip
> DIP ≠ dependency injection (DI). DIP is the principle; DI is a technique to implement it.
> Be ready to explain both independently.
