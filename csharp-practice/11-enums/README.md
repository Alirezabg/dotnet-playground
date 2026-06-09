# Topic 11 — Enums

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What is an `enum` backed by? When is an enum better than a set of constants or a class?)_

## Your Task
Model status and permissions in the Product Catalogue with enums.
- Define a `ProductStatus` enum (`Draft`, `Active`, `Discontinued`) and use it on `Product`
- Define a `[Flags]` enum `SalesChannel` (`None`, `Web`, `Store`, `Wholesale`) and combine values with bitwise OR
- Parse a string to the enum safely with `Enum.TryParse` (reject unknown values)
- Show why you set explicit underlying values for `[Flags]` (1, 2, 4, 8)

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What is an enum's default underlying type, and why might you change it?
- What does `[Flags]` change, and why must the values be powers of two?
- Why is a raw `enum` cast (`(ProductStatus)99`) dangerous, and how do you guard against invalid values?

## Interview Tip
> Enums are not validated on cast — `(ProductStatus)99` compiles and runs. In safety-critical code, validate with `Enum.IsDefined` (or a switch) at the boundary.


An enum is a value type that represents a fixed set of named values. By default, an enum is backed by an int, but you can choose another integral type like byte, short, or long.

Use an enum when a value can only be one of a known set of options, such as product status. It is better than string constants because it gives compile-time safety, avoids spelling mistakes, and makes the code easier to read.