# Topic 8 — Exceptions & Error Handling

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (When should you throw, and when should you return a result type instead?)_

## Your Task
Design error handling for creating a `Product`.
- Throw a domain-specific exception (e.g. `InvalidProductNameException`) on bad input
- Decide which failures are *exceptional* vs *expected* (e.g. "not found")
- Sketch an alternative `Result<T>` / try-pattern approach for expected failures
- Show correct `try`/`catch`/`finally` usage and why you avoid catching `Exception` broadly

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the cost of throwing exceptions, and why shouldn't they drive normal control flow?
- When is a custom exception type worth it over a built-in one?
- In a safety-critical system, what happens to in-flight state if an exception unwinds mid-operation?

## Interview Tip
> Strong answer: *"Exceptions for the exceptional; result types for expected outcomes like validation or not-found."* Then trace the failure path.
