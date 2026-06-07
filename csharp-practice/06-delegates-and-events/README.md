# Topic 6 — Delegates, `Func`/`Action` & Events

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is a delegate? How is an `event` different from a plain delegate field?)_

## Your Task
Add a price-change notification to the `Product` aggregate.
- Declare an `event` that fires when the price changes (a `PriceChangedEvent` payload)
- Raise it from a `ChangePrice` method, but only when the value actually changes
- Subscribe a handler that logs the old → new price
- Show the same idea expressed with a `Func<>`/`Action<>` instead of a named delegate

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- Why does `event` exist when a public delegate field would compile? What does it protect?
- What's the difference between `Func<>`, `Action<>`, and `Predicate<>`?
- How can event subscriptions cause memory leaks, and how do you unsubscribe safely?

## Interview Tip
> This is the bridge to **domain events** and messaging (RabbitMQ). Mention that in-process events and out-of-process messages solve the same decoupling problem at different scales.
