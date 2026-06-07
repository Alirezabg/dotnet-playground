# Topic 2 — Inheritance

**Interview definition (fill this in yourself before asking Copilot):**
> _Your answer here..._

## Your Task
Model a base class `CatalogueItem` with shared properties, then create derived types:
- `PhysicalProduct` — has a `Weight` in kg
- `DigitalProduct` — has a `DownloadUrl`

## Questions Your Instructor Will Ask
- When should you use inheritance vs composition?
Use inheritance when there is a real is-a relationship.
Use composition when there is a has-a relationship.

Dog is an Animal              → inheritance
Car has an Engine             → composition
Order has a Customer          → composition
Manager is an Employee        → inheritance
Product has a DiscountPolicy  → composition

I use inheritance when one class is a specialized version of another class and there is a clear is-a relationship. I use composition when a class should be built from other objects or behaviours. Composition is often safer because it avoids tightly coupling classes through an inheritance hierarchy.


- What does `virtual` / `override` enable that a plain method doesn't?
virtual and override allow a child class to replace or extend behaviour from the parent class.
virtual marks a base class method as replaceable, and override lets a derived class provide its own version. This enables polymorphism, where the method that runs is based on the actual object type at runtime, not just the variable type.


- What happens if a derived class doesn't call `base`?

A derived class constructor always calls a base class constructor.

If you do not write it explicitly, C# tries to call the base class’s parameterless constructor automatically.
But if the base class does not have a parameterless constructor, then the derived class must call base(...) manually.
Example:

public class CatalogueItem
{
    public CatalogueItem(string name, string description)
    {
    }
}

This will fail:

public class PhysicalProduct : CatalogueItem
{
    public PhysicalProduct(decimal weightKg)
    {
    }
}

Because C# tries to do:

public PhysicalProduct(decimal weightKg) : base()
{
}
But base() does not exist.

Correct:

public class PhysicalProduct : CatalogueItem
{
    public PhysicalProduct(string name, string description, decimal weightKg)
        : base(name, description)
    {
    }
}

A derived constructor always calls a base constructor. If I do not specify one, C# tries to call the parameterless base constructor automatically. If the base class only has constructors with parameters, I must call base(...) explicitly or the code will not compile.

Do not use inheritance just to reuse code. Prefer building classes from smaller objects unless there is a true is-a relationship.
Composition means delegating work

The difference

Composition answers:

What is this class made of / what does it use?

Injection answers:

How does this class receive what it uses?

So:

Composition = Car has an Engine
Injection   = Engine is given to Car from outside
Composition means a class is built using other objects. For example, a Car has an Engine.

Injection means those objects are provided from outside, usually through the constructor. For example, instead of Car creating its own Engine, we pass an Engine into the Car.

## Interview Tip
> "Favour composition over inheritance" — be ready to explain what this means
> and give a concrete example of when inheritance causes problems.
