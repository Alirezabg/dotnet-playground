# C# Language Practice — Product Catalogue

This folder contains one sub-folder per C# language topic.
Work through them in order — your instructor (Copilot) will guide each session.

## How a Session Works
1. Open the topic folder
2. Ask Copilot: _"Let's start a C# session on [topic]"_ — it will check progress notes first
3. Copilot introduces the concept, you explain it back, then you write code
4. Copilot reviews and updates `progress-notes/csharp-progress.md`

## Topic Sequence

| # | Topic | Folder | Status |
|---|-------|--------|--------|
| 1 | Records & Types (`record`, `class`, `struct`, immutability) | `01-records-and-types/` | Not started |
| 2 | Pattern Matching (switch expressions, property patterns) | `02-pattern-matching/` | Not started |
| 3 | LINQ (filtering, projection, aggregation) | `03-linq/` | Not started |
| 4 | Generics & Constraints | `04-generics/` | Not started |
| 5 | Nullable Reference Types & null safety | `05-nullable-reference-types/` | Not started |
| 6 | Delegates, `Func`/`Action` & Events | `06-delegates-and-events/` | Not started |
| 7 | Collections & Iteration (`IEnumerable`, `yield`) | `07-collections-and-iteration/` | Not started |
| 8 | Exceptions & Error Handling | `08-exceptions-and-error-handling/` | Not started |
| 9 | Extension Methods | `09-extension-methods/` | Not started |
| 10 | Equality, Comparison & Operators | `10-equality-and-comparison/` | Not started |

## Domain Context
All exercises use the **Product Catalogue** domain — treated as one microservice in a larger DDD platform:
- Aggregates: `Product` (aggregate root), `Category`
- Value Objects: `Money`, `ProductName`
- Domain Events: `ProductCreatedEvent`, `PriceChangedEvent`

Practice the language feature in isolation here, then apply it in `aspnet-practice/` and `oop-practice/`.

> **Tip:** This track is about *language fluency*. The interviewer will probe **why** a feature exists and **when** you'd reach for it — not just the syntax.
