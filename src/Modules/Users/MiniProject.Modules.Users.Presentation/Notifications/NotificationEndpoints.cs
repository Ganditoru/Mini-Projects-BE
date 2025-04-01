using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Presentation.Users;

namespace MiniProject.Modules.Users.Presentation.Notifications;

public static class NotificationEndpoints
{

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        AddNotificationToUser.MapEndpoint(app);
        GetNotification.MapEndpoint(app);
        GetNotifications.MapEndpoint(app);
        GetNotificationsByUser.MapEndpoint(app);
    }
}
