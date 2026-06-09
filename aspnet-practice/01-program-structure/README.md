# Topic 1 — Project Structure & `Program.cs`

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What does `WebApplication.CreateBuilder` set up? What's the builder → build → run lifecycle?)_

## Your Task
Sketch the entry point for the Product Catalogue API.
- Create the `WebApplicationBuilder`, register services, build the app, map a health endpoint, run
- Explain the order: service registration **before** `Build()`, middleware/endpoints **after**
- Map a single `GET /` that returns the service name and version

Write your code below in a new `.cs` file in this folder (or describe the structure of `Program.cs`).

## Questions Your Instructor Will Ask
- What's the difference between `builder.Services` and `app` — what can you only do on each side of `Build()`?
- Where does configuration (`appsettings.json`, environment variables) get loaded, and in what precedence?
- How does ASP.NET differ from the classic `Startup.cs` Configure/ConfigureServices split?

## Interview Tip
> Know the lifecycle cold: **register services → `Build()` → configure pipeline + map endpoints → `Run()`**. Doing things in the wrong order is a common bug.
