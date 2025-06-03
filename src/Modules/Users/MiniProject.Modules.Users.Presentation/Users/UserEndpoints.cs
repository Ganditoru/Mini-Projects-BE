using Microsoft.AspNetCore.Routing;

namespace MiniProject.Modules.Users.Presentation.Users;
public static  class UserEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        RegisterUser.MapEndpoint(app);
        GetUser.MapEndpoint(app);
        GetUsers.MapEndpoint(app);
        LoginUser.MapEndpoint(app);
    }
}
