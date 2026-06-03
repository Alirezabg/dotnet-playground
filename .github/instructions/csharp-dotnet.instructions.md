---
description: "Use when writing or reviewing C# .NET 8 minimal API code, endpoints, middleware, services, or repositories in the ProductCatalogue project."
applyTo: "src/**/*.cs"
---
# C# .NET 8 Minimal API — Coding Guidelines (Elekta-aligned)

## Project Stack
- .NET 8 Minimal API — one microservice in a larger domain-based platform
- C# 12 features (primary constructors, collection expressions, etc.)
- xUnit for testing — unit, component, and integration levels (V Model)
- Async/await throughout — this is an event-driven, message-queue-based system
- Domain Driven Design — aggregates own state, domain events cross boundaries

## General Rules
- Prefer `record` types for immutable DTOs and request/response models
- Use primary constructors where it simplifies the code
- Avoid magic strings — use constants or strongly-typed route groups
- Always return appropriate HTTP status codes (not just 200)
- Validate inputs at the API boundary; don't let bad data reach domain logic
- All I/O-bound operations must be `async` — no `.Result` or `.Wait()` calls
- Handle `CancellationToken` on all async endpoint methods

## Domain Driven Design Conventions
- Domain entities live in a `Domain/` folder and have **no dependency** on infrastructure
- Aggregates enforce invariants — state changes go through methods, not property setters
- Value Objects are immutable — equality by value, not reference
- Domain Events are raised by aggregates and published to a message bus (RabbitMQ)
- Repositories return domain aggregates, not raw data objects
- The Application layer (services) orchestrates — it does not contain business logic

## Endpoint Conventions
- Group related endpoints using `MapGroup()` with a shared route prefix
- Use `Results<T1, T2>` for multi-result typed responses
- Name route groups by domain concept (e.g. `/products`, `/categories`)
- Endpoints should call an application service — never hit a repository directly

## Async and Messaging Patterns
- Use `async Task<T>` consistently — never `async void` (except event handlers)
- Await all async calls; never fire-and-forget unless explicitly intended
- When publishing events, handle failures (outbox pattern or at-least-once delivery)
- Be ready to explain the difference between `Task.WhenAll` and `Task.WhenAny`

## Testing Expectations (V Model)
- **Unit**: domain logic tested in isolation — no infrastructure dependencies
- **Component**: service tested with an in-memory repository
- **Integration**: endpoint tested with `WebApplicationFactory<T>`
- Every endpoint: at least one happy-path and one error-path test
- Safety-critical paths: test failure modes explicitly — what happens when messaging fails?

## Interview Tips
> - "Why minimal APIs over controllers?" — know trade-offs around discoverability, middleware, and filter pipelines.
> - "What is the difference between a command and a query?" — CQRS is commonly asked.
> - "How do you prevent a long-running message handler from blocking the thread pool?" — `async/await` with proper backpressure.
