
namespace MiniProject.Modules.Users.Application.Notifications;

public sealed record NotificationResponse(Guid Id, string Title, string Message, DateTime CreatedAt);
