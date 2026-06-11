# Topic 7 — Validation

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (Where should validation live — endpoint, DTO, or domain? Probably more than one.)_

## Your Task
Validate `CreateProductRequest` before it reaches the domain.
- Reject empty names, negative prices, and unknown currencies
- Return `400 Bad Request` with a clear, structured error (e.g. `ValidationProblem` / `ProblemDetails`)
- Decide what belongs at the API boundary vs inside the `Product` aggregate's invariants
- Use `[ApiController]` automatic model validation (DataAnnotations) and/or an action filter so the action body stays clean

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the difference between *input validation* (API boundary) and *domain invariants* (aggregate)?
- Should the domain trust the API layer's validation? Why or why not?
- How does `[ApiController]` automatic 400 model validation work, and when would you add a custom action filter instead?

## Interview Tip
> Strong answer: validate *format/shape* at the boundary (DataAnnotations + `[ApiController]` auto-400), but enforce *business rules* inside the aggregate so the domain is always valid regardless of caller.


Validation belongs both at the API boundary and inside the domain. The API validates input format and returns 400 Bad Request for bad requests. The domain enforces business invariants so a Product can never exist in an invalid state, no matter who creates it.