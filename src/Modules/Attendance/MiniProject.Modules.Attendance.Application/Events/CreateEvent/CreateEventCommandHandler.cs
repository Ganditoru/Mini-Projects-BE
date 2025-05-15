using MediatR;
using MiniProject.Modules.Attendance.Application.Abstract;
using MiniProject.Modules.Attendance.Domain.Abstract;
using MiniProject.Modules.Attendance.Domain.Events;

namespace MiniProject.Modules.Attendance.Application.Events.CreateEvent;
internal sealed class CreateEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateEventCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        Result<Event> result = Event.Create(
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        eventRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}

