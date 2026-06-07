# Topic 2 — Routing & `MapGroup()`

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (How does endpoint routing work? What does `MapGroup` give you?)_

## Your Task
Define the Product Catalogue routes.
- Group all product routes under a `/products` prefix using `MapGroup()`
- Map `GET /products`, `GET /products/{id:guid}`, `POST /products`
- Use a route constraint so `{id}` must be a `Guid`
- Attach shared metadata (e.g. a tag) to the whole group

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What problem does `MapGroup()` solve compared to repeating the prefix on every endpoint?
- How do route constraints like `{id:guid}` affect matching and the response when they fail?
- How would you apply a filter or auth policy to an entire group at once?

## Interview Tip
> `MapGroup()` is where you hang cross-cutting concerns (auth, validation filters, tags) for a whole resource. Mention route constraints as cheap, early input validation.
