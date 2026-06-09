# Topic 2 — Routing & Attribute Routing (Controllers)

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (How does endpoint routing work? What do `[Route]` and the `[HttpGet]`/`[HttpPost]` attributes give you?)_

## Your Task
Define the Product Catalogue routes with an MVC controller.
- Create a `ProductsController : ControllerBase` decorated with `[ApiController]` and `[Route("products")]`
- Map `GET /products`, `GET /products/{id:guid}`, `POST /products` using `[HttpGet]`, `[HttpGet("{id:guid}")]`, `[HttpPost]`
- Use a route constraint so `{id}` must be a `Guid`
- Attach shared metadata (e.g. `[Tags("Products")]`) to the whole controller

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- How does ASP.NET endpoint routing work, and how do controller attribute routes (`[Route]`, `[HttpGet]`) get discovered and matched?
- How do route constraints like `{id:guid}` affect matching and the response when they fail?
- How would you apply a cross-cutting concern (auth policy, filter) to a whole controller — controller-level attributes vs per-action?

## Interview Tip
> On a controller, the `[Route]` prefix plus `[HttpGet]`/`[HttpPost]` attributes are where you describe routes; controller-level attributes (`[Authorize]`, `[Tags]`, filters) apply to every action. Mention route constraints as cheap, early input validation.
