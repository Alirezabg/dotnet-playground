# Topic 6 — Middleware (logging, error handling)

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is the middleware pipeline? What does `next` do and why does order matter?)_

## Your Task
Add cross-cutting behaviour to the pipeline.
- A request-logging middleware that records method, path, and elapsed time
- A global exception-handling middleware that turns unhandled errors into a `ProblemDetails` response
- Explain where each must sit in the pipeline relative to routing and `MapControllers`
- Show the difference between middleware (`app.Use(...)` / `IMiddleware`) and an MVC **action filter** (`IActionFilter` / `IAsyncActionFilter`)

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- Why does middleware order matter? What breaks if exception handling is registered *after* `MapControllers`?
- What's the difference between `Use`, `Run`, and `Map` in the pipeline — and when would you reach for an action filter instead of middleware?
- In a safety-critical system, what must a global error handler never do (e.g. leak internals)?

## Interview Tip
> Order is everything. Exception/error middleware goes **early** (outermost) so it can catch failures from everything downstream, including controllers. Middleware = whole-pipeline concerns; action filters = per-action concerns close to MVC model binding.


Interview definition

Middleware is a chain of components that every HTTP request passes through before it reaches the endpoint/controller, and every response passes back through on the way out.

Each middleware can:

inspect or change the request
stop the pipeline and return a response
call next() to pass control to the next middleware
run code after the next middleware finishes

Order matters because middleware wraps everything after it.
So error handling must be registered early, before controllers, so it can catch exceptions thrown later in the pipeline.

```
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();

var app = builder.Build();

//
// 1. Global exception handling goes EARLY.
// It wraps everything after it, including routing and controllers.
//
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        var exceptionFeature =
            context.Features.Get<IExceptionHandlerFeature>();

        // In real systems, log the exception here.
        // Do NOT expose exception details to the client.
        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occurred.",
            Detail = "The server could not complete the request.",
            Instance = context.Request.Path
        };

        await context.Response.WriteAsJsonAsync(problem);
    });
});

//
// 2. Request logging middleware.
// This also goes before controllers so it can measure the whole request.
//
app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();

    var method = context.Request.Method;
    var path = context.Request.Path;

    try
    {
        await next(); // Pass request to the next middleware/controller
    }
    finally
    {
        stopwatch.Stop();

        Console.WriteLine(
            $"{method} {path} responded {context.Response.StatusCode} in {stopwatch.ElapsedMilliseconds}ms");
    }
});

//
// 3. Routing decides which endpoint/controller matches the request.
//
app.UseRouting();

//
// 4. Authorization would usually go here, after routing.
// app.UseAuthentication();
// app.UseAuthorization();

//
// 5. Controllers/endpoints come near the end.
//
app.MapControllers();

app.Run();


// Example controller for testing
[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "Keyboard" },
            new { Id = 2, Name = "Mouse" }
        });
    }

    [HttpGet("boom")]
    public IActionResult ThrowError()
    {
        throw new InvalidOperationException("Database failed!");
    }
}
```


Browser/Postman
   ↓
Middleware 1: exception handling
   ↓
Middleware 2: logging
   ↓
Middleware 3: routing
   ↓
Controller action
   ↓
Response goes back


Middleware is a component in the ASP.NET Core request pipeline. Each middleware receives the HttpContext and can either handle the request, stop it, or call next() to pass it on. Order matters because middleware wraps everything after it. For example, exception handling should be registered early so it can catch errors from controllers. Middleware is best for whole-application concerns like logging and error handling, while action filters are better for controller/action-specific logic.
