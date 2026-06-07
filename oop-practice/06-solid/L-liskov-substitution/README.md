# L — Liskov Substitution Principle

**In your own words:**
> _Your answer here..._

## Task
A `ReadOnlyProduct` extends `Product` but throws `NotSupportedException` on `SetPrice()`.
Show why this violates LSP and fix it.

## Interview Tip
> LSP violations often show up as `NotImplementedException` or surprising `throw` in overrides.
> Know: subtypes must honour the contract of the base type.



A child class should be usable anywhere the parent class is expected, without breaking the program or surprising the caller.
If code works with Product, it should also work with any class that inherits from Product.

The Problem

Imagine we have this base class:

```
public class Product
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public virtual void SetPrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");

        Price = price;
    }
}
```

Now someone creates this:

```
public class ReadOnlyProduct : Product
{
    public ReadOnlyProduct(string name, decimal price)
        : base(name, price)
    {
    }

    public override void SetPrice(decimal price)
    {
        throw new NotSupportedException("This product is read-only.");
    }
}
```

At first, this looks okay because ReadOnlyProduct is a kind of Product.

But it violates LSP.


Why This Violates LSP

This code expects any Product to allow price changes:

```
public class PriceUpdater
{
    public void ApplyDiscount(Product product)
    {
        product.SetPrice(product.Price * 0.9m);
    }
}
````

This works fine:

```
Product product = new Product("Laptop", 1000m);

var updater = new PriceUpdater();
updater.ApplyDiscount(product);
```


But this breaks:

```
Product product = new ReadOnlyProduct("Archived Laptop", 1000m);

var updater = new PriceUpdater();
updater.ApplyDiscount(product); // Throws NotSupportedException

```

The problem is that PriceUpdater trusted the contract of Product.

The contract says:
```
product.SetPrice(...)
```

should update the price.

But ReadOnlyProduct changes that behaviour and throws instead.

That means ReadOnlyProduct cannot safely replace Product.

So this breaks LSP.


### Bad Design

public class Product
{
    public decimal Price { get; private set; }

    public virtual void SetPrice(decimal price)
    {
        Price = price;
    }
}

public class ReadOnlyProduct : Product
{
    public override void SetPrice(decimal price)
    {
        throw new NotSupportedException();
    }
}



Interview Explanation

You can say:

This violates LSP because ReadOnlyProduct inherits from Product, but it cannot honour the behaviour promised by Product. Code that expects a Product can call SetPrice(), but when given a ReadOnlyProduct, the method unexpectedly throws NotSupportedException. So ReadOnlyProduct is not safely substitutable for Product.

Then say the fix:

I would fix this by separating read-only behaviour from writable behaviour. A base IProduct interface should expose common read-only data like Name and Price, and a separate IPriceEditableProduct interface should expose SetPrice(). Then only editable products implement SetPrice().

# Simple Memory Hook

## LSP means:

A child class should not make the parent class less usable.

## Or even simpler:

Do not inherit just to throw exceptions.