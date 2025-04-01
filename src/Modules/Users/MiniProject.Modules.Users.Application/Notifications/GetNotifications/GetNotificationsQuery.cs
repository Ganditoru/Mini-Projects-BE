using MediatR;

namespace MiniProject.Modules.Users.Application.Notifications.GetNotifications;
public sealed record GetNotificationsQuery() : IRequest<IEnumerable<NotificationResponse>>;
