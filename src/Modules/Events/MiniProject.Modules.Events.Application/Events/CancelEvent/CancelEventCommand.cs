using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Events.CancelEvent;
public sealed record CancelEventCommand(Guid EventId) : IRequest<Result>;

