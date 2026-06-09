# Topic 13 — Unit Testing with xUnit

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What makes a good unit test? Where does it sit in the V Model vs integration tests?)_

## Your Task
Write unit tests for the `Product` aggregate's domain logic.
- Test the constructor rejects an empty name (assert it throws your domain exception)
- Test `ChangePrice` updates the price and raises the price-changed event (from Topic 6)
- Use `[Fact]` for a single case and `[Theory]` + `[InlineData]` for several price inputs
- Follow Arrange–Act–Assert and keep each test isolated (no shared state, no database)

Write the test outline below in a new `.cs` file (xUnit). *(Your instructor only sketches test stubs — you fill in the assertions.)*

## Questions Your Instructor Will Ask
- What's the difference between `[Fact]` and `[Theory]`, and when do you reach for each?
- Where does a unit test sit in the V Model, and what does it deliberately *not* cover?
- What does "isolated" mean — and why is a test that touches a database or clock not a unit test?

## Interview Tip
> 📌 Elekta cares about the V Model. Unit tests prove **domain logic** in isolation; component tests prove a service; integration tests prove the wired pipeline. Name the level for every test you write.


A good unit test checks one small piece of behaviour in isolation. It should be fast, repeatable, independent, and easy to understand. In the V Model, unit tests sit at the lowest level: they verify individual domain logic, such as Product.ChangePrice, without testing the database, API, UI, message queues, or the full system.

A good interview sentence:

A unit test proves that one small unit of code behaves correctly in isolation. It should not depend on external systems like a database, file system, clock, network, or message broker. In the V Model, unit tests sit at the bottom and verify low-level domain logic before higher-level component, integration, and system tests.


[Fact] vs [Theory]

Use [Fact] when there is one specific scenario.

[Fact]
public void Constructor_WithEmptyName_ThrowsInvalidProductNameException()
{
}

Use [Theory] when the same test should run with several inputs.

[Theory]
[InlineData(-1)]
[InlineData(-10)]
[InlineData(-0.01)]
public void Constructor_WithNegativePrice_ThrowsException(decimal price)
{
}

Important idea: Arrange–Act–Assert

Every test should follow this shape:

// Arrange
// Create the object and input data.

// Act
// Call the method being tested.

// Assert
// Check the result.

Example:

[Fact]
public void ChangePrice_WithDifferentPrice_UpdatesPrice()
{
    // Arrange
    var product = new Product("Keyboard", new Money(50m, "GBP"));

    // Act
    product.ChangePrice(new Money(60m, "GBP"));

    // Assert
    Assert.Equal(new Money(60m, "GBP"), product.Price);
}

What “isolated” means

A unit test should not touch:

Database
File system
Network
Message queue
Real clock
Real API

Easy interview answers

What is a unit test?

A unit test checks one small piece of code in isolation. It should be fast, repeatable, and independent of external systems.

What is [Fact]?

[Fact] is for one fixed test case with no parameters.

What is [Theory]?

[Theory] is for the same test logic with different input values.

Where does a unit test sit in the V Model?

Unit tests sit at the lowest level of the V Model. They verify individual pieces of code, usually domain logic, before component, integration, and system tests.

Why is a database test not a unit test?

Because it depends on an external system. That makes it slower, more fragile, and closer to an integration test.

What should we unit test in Product?

We should test the domain rules: invalid names are rejected, invalid prices are rejected, changing price updates state, and changing price raises the correct event.


The V Model is a way to describe how software development and testing line up.

Think of it like this:

Requirements        ←→        Acceptance / System Tests
   Design           ←→        Integration Tests
      Architecture  ←→        Component Tests
         Code       ←→        Unit Tests

It is called the V Model because it is often drawn like a letter V:

        Requirements              System Tests
              \                  /
               \                /
             Design          Integration Tests
                 \          /
                  \        /
                   Code
                  /    \
             Unit Tests

The left side is where you define/build the system.

The right side is where you test the system.

The bottom is the actual code.

For your interview, remember this:

In the V Model, every development stage has a matching test stage.

So:

Code              → Unit tests
Components        → Component tests
Integrated parts  → Integration tests
Full system       → System tests
User requirements → Acceptance tests

For your current topic:

Unit tests sit at the bottom of the V Model. They test small pieces of code, like Product.ChangePrice, in isolation.

So when we say:

A unit test sits low in the V Model

we mean:

It tests the smallest level of the software: individual methods/classes, not the whole application.