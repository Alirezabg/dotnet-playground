# Topic 3 — Polymorphism

**Interview definition (fill this in yourself before asking Copilot):**
> _Your answer here..._

## Your Task
Give `CatalogueItem` a virtual method `GetDisplayLabel()`.
Override it in each derived type to return a type-specific label.
Then write code that holds a `List<CatalogueItem>` and calls `GetDisplayLabel()` on each.

## Questions Your Instructor Will Ask
- What is runtime polymorphism vs compile-time polymorphism (overloading)?

Type	Example	When decided?
Runtime polymorphism	virtual / override	Runtime
Compile-time polymorphism	Method overloading	Compile time

Compile-time polymorphism usually means method overloading.

- What keyword is required on the base method? Why?
- What does `sealed override` do?
This class overrides the method, but no further child class can override it again.



Polymorphism means:

One thing can take many forms.

In OOP, it usually means:

You can use a base type reference to work with different derived objects, and each object can behave differently.


Runtime polymorphism

This is important for interviews.

CatalogueItem item = new PhysicalProduct("Laptop", "Gaming laptop", 2.5);

Console.WriteLine(item.GetSummary());

Even though the variable is declared as:

CatalogueItem

C# looks at the real object at runtime:

PhysicalProduct

So it calls:

PhysicalProduct.GetSummary()

This is called runtime polymorphism or dynamic dispatch.

Polymorphism allows us to treat different derived classes through a common base type or interface. The actual method that runs depends on the real object type at runtime. In C#, this is commonly done with virtual and override, or through interfaces.
Polymorphism allows us to treat different derived classes through a common base type or interface. The actual method that runs depends on the real object type at runtime. In C#, this is commonly done with virtual and override, or through interfaces.
Method hiding is not polymorphism

new keyword in methods

Sometimes C# shows this:

public new string Speak()

Example:

public class Child : Parent
{
    public new string Speak()
    {
        return "Child speaking";
    }
}

This means:

I know the parent already has a method with this name. I want to hide it, not override it.

This is usually not what you want for polymorphism.
