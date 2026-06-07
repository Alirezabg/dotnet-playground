# Topic 4 — Generics & Constraints

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (Why generics over `object`? What do type constraints buy you?)_

## Your Task
Design a generic `IRepository<T>` abstraction for the Product Catalogue.
- Define `Add`, `GetById`, and `GetAll` over a type parameter `T`
- Add a constraint so `T` must be an entity with an `Id` (e.g. `where T : IEntity`)
- Provide a simple in-memory implementation backed by a `Dictionary<Guid, T>`

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What problem do generics solve that `object` + casting does not?
- What does the `where T : ...` constraint give you inside the method body?
- What is the difference between covariance and contravariance, and where have you seen `out`/`in` on an interface?

## Interview Tip
> Be ready to explain *type safety* and *no boxing* as the two headline benefits of generics over `object`.
