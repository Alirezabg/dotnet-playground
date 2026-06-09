# Topic 4 — Dependency Injection in Controllers

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What are the three service lifetimes, and how does the container resolve a controller's constructor dependencies?)_

## Your Task
Wire a repository into a controller via DI.
- Register an `IProductRepository` with an in-memory implementation
- Inject it through the `ProductsController` **constructor**
- Choose and justify a lifetime: `Singleton`, `Scoped`, or `Transient`
- Show how the same service is resolved per request

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the difference between `Singleton`, `Scoped`, and `Transient`? Give a real example of each.
- What is a *captive dependency*, and why is injecting a scoped service into a singleton dangerous?
- How does ASP.NET resolve a controller's dependencies — constructor injection vs `[FromServices]` on an action parameter — and when would you use each?

## Interview Tip
> Captive dependencies are a favourite trap: a `Scoped` service captured by a `Singleton` lives too long and breaks per-request isolation (e.g. a shared `DbContext`).
