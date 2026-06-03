# Topic 11: Async / Await Patterns

## Elekta Relevance
Elekta's systems are event-driven and use message queues (RabbitMQ). Every I/O call must be async, and you must understand failure paths across async boundaries.

## Concepts to Master
- `Task<T>` and `async/await` fundamentals
- `CancellationToken` — cooperative cancellation
- `Task.WhenAll` and `Task.WhenAny`
- `IAsyncEnumerable<T>` — async streaming
- `async void` — the anti-pattern and when it appears
- Exception propagation across await points
- Fire-and-forget with error handling
- Back-pressure (bounded queues, `SemaphoreSlim`)

## Your Tasks (implement in this folder)

### Task A — Async All The Way Down
Implement a `ProductService` with a `GetProductAsync(Guid id, CancellationToken ct)` method.
- Must propagate the `CancellationToken` to every awaited call
- Must not use `.Result` or `.Wait()`

Before coding, answer:
- What is a synchronisation context and why does it matter in ASP.NET Core?
- What would happen if you called `.Result` inside an async controller action?

### Task B — Task Composition
Implement a method that fetches products from two sources concurrently and merges the results:
- Use `Task.WhenAll`
- Handle the case where one source throws

Before coding, answer:
- If `Task.WhenAll` throws, how do you access all the exceptions?
- What is the difference in behaviour between `WhenAll` and `WhenAny`?

### Task C — CancellationToken Propagation
Write a method that simulates a long-running operation and checks for cancellation:
- Use `ct.ThrowIfCancellationRequested()` at a logical checkpoint
- Wrap the call in a try/catch for `OperationCanceledException`

Before coding, answer:
- What is the difference between `OperationCanceledException` and a regular exception?
- How does the caller signal cancellation — and when?

### Task D — Async Stream (stretch)
Implement a method that yields products one-by-one from a source using `IAsyncEnumerable<Product>`:
- Use `await foreach` at the call site
- Accept a `CancellationToken` on the enumerable

Before coding, answer:
- When is streaming better than loading everything into a `List<T>`?
- How does back-pressure work with `IAsyncEnumerable`?

## Interview Questions Elekta May Ask
- "What is `async void` and why should you avoid it?"
- "How do you handle exceptions from `Task.WhenAll`?"
- "How do you prevent one slow consumer from blocking the thread pool?"
- "What is the Outbox Pattern and how does async fit into it?"
