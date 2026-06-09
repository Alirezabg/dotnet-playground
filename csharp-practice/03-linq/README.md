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


LINQ is a C# feature for querying collections using a readable, SQL-like style. With IEnumerable<T>, LINQ runs in memory against objects. With IQueryable<T>, LINQ builds an expression tree that can be translated by a provider, such as EF Core, into SQL.

Deferred execution means a LINQ query is not executed when you define it. It runs only when you enumerate it, for example with foreach, .ToList(), .Count(), .First(), or .Any().

When is a LINQ query executed?

This does not execute immediately:


IEnumerable<T>

Used for in-memory collections:


IQueryable<T>

Used when the query can be translated by a provider:


LINQ queries usually use deferred execution. Defining the query does not run it. It runs when we enumerate it, for example with foreach, ToList, Count, or First.

With IEnumerable, LINQ works against in-memory objects. With IQueryable, LINQ builds an expression tree that a provider like EF Core can translate into SQL. That means the same-looking LINQ query may either run in memory or run in the database.

A common performance issue is calling ToList() too early or multiple times. With EF Core, that can cause unnecessary database queries or load more data than needed.