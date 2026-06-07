# ASP.NET / Minimal API Practice — Product Catalogue

This folder contains one sub-folder per ASP.NET Minimal API topic.
Work through them in order — your instructor (Copilot) will guide each session.

## How a Session Works
1. Open the topic folder
2. Ask Copilot: _"Let's start an API session on [topic]"_ — it will check progress notes first
3. Copilot introduces the concept, you explain it back, then you write code
4. Copilot reviews and updates `progress-notes/api-progress.md`

## Topic Sequence

| # | Topic | Folder | Status |
|---|-------|--------|--------|
| 1 | Project structure & `Program.cs` entry point | `01-program-structure/` | Not started |
| 2 | Routing & `MapGroup()` | `02-routing-and-mapgroup/` | Not started |
| 3 | DTOs — `record` request/response models | `03-dtos-and-records/` | Not started |
| 4 | Dependency injection in endpoints | `04-dependency-injection/` | Not started |
| 5 | `Results<T1, T2>` typed responses | `05-typed-results/` | Not started |
| 6 | Middleware — logging, error handling | `06-middleware/` | Not started |
| 7 | Validation | `07-validation/` | Not started |
| 8 | Integration testing with `WebApplicationFactory<T>` | `08-integration-testing/` | Not started |
| 9 | In-memory repository for testing | `09-in-memory-repository/` | Not started |
| 10 | OpenAPI / Swagger setup | `10-openapi-swagger/` | Not started |

## Domain Context
All exercises use the **Product Catalogue** microservice:
- Endpoints: `GET /products`, `GET /products/{id}`, `POST /products`, `PUT /products/{id}`, `DELETE /products/{id}`, `GET /categories`
- The service owns its own data and communicates via REST + domain events
- Built on **.NET 8 Minimal API** (`Microsoft.NET.Sdk.Web`)

This is the same skeleton in `src/ProductCatalogue.Api/` — practice a concept here, then wire it into the real project.

> **Tip:** The interviewer cares about *correctness and testability* (V Model). For every endpoint, be ready to say which test level proves it: unit, component, or integration.
