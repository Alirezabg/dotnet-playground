# S — Single Responsibility Principle

**In your own words:**
> _Your answer here..._

## Task
You are given a `ProductService` class that:
- Fetches products from a repository
- Validates the product data
- Formats products for display
- Logs operations

Your job: identify which responsibilities violate SRP, then refactor by splitting them.

## Interview Tip
> SRP is not "one method per class" — it's "one reason to change."
> Know the difference between cohesion and coupling.


A class should have one main job and therefore one reason to change.
A class can have many methods, as long as those methods support the same responsibility.


Cohesion

Cohesion means:

How closely related the code inside a class is.

A class with high cohesion has methods/properties that all belong together.


public class ProductValidator
{
    public bool IsValid(Product product) { }
    public bool HasValidName(Product product) { }
    public bool HasValidPrice(Product product) { }
}


Coupling

Coupling means:

How dependent one class is on another class.

Bad coupling:

public class ProductService
{
    private readonly SqlProductRepository _repository = new SqlProductRepository();
}

This is tightly coupled because ProductService directly depends on SqlProductRepository.

Better:

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
}


The original ProductService violates SRP because it has several reasons to change: repository access, validation rules, display formatting, and logging. I would split those into separate classes such as IProductRepository, IProductValidator, IProductFormatter, and ILogger. Then ProductService only coordinates the workflow. This improves cohesion because each class has a focused purpose, and it reduces coupling because the service depends on interfaces instead of concrete implementations.