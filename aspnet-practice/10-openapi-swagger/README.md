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


OpenAPI is a contract for an API. It describes what endpoints exist, what they accept, and what they return. Swagger tooling uses that contract to generate documentation, UI, and client code.

OpenAPI is a machine-readable contract for an API. It lists the endpoints, methods, parameters, request bodies, response types, status codes, and schemas. Swagger tooling uses that contract to generate interactive documentation and client code. In ASP.NET Core, AddEndpointsApiExplorer() and AddSwaggerGen() generate the document, while UseSwagger() and UseSwaggerUI() expose it. Typed responses like ActionResult<ProductResponse> and attributes like [ProducesResponseType] make the generated contract more accurate.

Swagger/OpenAPI is useful, but it exposes the shape of your API. In production, I would harden it by controlling who can access it, removing unnecessary detail, and making sure the OpenAPI document matches the API’s real security model.


in ASP.NET Core the common equivalent is NWebsec for security headers, or you can add headers yourself with middleware; for Swagger/API hardening you also use built-in middleware like UseAuthentication(), UseAuthorization(), UseHttpsRedirection(), CORS, and rate limiting.