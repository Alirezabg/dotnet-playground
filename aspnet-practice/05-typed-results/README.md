# Topic 5 — `Results<T1, T2>` Typed Responses

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (How do typed results improve over returning `IResult`? What do they give OpenAPI?)_

## Your Task
Return precise status codes from a `GET /products/{id}` handler.
- Use `Results<Ok<ProductResponse>, NotFound>` as the return type
- Return `TypedResults.Ok(...)` when found, `TypedResults.NotFound()` when not
- Add a `POST /products` returning `Results<Created<ProductResponse>, BadRequest<string>>`
- Explain how typed results feed the OpenAPI document automatically

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the benefit of `Results<T1, T2>` over returning a plain `IResult`?
- How do `TypedResults` help generate accurate Swagger/OpenAPI metadata?
- Which status code belongs to which outcome — and why does `201 Created` need a `Location` header?

## Interview Tip
> Typed results make the *contract* explicit and testable: the compiler and OpenAPI both know exactly which statuses an endpoint can return.
