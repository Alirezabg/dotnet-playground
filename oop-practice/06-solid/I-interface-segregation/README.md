# I — Interface Segregation Principle

**In your own words:**
> _Your answer here..._

## Task
You have an `IProductRepository` with `GetAll`, `Add`, `Update`, `Delete`, and `Search`.
Not all consumers need all methods. Split it into focused interfaces.

## Interview Tip
> "Fat interfaces" force implementors to implement methods they don't need.
> Role interfaces (small, focused) are the solution.


Interface Segregation Principle means:

    Don’t force a class to depend on methods it does not need.
    Instead of one large interface, create smaller interfaces for specific roles.

    ISP means clients should not be forced to depend on methods they do not use. So I would split a large repository interface into smaller role interfaces based on what each consumer actually needs.

    SRP is about classes having one reason to change.

ISP is about interfaces being small enough that consumers only depend on what they use.