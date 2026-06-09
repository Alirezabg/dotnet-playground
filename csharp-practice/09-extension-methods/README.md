# Topic 9 — Extension Methods

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is an extension method really — and what are its limits?)_

## Your Task
Write extension methods that read fluently on catalogue types.
- An `IEnumerable<Product>` extension `InStock()` that filters available products
- A `Money` extension `WithVat(decimal rate)` that returns a new discounted/taxed value
- Show how extension methods enable a fluent chain: `products.InStock().CheaperThan(...)`
- Explain why these don't (and can't) access private members

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- How does the compiler resolve an extension method — what's really happening under the hood?
- Why can't an extension method access private state of the type it extends?
- When does an extension method become a code smell instead of a convenience?

## Interview Tip
> LINQ itself is just extension methods on `IEnumerable<T>`. Saying that out loud shows you understand the mechanism, not just the syntax.


Strong interview answer:

In safety-critical code, I avoid leaving objects half-mutated. I validate first, then apply state changes. If multiple steps must succeed together, I use transactions, compensation, or an explicit state machine. I also make sure exceptions are logged and handled at system boundaries, not swallowed silently.

Simple rule to remember

Use this in interview:

Throw exceptions when an invariant is broken or the system cannot safely continue. Return Result<T>, bool Try..., or nullable values for expected outcomes like validation errors, not-found, or user mistakes. Never use exceptions as normal control flow.

An exception is an event or error that interrupts the normal flow of the program.

 // Catch a specific exception that you know how to handle.

 Exceptional: throw

Throw when the system is in a situation where the current operation cannot safely continue.

Why avoid catching Exception broadly?
Because it catches everything, including errors you may not know how to recover from.

When is a custom exception worth it?

Use a custom exception when the failure has domain meaning and callers may want to catch it separately.

Safety-critical interview answer

In a safety-critical system, exception handling is not just about displaying an error message. You must think about state.

If an exception happens halfway through an operation, you may have partially changed state.

An error is the problem itself. An exception is the object/mechanism used by the program to report and handle that problem.

Term	Meaning
Error	Something went wrong
Exception	A C# object thrown to signal that something went wrong
An error is what went wrong; an exception is how the program tells you something went wrong.

 Errors are severe and cannot be handled effectively in code.
  Exceptions are manageable problems that your code can handle.