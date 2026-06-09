# Topic 10 — Equality, Comparison & Operators

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What's the difference between reference equality, value equality, and `IEquatable<T>`?)_

## Your Task
Make catalogue types compare correctly.
- Give `Product` **identity-based** equality (two products are equal iff their `Id` matches)
- Give `Money` **value-based** equality (compare `Amount` + `Currency`)
- Implement `IComparable<Product>` so a list of products sorts by price
- Override `GetHashCode()` consistently with `Equals()` and explain why that pairing matters

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- Why must `Equals` and `GetHashCode` always agree? What breaks in a `Dictionary`/`HashSet` if they don't?
- When you use a `record`, what equality do you get for free — and is it right for an entity?
- What's the difference between `==`, `Equals`, and `ReferenceEquals`?

## Interview Tip
> Tie it back to DDD: **entities compare by identity, value objects compare by value**. Records default to value equality, which is wrong for entities.


Equality means deciding whether two objects should be treated as the same.

There are three common types:

Reference equality means two variables point to the exact same object in memory.

Value equality means two objects are equal because their data is the same.

Identity equality means two entities are equal because they have the same unique identity, usually an Id.

Why Equals and GetHashCode must agree

This rule is very important:

If two objects are equal, they must return the same hash code.

For Product, equality uses Id.

So GetHashCode() must also use Id.