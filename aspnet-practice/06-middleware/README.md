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
