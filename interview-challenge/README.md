# Interview Challenge — Read, Run, Review, Extend

A self-contained mini-project for interview practice. Two versions of the same Product Catalogue service:

| Folder | What it is | Use it to |
|--------|-----------|-----------|
| [`clean/`](clean/) | A correct, idiomatic .NET 8 ASP.NET MVC (controllers) API + xUnit tests | **Read & run** good code; be quizzed on it; extend it |
| [`code-review/`](code-review/) | The same service, deliberately full of bugs/smells | **Review** it like a PR; find the issues; compare to `clean/` |

Study questions and step-by-step "add a test / add an endpoint / add validation" tasks live in [`STUDY.md`](STUDY.md).

> This is a **reference + quiz** artifact. Unlike `csharp-practice/` and `aspnet-practice/` (where *you* write the
> code from scratch), here you study a finished service and one stubbed test ([`GetAllProductsTests`](clean/tests/Catalogue.Tests/Api/GetAllProductsTests.cs)) is left for you to complete.

## Project shape (`clean/`)

```
clean/
  src/Catalogue.Api/
    Program.cs                      # builder → services (AddControllers) → Build → pipeline → MapControllers → Run
    Domain/                         # Product (aggregate), Money + ProductName (value objects), events, exceptions
    Application/IProductRepository   # repository abstraction
    Infrastructure/                 # InMemoryProductRepository (ConcurrentDictionary)
    Contracts/                      # request/response DTOs + mapping
    Controllers/ProductsController.cs # [ApiController], attribute routing, ActionResult<T>, validation
    Middleware/                     # exception handling (ProblemDetails) + request logging
  tests/Catalogue.Tests/
    Domain/                         # UNIT tests (Product, Money)
    Api/                            # INTEGRATION tests (WebApplicationFactory<Program>) + your TODO test
```

## How to run it

> ⚠️ The Copilot agent terminal here does **not** have the .NET SDK. Run these yourself in a normal terminal.

```bash
# from interview-challenge/clean
dotnet build  src/Catalogue.Api/Catalogue.Api.csproj
dotnet run    --project src/Catalogue.Api/Catalogue.Api.csproj   # then open the Swagger UI
dotnet test   tests/Catalogue.Tests/Catalogue.Tests.csproj
```

```bash
# the code-review version compiles and runs too — try breaking and fixing it
dotnet run  --project code-review/src/BadCatalogue.Api/BadCatalogue.Api.csproj
dotnet test code-review/tests/BadCatalogue.Tests/BadCatalogue.Tests.csproj
```

## Suggested workflow
1. **Read** `clean/` top to bottom. For each file, ask "why is it done this way?" (use [`STUDY.md`](STUDY.md)).
2. **Run** the tests; read what each one proves and at which V Model level.
3. **Complete** the stubbed `GetAllProductsTests` test yourself.
4. **Review** `code-review/` (`Program.cs` + `Controllers/ProductsController.cs`) — list every issue before opening [`REVIEW.md`](code-review/REVIEW.md).
5. **Extend**: pick a task from `STUDY.md` (add `/categories`, add a `PUT`, add a `Result<T>` pattern) and implement it.
6. Ask the **Backend Coach** agent or **Coding Instructor** to review your changes.
