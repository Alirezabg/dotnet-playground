# Topic 5 — `ActionResult<T>` Typed Responses

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (How does `ActionResult<T>` improve over returning a raw object or `IActionResult`? What does it give OpenAPI?)_

## Your Task
Return precise status codes from a `ProductsController`.
- Make `GetById` return `ActionResult<ProductResponse>` — `Ok(...)` when found, `NotFound()` when not
- Make `Create` return `CreatedAtAction(nameof(GetById), new { id }, response)` for `201 Created`
- Annotate the actions with `[ProducesResponseType(...)]` for each status code
- Explain how these attributes feed the OpenAPI document

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the benefit of `ActionResult<T>` over returning a plain object or `IActionResult`?
- How do `[ProducesResponseType]` attributes help generate accurate Swagger/OpenAPI metadata?
- Which status code belongs to which outcome — and why does `CreatedAtAction` / `201 Created` need a `Location` header?

## Interview Tip
> `ActionResult<T>` makes the *contract* explicit: the action can return the typed body **or** a status result, and `[ProducesResponseType]` tells OpenAPI exactly which statuses are possible.
