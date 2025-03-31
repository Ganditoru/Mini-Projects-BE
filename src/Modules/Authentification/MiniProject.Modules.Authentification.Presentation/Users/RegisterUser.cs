using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Authentification.Application.Users.RegisterUser;

namespace MiniProject.Modules.Authentification.Presentation.Users;
internal static class RegisterUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("register", async (RegisterRequest request, ISender sender) =>
        {
            var command = new RegisterUserCommand(request.Name, request.Email, request.Password, request.UserName);
            Guid userId = await sender.Send(command);

            return Results.Ok(userId);
        })
            .WithTags(Tags.Auth);

    }

    internal sealed class RegisterRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
