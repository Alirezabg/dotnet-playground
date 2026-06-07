# O — Open/Closed Principle

**In your own words:**
> _Your answer here..._

## Task
You have a `PriceCalculator` with a `switch` on product type.
Add a new product type without modifying the `PriceCalculator` class.

## Interview Tip
> OCP is about extension without modification.
> Strategy pattern, polymorphism, and interfaces are the common tools.


In your own words:

A class should be open for extension but closed for modification.
That means I should be able to add new behaviour without editing existing working code.

Interview answer

You can say:

The original PriceCalculator violates the Open/Closed Principle because it uses a switch on product type. Every new product type requires modifying the calculator. I would refactor this by using polymorphism or the strategy pattern, so each product type owns its own price calculation. Then PriceCalculator depends on an abstraction and does not need to change when new product types are added.

Important distinction

OCP does not mean:

Never modify code.

It means:

Avoid modifying stable, tested code every time a new variation is added.