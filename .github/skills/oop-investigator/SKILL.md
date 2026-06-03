---
name: oop-investigator
description: "OOP investigation and practice for interview prep. Use when starting a new OOP topic (SOLID, design patterns, encapsulation, inheritance, polymorphism, abstraction, interfaces, dependency injection, clean architecture), asking for an investigation into which approach is better, or wanting to practice a concept with guided questions. Always checks progress notes before starting."
argument-hint: "Name the OOP concept or topic you want to explore"
---
# OOP Investigator — Guided Practice & Investigation

## When to Use
- Starting a new OOP concept session
- Comparing approaches (e.g. "interface vs abstract class — which is better here?")
- Asking for an investigation with a recommendation
- Wanting guided questions to deepen understanding of a concept

## Procedure

### Starting a Session
1. Read `progress-notes/oop-progress.md` to understand what has been covered
2. Summarise the last session topic briefly for the user
3. Ask: "What would you like to work on today?" or confirm the next topic in the sequence
4. Introduce the topic with a 2–3 sentence definition
5. Ask the user to explain it back in their own words before any code

### Investigation Mode ("which is better?")
When the user asks to compare two approaches:
1. Identify the trade-offs for each approach in the context of the Product Catalogue domain
2. Recommend the better approach with reasoning
3. Ask the user to implement the recommended approach in the relevant `oop-practice/` folder
4. Review the implementation

**Investigation angles to consider:**
- Testability: which is easier to mock/stub?
- Extensibility: which obeys Open/Closed better?
- Clarity: which communicates intent more clearly?
- C# idioms: which is more idiomatic for the language?

### Practice Mode
1. Give the user a small, realistic task in the Product Catalogue domain
2. Point them to the correct `oop-practice/XX-topic/` folder
3. Do NOT provide the implementation — only the task description
4. When they share their code, give structured feedback:
   ```
   ✓ What works well:
   ⚠ What to reconsider:
   ? Guiding question:
   ```

### End of Session
After each major topic is covered, update `progress-notes/oop-progress.md`:
- Topic name and date
- Key concept taught
- Questions asked and how the user answered
- What the user got right / what needs reinforcement
- Next suggested topic

## OOP Topic Sequence
1. Encapsulation → `oop-practice/01-encapsulation/`
2. Inheritance → `oop-practice/02-inheritance/`
3. Polymorphism → `oop-practice/03-polymorphism/`
4. Abstraction → `oop-practice/04-abstraction/`
5. Interfaces vs Abstract Classes → `oop-practice/05-interfaces-vs-abstract/`
6. SOLID → `oop-practice/06-solid/` (one sub-topic per session)
7. Design Patterns → `oop-practice/07-design-patterns/`
8. Dependency Injection → `oop-practice/08-dependency-injection/`
9. Clean Architecture → `oop-practice/09-clean-architecture/`
10. **Domain Driven Design** → `oop-practice/10-domain-driven-design/` *(Elekta core)*
11. **Async / Await Patterns** → `oop-practice/11-async-patterns/` *(Elekta core)*
12. **Microservices Patterns** → `oop-practice/12-microservices/` *(Elekta core)*

## Interview Tips to Flag
- "Favour composition over inheritance" — when does this apply?
- Interface segregation vs fat interfaces — a classic code smell question
- Why `abstract class` still has value in C# despite `interface` default implementations
- The difference between DI the pattern and DI containers
- **Elekta DDD** — "Explain aggregate root" and "How do you ensure aggregate invariants are never violated?"
- **Elekta async** — "What is the risk of `async void`?" and "How do you handle exceptions from `Task.WhenAll`?"
- **Elekta microservices** — "What is the anti-corruption layer?" and "How do services discover each other?"
- **Safety-critical** — always ask: "What happens when this fails? Who is responsible for recovery?"
