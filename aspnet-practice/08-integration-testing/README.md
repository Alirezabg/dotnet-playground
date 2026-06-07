# Topic 8 — Integration Testing with `WebApplicationFactory<T>`

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What does an integration test prove that a unit test cannot? Where does it sit in the V Model?)_

## Your Task
Write an integration test for the products endpoints.
- Spin up the API in-memory with `WebApplicationFactory<Program>`
- Use the test `HttpClient` to `POST /products` then `GET /products/{id}`
- Assert the status codes and the round-tripped response body
- Swap the real repository for an in-memory test double via service override

Write the test outline below in a new `.cs` file (xUnit). *(Your instructor only sketches test stubs — you fill in the assertions.)*

## Questions Your Instructor Will Ask
- What exactly does `WebApplicationFactory<T>` host, and why is it faster than hitting a real server?
- Where does this test sit in the V Model — and what would the matching unit and component tests cover?
- How do you override DI registrations (e.g. swap the database) for a test run?

## Interview Tip
> V Model framing wins points: unit (domain logic) → component (a service in isolation) → integration (endpoint through the real pipeline) → system. Name the level for every test.
