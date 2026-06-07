# Topic 7 — Collections & Iteration

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (When do you pick `List<T>` vs `Dictionary<K,V>` vs `HashSet<T>` vs `IEnumerable<T>`?)_

## Your Task
Build a small in-memory catalogue store.
- Hold products in a `Dictionary<Guid, Product>` for O(1) lookup by id
- Expose `GetAll()` returning `IEnumerable<Product>` (not the backing collection)
- Write a custom iterator with `yield return` that streams only in-stock products
- Show why returning `IEnumerable` is safer than returning the internal `List`

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What is the Big-O of lookup, insert, and contains for `List`, `Dictionary`, and `HashSet`?
- What does `yield return` actually compile into, and when is lazy streaming a win?
- Why is exposing `IEnumerable<T>` instead of `List<T>` good encapsulation?

## Interview Tip
> Returning your internal `List<T>` lets callers mutate your aggregate's state. Expose a read-only view (`IReadOnlyList<T>` / `IEnumerable<T>`) instead.
