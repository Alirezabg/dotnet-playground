# Topic 12 — Boxing, Unboxing, Casting & Conversions

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is boxing? What's the difference between `is`, `as`, and an explicit cast?)_

## Your Task
Explore value/reference conversions using catalogue types.
- Box a `Money` value (or an `int` quantity) into `object`, then unbox it — and observe the allocation
- Demonstrate a failing **explicit cast** (`InvalidCastException`) vs a safe `as` returning `null`
- Use the `is` pattern (`if (item is Money m)`) to test-and-cast in one step
- Contrast an **implicit** conversion (no data loss) with an **explicit** one (possible loss), e.g. `decimal` → `int`
- Show how generics (`List<Money>`) avoid the boxing that `ArrayList`/`object` would cause

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What actually happens in memory when a value type is boxed? Why is it a performance concern on a hot path?
- When would `as` return `null` instead of throwing, and why prefer the `is` pattern today?
- What's the difference between an implicit and an explicit conversion operator, and when would you define your own?

## Interview Tip
> "Generics exist partly to kill boxing." Tie this back to Topic 4 — `List<int>` stores values inline; the old `ArrayList` boxed every element.
