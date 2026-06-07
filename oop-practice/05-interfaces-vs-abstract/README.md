# Topic 5 — Interfaces vs Abstract Classes

**Interview definition (fill this in yourself before asking Copilot):**
> _Your answer here..._

## Investigation Task
Your instructor will investigate with you: **Which is better for `IProductRepository` — an interface or an abstract class?**

Consider:
- Testability (mocking)
- Future extensibility
- What C# allows with each (`default` interface methods, abstract constructors, etc.)

Write both versions and then decide — your instructor will ask you to justify the choice.

## Interview Tip
> This is one of the most common OOP interview questions.
> Know: multiple interface implementation, default interface methods (.NET 8+),
> and why abstract classes can have state.


4. Important interview comparison
Feature	Interface	Abstract class
Main purpose	Define a contract	Share base behavior/state
Can have method signatures?	Yes	Yes
Can have implemented methods?	Yes, with default interface methods	Yes
Can have fields/state?	No instance fields	Yes
Can have constructors?	No instance constructors	Yes
Multiple inheritance?	A class can implement many interfaces	A class can inherit only one class
Good for mocking/testing?	Very good	Possible, but less flexible
Good for shared code?	Sometimes	Better
Common repository choice	Usually interface	Only if shared base logic is needed

Abstract class can have state

An interface defines a contract: “any class that implements me must provide these members.”

An abstract class is a base class that can contain shared code, shared state, constructors, and abstract members that derived classes must implement.

Use an interface when you want to say:

This class can do this.

Use an abstract class when you want to say:

This class is a kind of this base type and shares common code/state.