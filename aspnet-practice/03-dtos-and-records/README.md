# Topic 3 — DTOs (`record` request/response models)

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (Why not expose your domain entities directly over the wire?)_

## Your Task
Design the request/response contract for creating a product.
- A `CreateProductRequest` record (name, price, currency, categoryId)
- A `ProductResponse` record (id, name, formatted price, category name)
- Map domain `Product` → `ProductResponse` (a mapping method or extension)
- Explain why the API contract is decoupled from the domain model

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What goes wrong if you serialize your aggregate root directly to JSON?
- Why are `record` types a natural fit for DTOs?
- Where should mapping live, and how do you keep it from leaking domain rules into the API layer?

## Interview Tip
> "Never leak the domain over the wire." DTOs give you a stable public contract while the domain model evolves freely behind it.
