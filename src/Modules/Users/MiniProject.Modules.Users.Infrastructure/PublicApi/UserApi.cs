using MediatR;
using MiniProject.Modules.Users.Application.Users.GetUser;
using MiniProject.Modules.Users.Application.Users;
using MiniProject.Modules.Users.PublicApi;
using MiniProject.Modules.Users.Application.Users.GetUsers;

namespace MiniProject.Modules.Users.Infrastructure.PublicApi;
internal sealed class UserApi(ISender sender) : IUserPublicApi
{
    public async Task<PublicApiUserResponse?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        UserResponse response = await sender.Send(new GetUserQuery(userId), cancellationToken);

        return response is null ? null : new PublicApiUserResponse(response.Id, response.Name, response.UserName, response.Email);
    }

    public async Task<IReadOnlyCollection<PublicApiUserResponse>> GetUsers(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<UserResponse>? response = await sender.Send(new GetUsersQuery(), cancellationToken);

        if (response is null)
        {
            return null;
        }

        return [.. response.Select(u => new PublicApiUserResponse(u.Id, u.Name, u.UserName, u.Email))];
    }
}
