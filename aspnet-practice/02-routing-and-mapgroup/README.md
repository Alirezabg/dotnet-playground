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


Endpoint routing is the ASP.NET Core system that matches an incoming HTTP request to an endpoint/action based on the URL, HTTP verb, and route template. In controller APIs, [Route] defines the shared route pattern, and [HttpGet], [HttpPost], etc. define which HTTP method and sub-route map to each action. Attribute routing makes routes explicit and close to the controller code.

In controller-based APIs, endpoint routing builds a list of endpoints from controller attributes. [Route("products")] gives the controller a shared route prefix, and [HttpGet], [HttpPost], and other HTTP attributes map specific actions to verbs and templates. A route constraint like {id:guid} means the action only matches when the route value is a valid Guid. For shared behaviour like authorization, tags, or filters, I can put attributes at controller level so they apply to every action.