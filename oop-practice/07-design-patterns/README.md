# Design Patterns — GoF

Three categories. One or more patterns per session.

| Category | Folder | Examples |
|----------|--------|---------|
| Creational | `creational/` | Factory, Abstract Factory, Builder, Singleton |
| Structural | `structural/` | Adapter, Decorator, Facade, Proxy |
| Behavioural | `behavioural/` | Strategy, Observer, Command, Repository |

Start with patterns you are most likely to see in interviews:
1. **Factory Method** — how do you create objects without knowing the exact type?
2. **Strategy** — how do you swap algorithms at runtime?
3. **Decorator** — how do you add behaviour without modifying a class?
4. **Repository** — how do you abstract data access?


## 1. Factory Method
    Interview definition

    Factory Method is a creational pattern that lets a class create objects through a method instead of directly using new, so the exact type can be chosen without changing the calling code.
    The problem

    Imagine this:
    ```
    public class NotificationService
    {
        public void Send(string type, string message)
        {
            if (type == "email")
            {
                var sender = new EmailSender();
                sender.Send(message);
            }
            else if (type == "sms")
            {
                var sender = new SmsSender();
                sender.Send(message);
            }
        }
    }
    ```
    This works, but it has problems:
    ```
    new EmailSender();
    new SmsSender();
    ```
    NotificationService is responsible for:

    Deciding which sender to create
    Creating the sender
    Sending the notification

    That violates SRP and OCP.

    If we add PushNotificationSender, we must modify NotificationService.



## 2. Strategy Pattern
Interview definition

Strategy is a behavioural pattern that lets you define a family of algorithms, put each one in a separate class, and swap them at runtime.

The problem

Imagine this:
```
public class PriceCalculator
{
    public decimal Calculate(string customerType, decimal price)
    {
        if (customerType == "regular")
        {
            return price;
        }

        if (customerType == "premium")
        {
            return price * 0.9m;
        }

        if (customerType == "vip")
        {
            return price * 0.8m;
        }

        throw new ArgumentException("Unknown customer type");
    }
}
```

This is the same problem we saw with OCP.

Every time we add a new discount rule, we modify PriceCalculator.

