using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Application.Notifications;
using MiniProject.Modules.Users.Application.Notifications.GetNotification;

namespace MiniProject.Modules.Users.Presentation.Notifications;

internal static class GetNotification
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("notification/{id}", async (Guid id, ISender sender) =>
        {
            NotificationResponse response = await sender.Send(new GetNotificationQuery(id));

            return response is null ? Results.NotFound() : Results.Ok(response);
        })
            .WithTags(Tags.Notification);
    }
}
