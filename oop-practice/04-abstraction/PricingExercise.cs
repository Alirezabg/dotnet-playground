namespace OopPractice.Abstraction;

// TASK: Abstraction — Pricing Calculation
//
// You are building a pricing system for a Product Catalogue.
// Different products are priced differently:
//   - A standard product always returns its base price.
//   - A sale product returns the base price minus a discount.
//
// Your job:
// 1. Create an abstract class called `ProductBase` with:
//    - A public `Name` property (read-only, set in constructor)
//    - A public `Price` property (read-only, set in constructor)
//    - An abstract method `CalculateFinalPrice()` that returns a decimal
//
// 2. Create a concrete class `StandardProduct` that extends `ProductBase`:
//    - `CalculateFinalPrice()` returns Price as-is
//
// 3. Create a concrete class `SaleProduct` that extends `ProductBase`:
//    - Has a `DiscountRate` (e.g. 0.2 means 20% off)
//    - `CalculateFinalPrice()` returns Price * (1 - DiscountRate)
//
// CONSTRAINTS:
//   - Do NOT put any pricing logic in ProductBase itself
//   - DiscountRate must be between 0 and 1 (validate in constructor)
//   - Names must not be null or empty (validate in constructor)
//
// HINT: Think about what the abstract class guarantees vs what each subclass decides.

// Write your code below this line:



