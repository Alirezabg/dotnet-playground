# Study Guide — Questions & Tasks

Use this with [`clean/`](clean/). Try to answer **out loud** as if in an interview, then check the code.
Topics map to your practice tracks in `csharp-practice/` and `aspnet-practice/`.

---

## Part 1 — "Walk me through this code"

Open [`Program.cs`](clean/src/Catalogue.Api/Program.cs) and answer:

1. What happens between `CreateBuilder` and `Build()` vs after `Build()`? Why can't you swap the order?
2. Why is `ExceptionHandlingMiddleware` registered **before** `RequestLoggingMiddleware` and the endpoints?
3. `IProductRepository` is registered as a **singleton**. Is that safe here? What made it safe? *(see [`InMemoryProductRepository`](clean/src/Catalogue.Api/Infrastructure/InMemoryProductRepository.cs))*

## Part 2 — Domain (C# + DDD)

Open [`Product.cs`](clean/src/Catalogue.Api/Domain/Product.cs), [`Money.cs`](clean/src/Catalogue.Api/Domain/Money.cs), [`ProductName.cs`](clean/src/Catalogue.Api/Domain/ProductName.cs):

4. `Money` and `ProductName` are `record`s but `Product` is a `class`. Why the difference? *(value vs identity equality)*
5. `Product` overrides `Equals`/`GetHashCode` by `Id`. Why not let the compiler do it? Why must the two agree?
6. Why are `Name`, `Price`, `Status` setters `private`? How would you change a price — and what does that raise?
7. `Money`'s constructor validates and throws `InvalidMoneyException`. Where is that exception turned into an HTTP 400? *(trace it to [`ExceptionHandlingMiddleware`](clean/src/Catalogue.Api/Middleware/ExceptionHandlingMiddleware.cs))*
8. What's the risk of the public `Money(decimal, string)` constructor vs a static factory? When would you tighten it?

## Part 3 — API (ASP.NET)

Open [`ProductEndpoints.cs`](clean/src/Catalogue.Api/Endpoints/ProductEndpoints.cs):

9. Why `Results<Ok<ProductResponse>, NotFound>` instead of returning `IResult`? What does it give OpenAPI?
10. Why does `POST` return `201 Created` with a location instead of `200 OK`?
11. There's validation in the endpoint **and** invariants in the domain. Isn't that duplication? Defend it.
12. Why map `Product` → `ProductResponse` instead of returning the aggregate? *(see [`ProductDtos.cs`](clean/src/Catalogue.Api/Contracts/ProductDtos.cs))*
13. What does `MapGroup("/products")` buy you over three separate `MapGet/MapPost` calls?

## Part 4 — Testing (V Model)

Open [`ProductTests.cs`](clean/tests/Catalogue.Tests/Domain/ProductTests.cs) and [`ProductsEndpointTests.cs`](clean/tests/Catalogue.Tests/Api/ProductsEndpointTests.cs):

14. Which tests are **unit** and which are **integration**? How can you tell at a glance?
15. What does `WebApplicationFactory<Program>` host, and why is `public partial class Program {}` needed?
16. When would you use `[Theory]` + `[InlineData]` over `[Fact]`? Point to an example.
17. The integration tests share a singleton repository. Why don't they interfere? When *would* shared state bite you?

## Part 5 — Code review (find the bugs)

Open [`code-review/src/BadCatalogue.Api/Program.cs`](code-review/src/BadCatalogue.Api/Program.cs). **Before** reading the answer key:

18. List every problem you can find (aim for 8+). Group them: correctness, safety, async, testing, API design.
19. Which single issue is the **most dangerous** in safety-critical software, and why?
20. Now open [`code-review/REVIEW.md`](code-review/REVIEW.md). How many did you catch?

---

## Hands-on tasks (do these in order)

### Task A — Complete the stubbed test
File: [`GetAllProductsTests.cs`](clean/tests/Catalogue.Tests/Api/GetAllProductsTests.cs)
- POST two products, GET `/products`, assert both ids are present.
- Remove the `Skip`, run `dotnet test`. Which V Model level is this?

### Task B — Add an `Activate` endpoint
- Add `POST /products/{id:guid}/activate` that moves a product to `Active`.
- Decide the responses: `Results<Ok<ProductResponse>, NotFound>`.
- Add one integration test for found + not-found.
- 📌 Think: should "already active" be an error or idempotent?

### Task C — Add validation for a maximum price
- Reject prices above a sensible ceiling at the boundary **and** state where the invariant belongs.
- Add a unit test and an endpoint test for the rejection (expect 400).

### Task D — Introduce a `Result<T>` for "not found"
- Discuss with your coach: exception vs result type for expected "not found".
- Sketch how `GetByIdAsync` could return a result instead of `null`. What changes in the endpoint?

### Task E — Fix three issues in `code-review/`
- Pick three from your Part 5 list, fix them, and diff your result against `clean/`.
- Ask the **Coding Instructor** agent to review — it won't fix them for you, only guide.

---

> 📌 **Elekta framing:** for every endpoint, be ready to say (a) which test level proves it, (b) what happens
> when it fails, and (c) who is responsible for recovery. Correctness over cleverness.
