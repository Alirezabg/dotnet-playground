# Topic 1 — Encapsulation

**Interview definition (fill this in yourself before asking Copilot):**
> _Your answer here..._

## Your Task
Design a `Product` class that:
- Has `Id`, `Name`, `Price`, and `Stock` fields
- Prevents `Price` from being set to a negative value
- Prevents `Stock` from going below zero
- Does not expose its internal state in a way that allows invalid data

Write your class below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- Why did you choose `private set` vs a backing field?
- Where should validation live — in the property setter or in a method?
- What does "hiding implementation details" actually protect you from?

## Interview Tip
> Encapsulation is often tested through "what's wrong with this code" questions —
> look for public fields, missing validation, or classes that expose too much.

1. I used private ser when i want other classes to read the value not change it directly. It keeps the code simple while still pritecting the object's state

using bacjing field when you need custom logic.

```
private int stock;

public int Stock
{
    get { return stock; }
    set
    {
        if (value < 0)
            throw new ArgumentException("Stock cannot be negative.");

        stock = value;
    }
}
```

2. Simple value validation can live in the property setter. But if the change represents a business action, I would use a method because methods explain the intention more clearly.

If I am just protecting a signle value, I can validate in the setter. If the change has business meaning , like selling a product or applying a discount, I prefer a method.

3. It protects the class from outside code directly changing or depending on its internal data. This means the class controls its own rules , and I can change the internal implementaion later without breaking the rest of the program. 
hiding implementation details means outside code should know what your class can do but not exactly how it does it internally


| Modifier             | Meaning                                                                                      |
| -------------------- | -------------------------------------------------------------------------------------------- |
| `private`            | Only accessible inside the same class                                                        |
| `public`             | Accessible from anywhere                                                                     |
| `protected`          | Accessible inside the same class and child classes                                           |
| `internal`           | Accessible only inside the same project/assembly                                             |
| `protected internal` | Accessible inside the same project/assembly **or** from child classes                        |
| `private protected`  | Accessible inside the same class or child classes, but only within the same project/assembly |



A class is a blueprint for creating objects.

It defines:

Data an object can have
Behaviour an object can do
So the simple definition is:

A class is a template or blueprint that defines the properties and methods that its objects will have.

Interview version:

A class is a user-defined type that encapsulates data and behaviour into a single unit.