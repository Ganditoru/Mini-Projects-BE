namespace MiniProject.Common.Messaging.Contracts.User;

public interface IHttpUsersApi
{
    Task<IEnumerable<CommonUserResponse>> GetUsersAsync(CancellationToken cancellationToken = default);
}
