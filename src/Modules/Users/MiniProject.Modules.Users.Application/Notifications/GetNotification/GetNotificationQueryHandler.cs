using System.Data.Common;
using Dapper;
using MediatR;
using MiniProject.Modules.Events.Application.Abstractions.Data;

namespace MiniProject.Modules.Users.Application.Notifications.GetNotification;

internal sealed class GetNotificationQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetNotificationQuery, NotificationResponse?>
{
    public async Task<NotificationResponse?> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
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
                WHERE id = @NotificationId
            ";

        NotificationResponse? notificationResponse = await connection.QuerySingleOrDefaultAsync<NotificationResponse>(sql, request);
        return notificationResponse;
    }
}
