using MediatR;

namespace MiniProject.Modules.Users.Application.Notifications.GetNotificationsByUserQuery;

public sealed record GetNotificationsByUserQuery(Guid UserId) : IRequest<IEnumerable<NotificationResponse>>;
