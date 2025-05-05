using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Common.Messaging.Contracts.User;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Attendances.Presentation.Attendances;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController(IHttpUsersApi httpUserApi, IGrpcUserApi grpcUserApi) : ControllerBase
{

    [HttpGet("gRPC/{id}")]
    public async Task<IActionResult> GetAttendanceViaGRPC(Guid id)
    {
        CommonUserResponse? user = await grpcUserApi.GetUserAsync(id);

        return Ok(user);
    }

    [HttpGet("http")]
    public async Task<IActionResult> GetAttendance()
    {
        IEnumerable<CommonUserResponse> users = await httpUserApi.GetUsersAsync();
        return Ok(users);
    }
}
