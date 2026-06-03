// Product Catalogue — Minimal API Entry Point
// .NET 8 | C# 12
//
// Your task: Build this out step by step during practice sessions.
// Copilot will guide you — it will NOT write the implementations for you.
//
// Before you start, think about:
//   - What services will this application need?
//   - How should you organise your endpoints?
//   - What does the request pipeline look like?

var builder = WebApplication.CreateBuilder(args);

// TODO: Register your services here
// Hint: Think about which services belong to which layer.
//       What's the difference between AddScoped, AddSingleton, and AddTransient?

var app = builder.Build();

// TODO: Map your endpoint groups here
// Hint: Look into app.MapGroup() — why would you use that over individual app.MapGet() calls?

app.Run();
