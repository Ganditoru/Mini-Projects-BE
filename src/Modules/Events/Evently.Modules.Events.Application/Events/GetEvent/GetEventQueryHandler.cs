using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvent;
internal sealed class GetEventQueryHandler(IEventRepository eventRepository) : IRequestHandler<GetEventQuery, EventResponse?>
{
    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        Event? eventEntity = await eventRepository.GetById(request.eventId);

        if (eventEntity is null)
        {
            throw new KeyNotFoundException($"Event with ID {request.eventId} not found.");
        }

        return new EventResponse(
             eventEntity.Id,
             eventEntity.Title,
             eventEntity.Description,
             eventEntity.Location
        );
    }
}
