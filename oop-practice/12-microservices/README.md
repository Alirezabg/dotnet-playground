# Topic 12: Microservices Patterns

## Elekta Relevance
Elekta builds a domain-based service-oriented platform. Services are independently deployable, communicate via REST and message queues, and run in Docker containers (Kubernetes-managed). This topic focuses on the design patterns you need to discuss confidently.

## Concepts to Master
- Service boundaries — one bounded context per service
- Synchronous communication — REST / HTTP (request/response)
- Asynchronous communication — RabbitMQ messages / events
- Idempotency — processing the same message twice must be safe
- Anti-Corruption Layer (ACL) — translating between service models
- Outbox Pattern — reliable event publishing with a database
- Saga / Choreography — coordinating multi-step work across services
- Strangler Fig — incremental migration from a monolith
- Health checks and readiness probes — Kubernetes basics

## Your Tasks (implement in this folder)

### Task A — Service Boundary Design
Sketch (in comments or a markdown table) where the boundary should be between:
- Product Catalogue service
- Inventory service
- Pricing service

For each boundary answer:
- What data does each service own?
- What does each service expose via REST?
- What events does each service publish?

### Task B — Idempotent Message Consumer
Define an `IMessageConsumer<T>` interface and sketch a `ProductCreatedConsumer`:
- The handler must check: "have I already processed this message ID?"
- Must ack/nack appropriately (conceptual — no RabbitMQ SDK needed)

Before coding, answer:
- Why does "at least once delivery" require idempotency?
- Where would you store the processed-message-IDs — and what is the cost?

### Task C — Anti-Corruption Layer
Two services share the concept of "Product" but model it differently:
- Product Catalogue: `Product { Id, Name, Description, Price, CategoryId }`
- Inventory: `StockItem { Sku, ProductReference, QuantityOnHand }`

Implement a simple `ProductCatalogueAcl` that translates a `Product` event into an `InventoryProductReference` used by the Inventory service.

Before coding, answer:
- Why should the Inventory service NOT directly consume the Product Catalogue's model?
- What breaks when you skip the ACL?

### Task D — Health Checks (stretch)
Add a simple ASP.NET Core health-check endpoint to `src/ProductCatalogue.Api/`:
- `/health` returns 200 when the service is healthy
- Think about what "healthy" means for a service with a database dependency

Before coding, answer:
- What is the difference between a liveness probe and a readiness probe in Kubernetes?
- Who calls these endpoints — and when?

## Interview Questions Elekta May Ask
- "How do you decide where to draw a service boundary?"
- "What is the difference between choreography and orchestration in a saga?"
- "If a service is unavailable, how does your caller handle it?"
- "What does it mean to deploy a service independently?"
- "How would you migrate one feature from a monolith to a microservice without downtime?"
