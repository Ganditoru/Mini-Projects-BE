using MiniProject.Common.Messaging.Contracts.Dto;

namespace MiniProject.Common.Messaging.Contracts.Http;

public interface IHttpUsersApi
{
    Task<IEnumerable<CommonUserResponse>> GetUsersAsync(CancellationToken cancellationToken = default);
}
