# Topic 4 — Abstraction

**Interview definition (fill this in yourself before asking Copilot):**
> _Your answer here..._

## Your Task
Introduce an `abstract class` for pricing calculation.
`StandardProduct` always returns `Price`.
`SaleProduct` returns `Price * (1 - DiscountRate)`.

## Questions Your Instructor Will Ask
- What is the difference between `abstract` and `interface` in C#?
- Can an abstract class have a constructor? If so, when is it called?
- What does abstraction hide — implementation or state?


1. Difference between abstract class and interface

An abstract class is a base class for related classes. It can contain shared code, fields, constructors, properties, and abstract methods.

An interface is a contract. It says what a class must be able to do, but it is usually not used to store shared state.


2. Can an abstract class have a constructor?

Yes. An abstract class can have a constructor.

But you cannot create the abstract class directly:

Animal animal = new Animal(); // not allowed

The constructor is called when a child class object is created.


3. What does abstraction hide — implementation or state?

Abstraction mainly hides implementation details.

That means it hides how something works internally and shows only what the user needs to use.

Example:

public abstract class Payment
{
    public abstract void Pay(decimal amount);
}