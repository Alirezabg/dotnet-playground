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


Async/await is how C# lets us do I/O work without blocking a thread.

For example, when an API calls a database, the thread does not need to sit there waiting. It can return to the thread pool and serve another request. When the database result comes back, the rest of the method continues.

In interviews, the key idea is:

async/await does not make CPU work faster. It makes waiting for I/O scalable.


Task A — Async All The Way Down
A synchronisation context is an object that decides where the continuation after an await should run.

await Task.WhenAll(...) propagates failure. If I need every exception, I keep a reference to the combined task and inspect its Exception.InnerExceptions.
Task.WhenAll waits for all tasks to finish.
Task.WhenAny waits until the first task finishes.

OperationCanceledException represents cooperative cancellation. It usually means the caller no longer wants the result, not that the application failed.

using CancellationTokenSource cts = new();

cts.Cancel();


Important Interview Concepts
Task<T>

A Task<T> represents future work that will eventually produce a result.

Task<Product> GetProductAsync(Guid id, CancellationToken ct)

This means:

This method will eventually return a Product.


Task

A Task represents future work with no return value.

Task SaveProductAsync(Product product, CancellationToken ct)

This means:

This method does async work but does not return a value.


async

The async keyword allows the method to use await.

public async Task<Product> GetProductAsync(Guid id, CancellationToken ct)
{
    Product product = await _repository.GetByIdAsync(id, ct);

    return product;
}

Important:

async does not mean “run on another thread.”

It means the method can pause at an await point and resume later.


await

await means:

Wait asynchronously for this task to complete, without blocking the current thread.

This is good:

Product product = await _repository.GetByIdAsync(id, ct);

This is bad:

Product product = _repository.GetByIdAsync(id, ct).Result;


Outbox Pattern
What problem does the Outbox Pattern solve?

Imagine this code:

await _db.SaveChangesAsync(ct);
await _messageBus.PublishAsync(productCreatedEvent, ct);

Problem:

What if the database save succeeds, but publishing to RabbitMQ fails?

Then your database says:

Product was created.

But RabbitMQ never receives:

ProductCreatedEvent

Your system becomes inconsistent.

What you should say in an interview

Use this as your compact answer:

In C#, async/await is mainly for non-blocking I/O. I avoid .Result and .Wait() because they block thread-pool threads and can cause deadlocks or thread-pool starvation. I propagate CancellationToken through every async call so the caller can cancel work cooperatively. For concurrent operations I use Task.WhenAll, and if I need all exceptions I inspect the combined task’s Exception.InnerExceptions. I avoid async void except for event handlers. For high-volume async work, I use bounded concurrency with SemaphoreSlim, ch