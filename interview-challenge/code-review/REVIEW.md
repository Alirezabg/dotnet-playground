# Code Review — Answer Key 🔒

> **Spoiler warning.** Try to find the issues in [`src/BadCatalogue.Api/Program.cs`](src/BadCatalogue.Api/Program.cs),
> [`src/BadCatalogue.Api/Controllers/ProductsController.cs`](src/BadCatalogue.Api/Controllers/ProductsController.cs),
> and [`tests/BadCatalogue.Tests/ProductTests.cs`](tests/BadCatalogue.Tests/ProductTests.cs) yourself **first**.
> Then check your list against this one.

## Program.cs

| # | Issue | Why it matters | Fix |
|---|-------|----------------|-----|
| 1 | **Catch-all middleware sets status 200** | Every failure is hidden and reported as success — catastrophic in safety-critical software. | Log the error and return `ProblemDetails` with a real status (400/500). |
| 2 | **Returns domain entities directly** (`store.Products`, `product`) | Leaks the internal model; serialises mutable state and any future internal fields. | Map to a `ProductResponse` DTO. |
| 3 | **Missing product returns `null` → HTTP 200** | Caller can't tell "not found" from "found nothing". | Return `ActionResult<ProductResponse>` and `NotFound()` when missing. |
| 4 | **No input validation** | Negative price, empty name, null currency all persist — invalid domain state. | Validate at the boundary and enforce invariants in the aggregate. |
| 5 | **Loose `string id` + `.ToString()` comparison** | No route constraint; string compare instead of `Guid`. | Use `{id:guid}` and compare `Guid` values. |
| 6 | **`DateTime.Now`** | Local time, not UTC — breaks across time zones and servers. | Use `DateTimeOffset.UtcNow`. |
| 7 | **`.Wait()` on async work** | Blocks a thread-pool thread; can deadlock; defeats async. | `await` it: make the handler `async` and `await SaveToDiskAsync(...)`. |
| 8 | **`.First(...)` on reprice** | Throws if the id is unknown → unhandled 500 (and here, swallowed to 200). | Use `FirstOrDefault` and return `NotFound`. |
| 9 | **Singleton `ProductStore` over `List<T>`** | Not thread-safe; concurrent requests can corrupt the list. | Use `ConcurrentDictionary` (or proper synchronisation). |
| 10 | **Anaemic model with public setters** | No invariants; any code can set an invalid state. | Encapsulate behind methods; validate in the constructor. |
| 11 | **Magic string `"ok"` returned from POST** | Not a real response; wrong status (should be 201 Created + Location). | `CreatedAtAction(nameof(GetById), new { id }, dto)`. |
| 12 | **No `CancellationToken`** | Long-running work can't be cancelled. | Accept and pass `CancellationToken` through async calls. |
| 13 | **No OpenAPI / endpoint metadata** | Undocumented contract. | Add `AddEndpointsApiExplorer`/Swagger and `[ProducesResponseType]`/`[Tags]` on actions. |

## ProductTests.cs

| # | Issue | Why it matters | Fix |
|---|-------|----------------|-----|
| A | **Shared `static` store across tests** | Tests are not isolated; results depend on each other. | Fresh `ProductStore` per test (constructor / local). |
| B | **Order dependency** (`Assert.Equal(2, ...)`) | Passes only if the other test ran first; xUnit gives no order guarantee. | Make each test self-contained. |
| C | **Weak assertion** (`Count > 0`) | Passes even when behaviour is wrong. | Assert the exact expected outcome. |
| D | **Negative price added but never asserted** | The real bug (no validation) isn't tested. | Add a test asserting invalid input is rejected. |
| E | **No error-path tests** | Only happy paths covered. | Add not-found / invalid-input cases. |

## How to use this
Pick **three** issues and fix them in a copy, then compare your fixed version against the
[`clean/`](../clean/) reference. Ask your coach to review your fixes the same way an interviewer would.
