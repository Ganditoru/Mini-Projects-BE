using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Modules.Users.PublicApi;

namespace MiniProject.Modules.Events.Presentation.Controllers;

[ApiController]
[Route("api/Events/[controller]")]
[Tags("Events")]
public class UserController(IUserPublicApi userPublicApi) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PublicApiUserResponse>>> GetUsers(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<PublicApiUserResponse> users = await userPublicApi.GetUsers(cancellationToken);

        return Ok(users);
    }
}
