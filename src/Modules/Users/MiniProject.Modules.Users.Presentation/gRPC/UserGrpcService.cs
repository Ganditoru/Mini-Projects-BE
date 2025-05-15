using Grpc.Core;
using MiniProject.Modules.Users.Application.Users.GetUser;
using MiniProject.Modules.Users.Application.Users;
using MiniProjects.Common.Messaging.Contracts.gRPC;
using MediatR;

namespace MiniProject.Modules.Users.Presentation.gRPC;
public sealed class UserGrpcService(ISender sender) : UserService.UserServiceBase
{
    public override async Task<GetUserResponse> GetUser(
        GetUserRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.UserId, out Guid userId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid GUID"));
        }

        UserResponse response = await sender.Send(new GetUserQuery(Guid.Parse(request.UserId)));

        if (response is null)
        {

            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }

        return new GetUserResponse
        {
            UserId = response.Id.ToString(),
            Name = response.Name,
            Email = response.Email,
            UserName = response.UserName
        };
    }
}
