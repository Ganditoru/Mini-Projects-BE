namespace MiniProject.Common.Messaging.Contracts.User;

public interface IGrpcUserApi
{
    Task<CommonUserResponse?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
