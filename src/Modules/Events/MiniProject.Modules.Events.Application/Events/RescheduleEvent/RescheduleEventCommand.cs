
using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Events.RescheduleEvent;
public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : IRequest<Result>;

