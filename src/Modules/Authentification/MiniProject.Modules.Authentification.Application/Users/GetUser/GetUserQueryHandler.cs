using System.Data.Common;
using Dapper;
using MiniProject.Modules.Events.Application.Abstractions.Data;
using MediatR;

namespace MiniProject.Modules.Authentification.Application.Users.GetUser;
internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetUserQuery, UserResponse?>
{
    public async Task<UserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
                SELECT
                    id as { nameof(UserResponse.Id) },
                    name as { nameof(UserResponse.Name) },
                    email as {  nameof(UserResponse.Email) },
                    user_name as { nameof(UserResponse.UserName)},
                    password as { nameof(UserResponse.Password)}
                FROM auth.users
                WHERE id = @UserId
            """;

        UserResponse? userResponse = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        return userResponse;
    }
}
