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
