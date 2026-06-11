# Topic 8 — Integration Testing with `WebApplicationFactory<T>`

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What does an integration test prove that a unit test cannot? Where does it sit in the V Model?)_

## Your Task
Write an integration test for the products controller.
- Spin up the API in-memory with `WebApplicationFactory<Program>`
- Use the test `HttpClient` to `POST /products` then `GET /products/{id}`
- Assert the status codes and the round-tripped response body
- Swap the real repository for an in-memory test double via `WithWebHostBuilder` / `ConfigureTestServices`

Write the test outline below in a new `.cs` file (xUnit). *(Your instructor only sketches test stubs — you fill in the assertions.)*

## Questions Your Instructor Will Ask
- What exactly does `WebApplicationFactory<T>` host, and why is it faster than hitting a real server?
- Where does this test sit in the V Model — and what would the matching unit and component tests cover?
- How do you override DI registrations (e.g. swap the database) for a test run?

## Interview Tip
> V Model framing wins points: unit (domain logic) → component (a service in isolation) → integration (endpoint through the real pipeline) → system. Name the level for every test.


An integration test proves that multiple parts of the application work together through the real request pipeline.
Unlike a unit test, it does not just test one method or class. It can test routing, model binding, validation, middleware, dependency injection, controller logic, serialization, and repository interaction together.
In the V Model, it sits above unit/component tests and below full system/end-to-end tests.


That means the request goes through the real API pipeline:

HttpClient
   ↓
Routing
   ↓
Middleware
   ↓
Model binding / validation
   ↓
Controller action
   ↓
Repository
   ↓
JSON response

Why it is faster than a real server

It does not need IIS, Kestrel, a real port, or Postman.

It uses an in-memory test server. Microsoft’s ASP.NET Core integration testing support creates a test web host and a test server client for handling requests and responses in the test process.

So it is closer to the real app than a unit test, but faster than deploying and calling a real hosted API.


Why override DI?

In production, your app might use:

services.AddScoped<IProductRepository, SqlProductRepository>();

But in the test, you do not want a real database.

So the test replaces it:

services.Remove(descriptor);
services.AddSingleton<IProductRepository, InMemoryProductRepository>();

That means the controller still asks for:

IProductRepository

But during the test, ASP.NET gives it the fake/in-memory version.

What does WebApplicationFactory<T> host?

It hosts the real ASP.NET Core application in memory using the app’s entry point, usually Program. It lets the test create an HttpClient and send real HTTP requests through the app pipeline without starting a real external web server.

Where does this sit in the V Model?

This is an integration test.

Unit test       → Product domain rules only
Component test  → Product service or repository in isolation
Integration test → POST /products through real API pipeline
System test     → Whole deployed system with real database/external services

An integration test checks that several parts of your app work together.

An E2E test checks the whole system like a real user would use it.