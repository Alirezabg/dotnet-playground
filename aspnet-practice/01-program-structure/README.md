# Topic 1 — Project Structure & `Program.cs` (MVC)

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What does `WebApplication.CreateBuilder` set up? What's the builder → build → run lifecycle, and where do controllers plug in?)_

## Your Task
Sketch the entry point for the Product Catalogue API using **MVC controllers**.
- Create the `WebApplicationBuilder`, call `AddControllers()`, build the app, `MapControllers()`, run
- Explain the order: service registration **before** `Build()`, middleware/`MapControllers` **after**
- Add a simple `HealthController` with `GET /health` that returns the service name and version

Write your code below in a new `.cs` file in this folder (or describe the structure of `Program.cs` + a controller).

## Questions Your Instructor Will Ask
- What's the difference between `builder.Services` and `app` — what can you only do on each side of `Build()`?
- Where does configuration (`appsettings.json`, environment variables) get loaded, and in what precedence?
- How does the modern minimal hosting model (`Program.cs` with top-level statements) differ from the classic `Startup.cs` `ConfigureServices`/`Configure` split?

## Interview Tip
> Know the lifecycle cold: **register services → `Build()` → configure pipeline + map endpoints → `Run()`**. Doing things in the wrong order is a common bug.


```
var builder = WebApplication.CreateBuilder(args);

// 1. Register services BEFORE Build()
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Configure middleware AFTER Build()
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 3. Map controller routes AFTER Build()
app.MapControllers();

// 4. Start the application
app.Run();
```


Easy interview definition

Program.cs is the entry point of an ASP.NET Core application. WebApplication.CreateBuilder(args) creates the application builder, loads configuration, sets up logging, dependency injection, and web host defaults. The lifecycle is: register services on builder.Services, call Build() to create the app, configure middleware and endpoints on app, then call Run() to start listening for HTTP requests.

Configuration loading

WebApplication.CreateBuilder(args) automatically loads configuration from places like:

appsettings.json
appsettings.Development.json
environment variables
command-line arguments

ASP.NET Core loads configuration automatically when CreateBuilder(args) runs. It reads appsettings.json, then environment-specific settings like appsettings.Development.json, then environment variables, then command-line arguments. Later sources override earlier ones.

Before Build(), I configure the dependency injection container. After Build(), I configure how HTTP requests flow through the application. Then Run() starts the web server.