using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Authentification.Application.Users.GetUser;
using MiniProject.Modules.Authentification.Application.Users.Models;

namespace MiniProject.Modules.Authentification.Presentation.Users;
internal static class GetUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("event/{id}", async (Guid id, ISender sender) =>
        {
            UserResponse response = await sender.Send(new GetUserQuery(id));

            return response is null ? Results.NotFound() : Results.Ok(response);
        })
            .WithTags(Tags.Auth);
    }

}
