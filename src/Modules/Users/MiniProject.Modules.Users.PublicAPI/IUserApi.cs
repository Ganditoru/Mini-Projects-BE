

using MediatR;
using MiniProject.Modules.Users.Application.Users;
using MiniProject.Modules.Users.Application.Users.GetUser;

namespace MiniProject.Modules.Users.PublicAPI;

public interface IUserApi
{
    Task<PublicApiUserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
}

public sealed record PublicApiUserResponse(Guid Id, string Name, string UserName, string Email);
