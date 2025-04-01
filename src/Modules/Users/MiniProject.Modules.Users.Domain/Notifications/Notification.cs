namespace MiniProject.Modules.Users.Domain.Notifications;
public sealed class Notification
{
    private Notification() { }
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime NotificationDate { get; private set; }

    public static Notification Create(string title, string description)
    {
        return new Notification
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            NotificationDate = DateTime.UtcNow
        };
    }
}
