using MiniProject.Modules.Events.Application.TicketTypes.GetTicketType;
using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.TicketTypes.GetTicketTypes;
public sealed record GetTicketTypesQuery(Guid EventId) : IRequest<Result<IReadOnlyCollection<TicketTypeResponse>>>;

