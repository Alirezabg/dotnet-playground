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
