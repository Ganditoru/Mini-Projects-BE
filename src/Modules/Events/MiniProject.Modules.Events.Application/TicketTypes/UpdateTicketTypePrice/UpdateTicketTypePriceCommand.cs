using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
public sealed record UpdateTicketTypePriceCommand(Guid TicketTypeId, decimal Price) : IRequest<Result>;

