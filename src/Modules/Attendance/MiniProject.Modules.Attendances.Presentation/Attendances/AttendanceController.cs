using Microsoft.AspNetCore.Mvc;
using MiniProject.Common.Messaging.Contracts.Dto;
using MiniProject.Common.Messaging.Contracts.gRPC;
using MiniProject.Common.Messaging.Contracts.Http;
namespace MiniProject.Modules.Attendances.Presentation.Attendances;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController(IHttpUsersApi httpUserApi, IGrpcUserApi grpcUserApi) : ControllerBase
{

    [HttpGet("users/gRPC/{id}")]
    public async Task<IActionResult> GetAttendanceViaGRPC(Guid id)
    {
        CommonUserResponse? user = await grpcUserApi.GetUserAsync(id);

        return Ok(user);
    }

    [HttpGet("users/http")]
    public async Task<IActionResult> GetAttendance()
    {
        IEnumerable<CommonUserResponse> users = await httpUserApi.GetUsersAsync();
        return Ok(users);
    }
}
