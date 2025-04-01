using MiniProject.Modules.Users.Domain.Notifications;
using MiniProject.Modules.Users.Infrastructure.Database;

namespace MiniProject.Modules.Users.Infrastructure.Notifications;

internal sealed class NotificationRepository(UserDbContext context) : INotificationRepository
{
    public void Insert(Notification notification)
    {
        context.Notifications.Add(notification);
    }
}
