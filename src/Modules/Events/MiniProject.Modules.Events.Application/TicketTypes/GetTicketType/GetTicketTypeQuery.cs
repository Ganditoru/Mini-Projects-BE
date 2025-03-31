
using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.TicketTypes.GetTicketType;
public sealed record GetTicketTypeQuery(Guid TicketTypeId) : IRequest<Result<TicketTypeResponse>>;

