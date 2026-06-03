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
