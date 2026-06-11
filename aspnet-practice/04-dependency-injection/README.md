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


Dependency Injection means the controller does not create its own dependencies with new. Instead, ASP.NET Core’s DI container creates the controller and supplies registered services through its constructor.

The three lifetimes are:

Lifetime	Meaning	Example
Singleton	One instance for the whole app	App settings, cache
Scoped	One instance per HTTP request	Repository, DbContext, unit of work
Transient	New instance every time it is requested	Small stateless helper service

For a repository, especially one that may later use EF Core DbContext, choose Scoped. In normal ASP.NET Core web apps, scoped services are reused within the same request and disposed after the request ends.

ASP.NET Core has a built-in DI container. I register services in builder.Services, usually against an interface. When a request matches a controller action, ASP.NET creates the controller and resolves its constructor parameters from the container. Singleton creates one instance for the app, Scoped creates one instance per request, and Transient creates a new instance every time. For repositories I usually choose Scoped, because repositories often depend on request-scoped resources like EF Core DbContext. I avoid injecting scoped services into singletons because that creates a captive dependency.