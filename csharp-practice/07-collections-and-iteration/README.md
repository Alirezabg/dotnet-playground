# Topic 7 — Collections & Iteration

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (When do you pick `List<T>` vs `Dictionary<K,V>` vs `HashSet<T>` vs `IEnumerable<T>`?)_

## Your Task
Build a small in-memory catalogue store.
- Start with a fixed `Product[]` array of seed data, then explain when you'd switch to a `List<T>`
- Hold products in a `Dictionary<Guid, Product>` for O(1) lookup by id
- Use a `HashSet<string>` to enforce unique category names
- Expose `GetAll()` returning `IEnumerable<Product>` (not the backing collection)
- Write a custom iterator with `yield return` that streams only in-stock products
- Show why returning `IEnumerable` is safer than returning the internal `List`

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- When is a fixed-size `array` the right choice over a `List<T>`?
- What is the Big-O of lookup, insert, and contains for `List`, `Dictionary`, and `HashSet`?
- What does `yield return` actually compile into, and when is lazy streaming a win?
- Why is exposing `IEnumerable<T>` instead of `List<T>` good encapsulation?

## Interview Tip
> Returning your internal `List<T>` lets callers mutate your aggregate's state. Expose a read-only view (`IReadOnlyList<T>` / `IEnumerable<T>`) instead.


List<T> is best when you want an ordered, growable collection and you often loop through items.

Dictionary<TKey, TValue> is best when you need fast lookup by a key, for example ProductId -> Product.

HashSet<T> is best when you only care whether something exists, and duplicates are not allowed.

IEnumerable<T> is best when you want to expose “something you can loop over” without exposing how it is stored internally.

Big-O interview table
Collection	Lookup by index	Lookup by key/value	Insert	Contains


## Big-O Cheat Sheet (Common Collections)

| Collection         | Lookup by index     | Lookup by key/value     | Insert                    | Contains                  |
|------------------|--------------------|--------------------------|---------------------------|---------------------------|
| Array            | O(1)               | O(n)                     | Fixed size                | O(n)                      |
| List<T>          | O(1) by index      | O(n) search              | Usually O(1) at end       | O(n)                      |
| Dictionary<K,V>  | Not index-based    | Usually O(1) by key      | Usually O(1)              | Usually O(1) by key       |
| HashSet<T>       | Not index-based    | Usually O(1)             | Usually O(1)              | Usually O(1)              |