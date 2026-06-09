# Topic 10 — OpenAPI / Swagger Setup

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is OpenAPI? What does it generate and who consumes it?)_

## Your Task
Document the Product Catalogue API.
- Enable OpenAPI document generation for the MVC API (`AddEndpointsApiExplorer()` + `AddSwaggerGen()`)
- Annotate controller actions with `[ProducesResponseType(...)]`, `[Tags(...)]`, and XML doc comments
- Show how `[ApiController]` + typed `ActionResult<T>` (Topic 5) make the generated schema accurate without extra work
- Describe how a frontend or another service consumes the resulting document

Write your code below in a new `.cs` file in this folder (or describe the registration).

## Questions Your Instructor Will Ask
- What does the OpenAPI document actually contain, and who are its consumers?
- How do `[ProducesResponseType]` and `ActionResult<T>` improve the generated schema?
- Would you expose Swagger UI in production? Why or why not, and what are the alternatives?

## Interview Tip
> Connect it back: accurate OpenAPI is mostly a *free* by-product of typed results and good metadata — it's a sign of a well-described contract, not extra busywork.
