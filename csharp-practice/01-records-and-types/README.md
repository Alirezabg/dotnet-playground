# Topic 1 — Records & Types

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (When do you choose a `record` over a `class` or a `struct`? What does value-based equality mean?)_

## Your Task
Model a `Money` value object for the Product Catalogue.
- Use a `record` (or `readonly record struct`) with `Amount` and `Currency`
- Make it immutable — no setters after construction
- Demonstrate non-destructive mutation with a `with` expression (e.g. apply a discount)
- Show that two `Money` instances with the same values are equal

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What does the compiler generate for you when you declare a `record`?
- Why is value-based equality the right default for a value object but the wrong default for an entity like `Product`?
- When would a `record struct` be a better choice than a `record class`, and what's the trade-off?

## Interview Tip
> Interviewers love the `record` vs `class` distinction. Tie it to DDD: **value objects → records, entities → classes with identity**.
