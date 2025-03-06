using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Authentification.Presentation.Users;

namespace MiniProject.Modules.Authentification.Presentation.User;
public static  class UserEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        RegisterUser.MapEndpoint(app);
        GetUser.MapEndpoint(app);
        GetUsers.MapEndpoint(app);
    }
}
