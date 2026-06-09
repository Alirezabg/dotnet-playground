# Topic 5 — Nullable Reference Types & Null Safety

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (What do nullable reference types actually change — runtime or compile time?)_

## Your Task
Harden a `Product` model against null bugs.
- Mark which reference properties are non-nullable (`string Name`) vs nullable (`string? Description`)
- Guard the constructor so a `Product` can never be created with a null name
- Show the difference between the null-forgiving operator `!` and a real null check
- Demonstrate `??`, `?.`, and `??=` in realistic spots

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- Are nullable reference types enforced at runtime, or are they compile-time warnings only?
- What does the `!` null-forgiving operator do, and why is it dangerous in safety-critical code?
- How do you validate input at a system boundary so the rest of your domain can trust non-null?

## Interview Tip
> In safety-critical software, null guards belong at the **boundary** (constructors, API input). Inside the domain, types should already guarantee non-null.


Nullable Reference Types do not change runtime behaviour by themselves. They are a compile-time safety feature.

They help the compiler warn you when a reference value might be null.

For example:

string name = null;        // Warning
string? description = null; // OK

Null-forgiving operator !

The ! operator tells the compiler:

“Trust me, this value is not null.”


string? possibleName = GetNameFromApi();

string name = possibleName!;
Console.WriteLine(name.Length);

This removes the compiler warning, but it does not check anything at runtime.

Description ?? Name

means:

Use Description if it is not null. Otherwise use Name.

Description?.Length

means:

Get Length only if Description is not null.

Description ??= "No description provided.";

means:

If Description is null, assign this default value.