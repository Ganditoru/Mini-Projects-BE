using MediatR;
using MiniProject.Modules.Users.Application.Users.GetUser;
using MiniProject.Modules.Users.Application.Users;
using MiniProject.Common.Messaging.Contracts.User;

namespace MiniProject.Modules.Users.Infrastructure.PublicApi;
internal sealed class UserApi(ISender sender) : IUserApi
{
    public async Task<PublicApiUserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        UserResponse response = await sender.Send(new GetUserQuery(userId), cancellationToken);

        return response is null ? null : new PublicApiUserResponse(response.Id, response.Name, response.UserName, response.Email);
    }
}
