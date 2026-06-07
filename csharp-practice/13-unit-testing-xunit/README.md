# Topic 13 — Unit Testing with xUnit

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What makes a good unit test? Where does it sit in the V Model vs integration tests?)_

## Your Task
Write unit tests for the `Product` aggregate's domain logic.
- Test the constructor rejects an empty name (assert it throws your domain exception)
- Test `ChangePrice` updates the price and raises the price-changed event (from Topic 6)
- Use `[Fact]` for a single case and `[Theory]` + `[InlineData]` for several price inputs
- Follow Arrange–Act–Assert and keep each test isolated (no shared state, no database)

Write the test outline below in a new `.cs` file (xUnit). *(Your instructor only sketches test stubs — you fill in the assertions.)*

## Questions Your Instructor Will Ask
- What's the difference between `[Fact]` and `[Theory]`, and when do you reach for each?
- Where does a unit test sit in the V Model, and what does it deliberately *not* cover?
- What does "isolated" mean — and why is a test that touches a database or clock not a unit test?

## Interview Tip
> 📌 Elekta cares about the V Model. Unit tests prove **domain logic** in isolation; component tests prove a service; integration tests prove the wired pipeline. Name the level for every test you write.
