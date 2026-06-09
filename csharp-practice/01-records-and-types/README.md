# Topic 1 — Records & Types

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (When do you choose a `record` over a `class` or a `struct`? What does value-based equality mean?)_

## Your Task
Model a `Money` value object for the Product Catalogue.
- Use a `record` (or `readonly record struct`) with `Amount` and `Currency`
- Make it immutable — no setters after construction
- Demonstrate non-destructive mutation with a `with` expression (e.g. apply a discount)
- Show that two `Money` instances with the same values are equal

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What does the compiler generate for you when you declare a `record`?
- Why is value-based equality the right default for a value object but the wrong default for an entity like `Product`?
- When would a `record struct` be a better choice than a `record class`, and what's the trade-off?

## Interview Tip
> Interviewers love the `record` vs `class` distinction. Tie it to DDD: **value objects → records, entities → classes with identity**.


A record is usually chosen when the object represents a value, not an identity.

Use a record when:

new Money(10, "GBP") == new Money(10, "GBP")


should be true because both objects have the same data.

That is called value-based equality.

I choose a record for value objects where equality is based on the values inside the object. I choose a class for entities where identity and lifecycle matter. A struct or record struct can be useful for small immutable value types, but I need to be careful because structs are copied by value.


A struct is a c# type used to model a small value.
In C#, int, decimal, bool, DateTime, and Guid are all structs.
Structs are value types
Structs always have a default value
You can give a struct a constructor:
A readonly struct tells C#:

This value should not be modified after creation.
Normal structs do not automatically get ==.

That is one reason record struct is convenient.
A struct cannot inherit from another struct or class.

A struct normally cannot be null.

The ? means:

Nullable<T>

Use a struct when the type is:

small
simple
value-like
usually immutable
not inherited from
not null by default

Avoid structs when the type is:

large
mutable
has identity
needs inheritance
represents an entity
has complex lifecycle
must never have an invalid default value

The record struct gives you extra generated features:

value equality
== and !=
ToString()
Deconstruct()
with expression


Stack = short-lived method/local data
Heap  = objects that can live longer and are managed by the garbage collector

Classes/record classes are reference types.
Structs/record structs are value types.


“structs always go on the stack” is not always true.
Where a value is stored depends on where it is used.

stack memory is:

fast
short-lived
automatically cleaned when method ends
used for local variables and method call information

The heap stores objects that may need to live longer than one method call.


Stack
------------------------
product reference -----> Heap
                         ---------------------
                         Product object
                         Name = "Keyboard"

