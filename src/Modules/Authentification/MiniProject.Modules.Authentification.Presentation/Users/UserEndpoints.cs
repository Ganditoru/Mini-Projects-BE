using Microsoft.AspNetCore.Routing;

namespace MiniProject.Modules.Authentification.Presentation.User;
public static  class UserEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        RegisterUser.MapEndpoint(app);
    }
}
