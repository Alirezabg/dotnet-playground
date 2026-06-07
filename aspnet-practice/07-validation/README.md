# Topic 7 — Validation

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (Where should validation live — endpoint, DTO, or domain? Probably more than one.)_

## Your Task
Validate `CreateProductRequest` before it reaches the domain.
- Reject empty names, negative prices, and unknown currencies
- Return `400 Bad Request` with a clear, structured error (e.g. `ValidationProblem`)
- Decide what belongs at the API boundary vs inside the `Product` aggregate's invariants
- (Optional) Express it as an endpoint filter so the handler stays clean

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the difference between *input validation* (API boundary) and *domain invariants* (aggregate)?
- Should the domain trust the API layer's validation? Why or why not?
- How does an endpoint filter help keep validation out of the handler body?

## Interview Tip
> Strong answer: validate *format/shape* at the boundary, but enforce *business rules* inside the aggregate so the domain is always valid regardless of caller.
