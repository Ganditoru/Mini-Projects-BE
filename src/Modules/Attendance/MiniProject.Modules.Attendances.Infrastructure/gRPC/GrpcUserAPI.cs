using Grpc.Core;
using MiniProject.Common.Messaging.Contracts.User;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Attendances.Infrastructure.gRPC;
public class GrpcUserApi(UserService.UserServiceClient grpc) : IGrpcUserApi
{

    public async Task<CommonUserResponse?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var request = new GetUserRequest { UserId = userId.ToString() };
        try
        {
            // pass the cancellationToken into the gRPC call
            GetUserResponse reply = await grpc.GetUserAsync(request, cancellationToken: cancellationToken);

            return new CommonUserResponse(
                Guid.Parse(reply.UserId),
                reply.Name,
                reply.UserName,
                reply.Email
            );
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return null;
        }
    }
}
