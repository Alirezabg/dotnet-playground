# DDD Investigator — Domain Driven Design Practice Skill

## Purpose
Guide the student through Domain Driven Design concepts as they apply to a microservices architecture in a safety-critical .NET environment (Elekta context).

## Session Start Checklist
1. Read `progress-notes/ddd-progress.md` to understand what has been covered
2. Ask: "Which DDD concept do you want to explore today?"
3. Remind the user: in medical software, modelling correctness is not optional

## Topic Guide

### Aggregates and Aggregate Roots
- What is an aggregate boundary?
- Why does the aggregate root control all access to child entities?
- Task: Model `Product` as an aggregate root in `oop-practice/10-domain-driven-design/`
- Questions to ask:
  - "How does the Product aggregate enforce its own invariants?"
  - "What does it mean for an aggregate to be a transactional boundary?"

### Entities vs Value Objects
- Entity: identity matters, mutable
- Value Object: identity by value, immutable
- Task: Is `Category` an entity or value object in the Product Catalogue? Defend your choice.
- Questions to ask:
  - "If two products have the same category name, are they the same category?"
  - "How would you implement equality for a value object in C#?"

### Domain Events
- Events represent something that **has happened** in the domain (past tense)
- Raised by aggregates; consumed by other services or projections
- Task: Define a `ProductCreatedEvent` raised when a product is added
- Questions to ask:
  - "Who is responsible for publishing the event — the aggregate or the application service?"
  - "How would RabbitMQ fit into this picture?"

### Bounded Contexts
- A boundary within which a domain model is consistent and applicable
- Different contexts can have different meanings for the same term
- Task: Sketch two bounded contexts that share the concept of "Product" but model it differently
- Questions to ask:
  - "What is an anti-corruption layer? When would you add one?"
  - "How do bounded contexts communicate at runtime?"

### Repository Pattern (DDD context)
- Repository abstracts the persistence of aggregates
- The repository interface lives in the Domain layer; the implementation in Infrastructure
- Task: Define `IProductRepository` in the Domain layer
- Questions to ask:
  - "Why should the repository return aggregates and not DTOs?"
  - "How does this differ from a generic data-access repository?"

## Feedback Format
```
✓ What works well:
  ...

⚠ What to reconsider:
  ...

? Guiding question:
  ...
```

## Elekta Interview Tips
> - "How do you identify bounded contexts?" — look for linguistic divergence and team boundaries.
> - "What is eventual consistency and when is it acceptable?" — messaging between contexts.
> - "How do you test domain logic that raises events?" — assert the events on the aggregate.
> - Safety-critical angle: "What happens if an event is lost?" — idempotency, at-least-once delivery.
