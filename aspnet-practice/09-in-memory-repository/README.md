# Topic 9 — In-Memory Repository for Testing

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (Why use a test double for the data layer? What's the difference between a fake, a stub, and a mock?)_

## Your Task
Build an in-memory `IProductRepository` for fast tests.
- Back it with a `Dictionary<Guid, Product>`
- Implement `Add`, `GetById`, `GetAll`, `Update`, `Delete`
- Make it interchangeable with the real implementation behind the interface
- Show how a test registers it in place of the production repository

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the difference between a *fake*, a *stub*, and a *mock*? Which is this?
- Why does coding against `IProductRepository` (not a concrete class) make this swap possible?
- What are the limits of an in-memory repository — what bugs will it *never* catch that a real database would?

## Interview Tip
> This is the **Dependency Inversion Principle** paying off: depend on the abstraction and you can swap a real DB for an in-memory fake at test time.
