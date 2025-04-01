using MediatR;

namespace MiniProject.Modules.Users.Application.Notifications.AddNotificationToUserCommand;

public sealed record AddNotificationToUserCommand(Guid UserId, string Title, string Description): IRequest<Guid>;
