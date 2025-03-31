using System.Data.Common;
using Dapper;
using MiniProject.Modules.Events.Application.TicketTypes.GetTicketType;
using MediatR;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.TicketTypes.GetTicketTypes;
internal sealed class GetTicketTypesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IRequestHandler<GetTicketTypesQuery, Result<IReadOnlyCollection<TicketTypeResponse>>>
{
    public async Task<Result<IReadOnlyCollection<TicketTypeResponse>>> Handle(
        GetTicketTypesQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(TicketTypeResponse.Id)},
                 event_id AS {nameof(TicketTypeResponse.EventId)},
                 name AS {nameof(TicketTypeResponse.Name)},
                 price AS {nameof(TicketTypeResponse.Price)},
                 currency AS {nameof(TicketTypeResponse.Currency)},
                 quantity AS {nameof(TicketTypeResponse.Quantity)}
             FROM events2.ticket_types
             WHERE event_id = @EventId
             """;

        List<TicketTypeResponse> ticketTypes =
            (await connection.QueryAsync<TicketTypeResponse>(sql, request)).AsList();

        return ticketTypes;
    }
}
