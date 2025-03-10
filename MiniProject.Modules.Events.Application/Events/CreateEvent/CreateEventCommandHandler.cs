using MediatR;
using MiniProject.Modules.Events.Application.Abstraction;
using MiniProject.Modules.Events.Domain.Events;

namespace MiniProject.Modules.Events.Application.Events.CreateEvent;
internal sealed class CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            Location = request.Location,   
            Title = request.Title
        };

        eventRepository.Insert(@event);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return @event.Id;
    }
}
