using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IRequest<Result<EventResponse>>;
