using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Authentification.Application.Users.GetUsers;
using MiniProject.Modules.Authentification.Application.Users.Models;

namespace MiniProject.Modules.Authentification.Presentation.Users;
internal static class GetUsers
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events", async (ISender sender) =>
        {
            IReadOnlyCollection<UserResponse> response = await sender.Send(new GetUsersQuery());

            return Results.Ok(response);
        })
            .WithTags(Tags.Auth);
    }

}
