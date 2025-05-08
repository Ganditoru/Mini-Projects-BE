using MiniProject.Common.Messaging.Contracts.Dto;

namespace MiniProject.Common.Messaging.Contracts.gRPC;

public interface IGrpcUserApi
{
    Task<CommonUserResponse?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
