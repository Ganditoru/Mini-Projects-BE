namespace MiniProject.Common.Messaging.Contracts.User;

public interface IUserApi
{
    Task<PublicApiUserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
}

public sealed record PublicApiUserResponse(Guid Id, string Name, string UserName, string Email);
