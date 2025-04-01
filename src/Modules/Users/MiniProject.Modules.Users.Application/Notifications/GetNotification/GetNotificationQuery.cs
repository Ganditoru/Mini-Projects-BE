using MediatR;

namespace MiniProject.Modules.Users.Application.Notifications.GetNotification;

public sealed record GetNotificationQuery(Guid NotificationId) : IRequest<NotificationResponse?>;
