using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Application.Notifications;
using MiniProject.Modules.Users.Application.Notifications.GetNotifications;

namespace MiniProject.Modules.Users.Presentation.Notifications;

internal static class GetNotifications
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("notifications", async (ISender sender) =>
        {
            IEnumerable<NotificationResponse> response = await sender.Send(new GetNotificationsQuery());

            return response is null ? Results.NotFound() : Results.Ok(response);
        })
            .WithTags(Tags.Notification);
    }
}
