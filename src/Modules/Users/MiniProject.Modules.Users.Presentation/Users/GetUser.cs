using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Application.Users;
using MiniProject.Modules.Users.Application.Users.GetUser;

namespace MiniProject.Modules.Users.Presentation.Users;
internal static class GetUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user/{id}", async (Guid id, ISender sender) =>
        {
            UserResponse response = await sender.Send(new GetUserQuery(id));

            return response is null ? Results.NotFound() : Results.Ok(response);
        })
            .WithTags(Tags.User);
    }

}
