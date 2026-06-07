# Topic 3 — LINQ

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is deferred execution? What's the difference between `IEnumerable` and `IQueryable`?)_

## Your Task
Given an in-memory `List<Product>`, write LINQ queries that:
- Filter products above a price threshold
- Project each into a lightweight summary (`ProductName` + formatted price)
- Group products by `Category` and count each group
- Find the most expensive product per category

Write both **method syntax** and **query syntax** versions of at least one query.

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- When is a LINQ query actually executed? What triggers it?
- Why can calling `.ToList()` twice run the query twice, and when does that matter for performance?
- What changes when the source is `IQueryable` (e.g. EF Core) instead of `IEnumerable`?

## Interview Tip
> Deferred execution is a classic gotcha. Be ready to explain why iterating a query inside a loop can cause repeated database hits.
