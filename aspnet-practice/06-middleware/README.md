# Topic 6 — Middleware (logging, error handling)

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is the middleware pipeline? What does `next` do and why does order matter?)_

## Your Task
Add cross-cutting behaviour to the pipeline.
- A request-logging middleware that records method, path, and elapsed time
- A global exception-handling middleware that turns unhandled errors into a `ProblemDetails` response
- Explain where each must sit in the pipeline relative to routing and endpoints
- Show the difference between `app.Use(...)` and a class-based `IMiddleware`

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- Why does middleware order matter? What breaks if exception handling is registered *after* the endpoints?
- What's the difference between `Use`, `Run`, and `Map` in the pipeline?
- In a safety-critical system, what must a global error handler never do (e.g. leak internals)?

## Interview Tip
> Order is everything. Exception/error middleware goes **early** (outermost) so it can catch failures from everything downstream. Use `ProblemDetails` for consistent error shapes.
