using MiniProject.Modules.Users.Domain.Notifications;

namespace MiniProject.Modules.Users.Domain.Users;

public sealed class User
{
    private User() { }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string UserName { get; private set; }
    public ICollection<Notification> Notifications { get; private set; } = new List<Notification>();

    public static User Create(string name, string email, string username, string password)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            UserName = username,
            Password = password
        };

        return user;
    }
    public void AddNotification(Notification notification)
    {
        Notifications.Add(notification);
    }
}

