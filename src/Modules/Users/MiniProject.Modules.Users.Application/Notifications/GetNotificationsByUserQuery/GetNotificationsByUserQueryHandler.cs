using System.Data.Common;
using Dapper;
using MediatR;
using MiniProject.Modules.Events.Application.Abstractions.Data;

namespace MiniProject.Modules.Users.Application.Notifications.GetNotificationsByUserQuery;

internal sealed class GetNotificationsByUserQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetNotificationsByUserQuery, IEnumerable<NotificationResponse>>
{
    public async Task<IEnumerable<NotificationResponse>> Handle(GetNotificationsByUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $@"
                SELECT
                    id as {nameof(NotificationResponse.Id)},
                    title as {nameof(NotificationResponse.Title)},
                    message as {nameof(NotificationResponse.Message)},
                    created_at as {nameof(NotificationResponse.CreatedAt)}
                FROM user2.notifications
                WHERE user_id = @UserId
            ";

        IEnumerable<NotificationResponse> notifications = await connection.QueryAsync<NotificationResponse>(sql, request);
        return notifications;
    }
}
