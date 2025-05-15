using MediatR;
using MiniProject.Modules.Attendance.Application.Abstract;
using MiniProject.Modules.Attendance.Domain.Abstract;
using MiniProject.Modules.Attendance.Domain.Events;

namespace MiniProject.Modules.Attendance.Application.Events.CompensateEventCommand;
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
