using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Application.Users;
using MiniProject.Modules.Users.Application.Users.GetUsers;

namespace MiniProject.Modules.Users.Presentation.Users;
internal static class GetUsers
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (ISender sender) =>
        {
            IReadOnlyCollection<UserResponse> response = await sender.Send(new GetUsersQuery());

            return Results.Ok(response);
        })
            .WithTags(Tags.User);
    }

}
