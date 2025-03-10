using System.Data.Common;
using Dapper;
using MediatR;
using MiniProject.Modules.Events.Application.Abstraction;

namespace MiniProject.Modules.Events.Application.Events.GetEvent;
internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetEventQuery, EventResponse?>
{
    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $""""
            SELECT
                id AS {nameof(EventResponse.Id)},
                title AS {nameof(EventResponse.Title)},
                description AS {nameof(EventResponse.Description)},
                location AS {nameof(EventResponse.Location)}
             FROM "events-3".events
              WHERE id = @EventId
            """";

        EventResponse? response = await connection.QuerySingleOrDefaultAsync<EventResponse>(sql, request);

        return response;
    }
}
