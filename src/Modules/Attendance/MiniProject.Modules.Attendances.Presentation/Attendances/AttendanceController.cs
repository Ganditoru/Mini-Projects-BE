using Microsoft.AspNetCore.Mvc;
using MiniProject.Modules.Users.PublicAPI;

namespace MiniProject.Modules.Attendances.Presentation.Attendances;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController(IUserApi userApi) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttendance(Guid id)
    {
        PublicApiUserResponse? user = await userApi.GetAsync(id);

        return Ok(user);
    }
}
