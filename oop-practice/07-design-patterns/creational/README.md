# Creational Patterns

Focus: **Factory Method** and **Builder** in the Product Catalogue context.

## Factory Method Task
Create a `ProductFactory` that returns the correct product type (`PhysicalProduct` or `DigitalProduct`)
based on a `ProductType` enum — without the caller knowing which type is created.

## Builder Task
Use the Builder pattern to construct a `Product` with many optional fields
(tags, metadata, images) without telescoping constructors.
