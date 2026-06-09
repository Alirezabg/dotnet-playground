using Catalogue.Api.Application;
using Catalogue.Api.Infrastructure;
using Catalogue.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// --- Service registration (everything BEFORE Build) ---
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Request pipeline (everything AFTER Build) ---
// Exception handling goes first so it can catch failures from everything downstream.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok(new { service = "Catalogue.Api", version = "1.0" }));
app.MapControllers();

app.Run();

// Exposed so the integration tests can use WebApplicationFactory<Program>.
public partial class Program { }
