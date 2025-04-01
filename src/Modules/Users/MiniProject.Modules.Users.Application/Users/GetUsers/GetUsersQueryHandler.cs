using MiniProject.Modules.Events.Application.Abstractions.Data;
using System.Data.Common;
using MediatR;
using Dapper;

namespace MiniProject.Modules.Users.Application.Users.GetUsers;
internal sealed class GetUsersQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetUsersQuery, IReadOnlyCollection<UserResponse>>
{
    public async Task<IReadOnlyCollection<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
                SELECT
                    id as {nameof(UserResponse.Id)},
                    name as {nameof(UserResponse.Name)},
                    email as {nameof(UserResponse.Email)},
                    user_name as {nameof(UserResponse.UserName)},
                    password as {nameof(UserResponse.Password)}
                FROM user2.users
            """;

        List<UserResponse> userResponse = (await connection.QueryAsync<UserResponse>(sql, request)).AsList();

        return userResponse;
    }
}
