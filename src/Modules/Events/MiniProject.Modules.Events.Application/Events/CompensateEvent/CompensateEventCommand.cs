using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Events.CompensateEvent;
public sealed record CompensateEventCommand(Guid EventId): IRequest<Result>;
