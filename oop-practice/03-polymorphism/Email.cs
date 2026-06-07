List<Notification> notifications = new()
{
        new EmailNotification(),
    new SmsNotification(),
    new PushNotification()
    };

public class Notification
{
    public virtual void Send()
    {
        Console.WriteLine("Sending a generic notification...");
    }
}