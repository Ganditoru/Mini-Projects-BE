
using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events.CreateEvent;
internal sealed class CreateEventCommandHandler(IEventRepository eventRepository) : IRequestHandler<CreateEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Location = request.Location
        };

        await eventRepository.Insert(newEvent);

        return newEvent.Id;
    }
}
