# OOP Practice — Product Catalogue

This folder contains one sub-folder per OOP concept.
Work through them in order — your instructor (Copilot) will guide each session.

## How a Session Works
1. Open the topic folder
2. Ask Copilot: _"Let's start an OOP session on [topic]"_ — it will check progress notes first
3. Copilot introduces the concept, you explain it back, then you write code
4. Copilot reviews and updates `progress-notes/oop-progress.md`

## Topic Sequence

| # | Topic | Folder | Status |
|---|-------|--------|--------|
| 1 | Encapsulation | `01-encapsulation/` | Not started |
| 2 | Inheritance | `02-inheritance/` | Not started |
| 3 | Polymorphism | `03-polymorphism/` | Not started |
| 4 | Abstraction | `04-abstraction/` | Not started |
| 5 | Interfaces vs Abstract Classes | `05-interfaces-vs-abstract/` | Not started |
| 6 | SOLID — Single Responsibility | `06-solid/S-single-responsibility/` | Not started |
| 7 | SOLID — Open/Closed | `06-solid/O-open-closed/` | Not started |
| 8 | SOLID — Liskov Substitution | `06-solid/L-liskov-substitution/` | Not started |
| 9 | SOLID — Interface Segregation | `06-solid/I-interface-segregation/` | Not started |
| 10 | SOLID — Dependency Inversion | `06-solid/D-dependency-inversion/` | Not started |
| 11 | Design Patterns — Creational | `07-design-patterns/creational/` | Not started |
| 12 | Design Patterns — Structural | `07-design-patterns/structural/` | Not started |
| 13 | Design Patterns — Behavioural | `07-design-patterns/behavioural/` | Not started |
| 14 | Dependency Injection | `08-dependency-injection/` | Not started |
| 15 | Clean Architecture | `09-clean-architecture/` | Not started |
| 16 | **Domain Driven Design** *(Elekta core)* | `10-domain-driven-design/` | Not started |
| 17 | **Async / Await Patterns** *(Elekta core)* | `11-async-patterns/` | Not started |
| 18 | **Microservices Patterns** *(Elekta core)* | `12-microservices/` | Not started |

## Domain Context
All exercises use the **Product Catalogue** domain — treated as one microservice in a larger DDD platform:
- Aggregates: `Product` (aggregate root), `Category`
- Value Objects: `Money`, `ProductName`
- Domain Events: `ProductCreatedEvent`, `PriceChangedEvent`
- Other services (conceptual): Inventory, Pricing

This models the kind of domain-based service-oriented architecture Elekta uses in its radiation therapy platform.
