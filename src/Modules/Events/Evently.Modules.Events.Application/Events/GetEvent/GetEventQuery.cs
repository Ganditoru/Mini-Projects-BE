using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvent;
public sealed record GetEventQuery(Guid eventId) : IRequest<EventResponse?>;

