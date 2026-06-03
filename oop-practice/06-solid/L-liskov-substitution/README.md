# L — Liskov Substitution Principle

**In your own words:**
> _Your answer here..._

## Task
A `ReadOnlyProduct` extends `Product` but throws `NotSupportedException` on `SetPrice()`.
Show why this violates LSP and fix it.

## Interview Tip
> LSP violations often show up as `NotImplementedException` or surprising `throw` in overrides.
> Know: subtypes must honour the contract of the base type.
