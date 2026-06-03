# I — Interface Segregation Principle

**In your own words:**
> _Your answer here..._

## Task
You have an `IProductRepository` with `GetAll`, `Add`, `Update`, `Delete`, and `Search`.
Not all consumers need all methods. Split it into focused interfaces.

## Interview Tip
> "Fat interfaces" force implementors to implement methods they don't need.
> Role interfaces (small, focused) are the solution.
