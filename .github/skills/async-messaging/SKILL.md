# Async & Messaging — Practice Skill

## Purpose
Guide the student through async/await patterns and event-driven messaging as used in .NET microservices — specifically the patterns Elekta employs with RabbitMQ and an async-first service architecture.

## Session Start Checklist
1. Read `progress-notes/async-progress.md` to understand what has been covered
2. Ask: "Which async or messaging concept is least clear to you right now?"
3. Remind the user: in event-driven systems, failure paths are as important as happy paths

## Topic Guide

### async / await Fundamentals
- `Task` represents work that will complete in the future
- `await` yields control without blocking a thread
- Questions to ask:
  - "What is the difference between `Task.Run` and an async I/O call?"
  - "Why should you never use `.Result` or `.Wait()` in an async context?"
  - "What is a deadlock in async code and when can it occur?"

### CancellationToken
- Cooperative cancellation — producer signals, consumer checks
- Questions to ask:
  - "How do you propagate a CancellationToken through five nested async calls?"
  - "What should a method do when it receives a cancelled token?"
  - "How does this relate to a user clicking 'Cancel' in a medical imaging workflow?"

### Task Composition
- `Task.WhenAll` — wait for all, fail fast on first exception
- `Task.WhenAny` — return as soon as the first completes
- `IAsyncEnumerable<T>` — async streaming (e.g. reading a large result set)
- Task: Write a method that fetches products from two sources in parallel and merges results
- Questions to ask:
  - "If one source throws, what happens to the other Task?"
  - "When would streaming with `IAsyncEnumerable` beat loading everything at once?"

### async void — The Anti-Pattern
- `async void` cannot be awaited and exceptions crash the process
- Acceptable only in event handlers
- Questions to ask:
  - "How would you test a method that is `async void`?"
  - "What alternative would you use for fire-and-forget with error handling?"

### RabbitMQ — Consumer Pattern
- A consumer is a long-running async loop: receive, handle, ack/nack
- Questions to ask:
  - "What is the difference between ack and nack in RabbitMQ?"
  - "How do you prevent one slow message from blocking all others?"
  - "What is a dead-letter queue and when do you use one?"
- Task: Sketch a `ProductCreatedConsumer` class with an async handle method. Do not implement it — describe the shape.

### RabbitMQ — Publisher Pattern
- Publishing should be idempotent where possible
- Questions to ask:
  - "What happens if the publisher crashes after writing to the DB but before publishing?"
  - "How does the Outbox pattern solve this?"
  - "Where does the publishing code live — domain, application, or infrastructure?"

### Error Handling in Async Code
- `try/catch` around `await` works correctly
- Aggregate exceptions from `Task.WhenAll`
- Questions to ask:
  - "How do you log the full exception when `Task.WhenAll` throws an `AggregateException`?"
  - "What retry strategy is appropriate for a transient messaging failure?"

## Feedback Format
```
✓ What works well:
  ...

⚠ What to reconsider:
  ...

? Guiding question:
  ...
```

## Elekta Interview Tips
> - "Walk me through what happens when an async method throws" — stack trace, exception propagation.
> - "How do you ensure a message is processed exactly once?" — idempotency key, check before processing.
> - "What is back-pressure and how do you apply it in .NET?" — channel, bounded queues, `SemaphoreSlim`.
> - Safety-critical angle: "If a domain event is dropped, what is the consequence?" — design for resilience.
