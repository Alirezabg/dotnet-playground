# Topic 2 — Pattern Matching

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What kinds of patterns does C# support: type, property, relational, logical, list?)_

## Your Task
Write a method that maps a `Product` to a shipping band using a **switch expression**.
- Match on price ranges (relational patterns: `< 10`, `>= 10 and < 100`, `>= 100`)
- Add a property pattern (e.g. a `Category` of `"Hazardous"` always ships as `Restricted`)
- Handle the `null` product case explicitly
- Return an enum or string describing the band

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What's the difference between a `switch` statement and a `switch` expression?
- How does the compiler help you with exhaustiveness, and what does the `_` discard pattern do?
- When does pattern matching make code clearer than a chain of `if`/`else`, and when does it hurt readability?

## Interview Tip
> Show you can combine patterns: `{ Price: < 10, Category.Name: "Books" }`. Property + relational patterns together is a strong signal.
