using System.Data.Common;
using Dapper;
using MediatR;
using MiniProject.Modules.Events.Application.Abstractions.Data;

namespace MiniProject.Modules.Users.Application.Notifications.GetNotifications;

internal sealed class GetNotificationsQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetNotificationsQuery, IEnumerable<NotificationResponse>>
{
    public async Task<IEnumerable<NotificationResponse>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
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
            ";

        IEnumerable<NotificationResponse> notifications = await connection.QueryAsync<NotificationResponse>(sql, request);
        return notifications;
    }
}
