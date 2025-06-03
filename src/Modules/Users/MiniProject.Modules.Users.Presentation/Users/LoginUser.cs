using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Users.Application.Users.Login;

namespace MiniProject.Modules.Users.Presentation.Users;
internal static class LoginUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async ([FromBody] LoginRequest request, ISender sender) =>
        {
            string? token = await sender.Send(new LoginCommand(request.Email));

            if(token is null)
            {
                return Results.BadRequest("Email not found");
            }

            return Results.Ok(token);
        })
            .WithTags(Tags.User);
    }
}
