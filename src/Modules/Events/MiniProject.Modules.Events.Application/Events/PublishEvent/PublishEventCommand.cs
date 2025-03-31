using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Events.PublishEvent;
public sealed record PublishEventCommand(Guid EventId) : IRequest<Result>;
