# Behavioural Patterns

Focus: **Strategy** and **Observer** in the Product Catalogue context.

## Strategy Task
Implement different discount strategies (`NoDiscount`, `PercentageDiscount`, `BulkDiscount`)
and apply them to `Product` without a `switch` statement.

## Observer Task
When a product's stock drops below a threshold, notify registered observers
(e.g., a `LowStockEmailNotifier`, a `LowStockLogNotifier`).
