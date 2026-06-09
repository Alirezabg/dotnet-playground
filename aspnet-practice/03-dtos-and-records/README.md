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
- Where should mapping (domain → DTO) live so it doesn't leak domain rules into the controller?

## Interview Tip
> "Never leak the domain over the wire." DTOs give you a stable public contract while the domain model evolves freely behind it.


A DTO is a simple object used for data going into or out of the API. We do not expose domain entities directly because the domain model contains business rules, internal structure, and may change often. DTOs give the API a stable public contract while the domain can evolve separately.

namespace ProductCatalogue.Api.Contracts;

// Request DTO: what the client sends to create a product
public record CreateProductRequest(
    string Name,
    decimal Price,
    string Currency,
    Guid CategoryId
);

// Response DTO: what the API sends back to the client
public record ProductResponse(
    Guid Id,
    string Name,
    string FormattedPrice,
    string CategoryName
);

Why not expose domain entities directly?

Because your domain model is for business logic, not for JSON.

For example, your Product entity might contain:

private setters
domain events
validation rules
navigation properties
internal methods
relationships to other objects

You do not want all of that accidentally serialized and sent to the client.

Bad idea:

return Results.Ok(product); // exposes the domain entity directly

Better idea:

return Results.Ok(product.ToResponse()); // exposes only the API contract



Why are records good for DTOs?

record types are a natural fit because DTOs are usually just data containers.

public record ProductResponse(
    Guid Id,
    string Name,
    string FormattedPrice,
    string CategoryName
);

Where should mapping live?

Good places:

API layer
Application layer
Mapping folder
Extension method class


Easy answer for your instructor

What goes wrong if you serialize your aggregate root directly to JSON?

You leak internal domain structure, private business concepts, navigation properties, and maybe sensitive data. It also couples the API contract to the domain model, so changing the domain could accidentally break clients.

Why are record types a natural fit for DTOs?

DTOs are mostly immutable data shapes. Records give us a concise way to define request and response models with value-style equality and less boilerplate.

Where should mapping live?

Mapping usually lives at the API boundary or application boundary. It should translate between DTOs and domain objects, but it should not contain domain rules. Business decisions stay in the domain/application layer.

Final interview line:

DTOs protect the boundary of the system. The API exposes stable request and response models, while the domain model stays focused on business behavior and can change without breaking clients.