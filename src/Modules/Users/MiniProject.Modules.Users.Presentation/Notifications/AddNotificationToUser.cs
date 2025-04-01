using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Application.Notifications.AddNotificationToUserCommand;

namespace MiniProject.Modules.Users.Presentation.Notifications;

internal static class AddNotificationToUser
{

    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user/notification", async ([FromBody] AddNotificationRequest request, ISender sender) =>
        {
            Guid response = await sender.Send(new AddNotificationToUserCommand(request.UserId, request.Title, request.Description));

            return response == Guid.Empty ? Results.NotFound() : Results.Ok(response);
        })
            .WithTags(Tags.Notification);
    }

    internal sealed class AddNotificationRequest
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
