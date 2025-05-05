namespace MiniProject.Modules.Users.PublicApi;

public interface IUserPublicApi
{
    Task<IReadOnlyCollection<PublicApiUserResponse>> GetUsers(CancellationToken cancellationToken = default);

    Task<PublicApiUserResponse?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}

public sealed record PublicApiUserResponse(Guid Id, string Name, string UserName, string Email);
