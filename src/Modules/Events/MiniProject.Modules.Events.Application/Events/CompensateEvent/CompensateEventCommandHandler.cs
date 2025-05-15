
using MediatR;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Domain.Events;

namespace MiniProject.Modules.Events.Application.Events.CompensateEvent;
internal sealed class CompensateEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CompensateEventCommand, Result>
{
    public async Task<Result> Handle(CompensateEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);
        if (@event is null)
        {
            return Result.Failure($"Event {request.EventId} not found.");
        }

        eventRepository.Delete(@event);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
