
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Application.Notifications;
using MiniProject.Modules.Users.Application.Notifications.GetNotificationsByUserQuery;

namespace MiniProject.Modules.Users.Presentation.Notifications;

public static class GetNotificationsByUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user/{userId}/notifications", async (Guid userId, ISender sender) =>
        {
            IEnumerable<NotificationResponse> response = await sender.Send(new GetNotificationsByUserQuery(userId));

            return response is null ? Results.NotFound() : Results.Ok(response);
        })
            .WithTags(Tags.Notification);
    }
}
