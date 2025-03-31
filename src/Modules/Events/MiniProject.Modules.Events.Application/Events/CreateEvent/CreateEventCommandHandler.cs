
using MediatR;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Domain.Categories;
using MiniProject.Modules.Events.Domain.Events;

namespace MiniProject.Modules.Events.Application.Events.CreateEvent;

public sealed class CreateEventCommandHandler(
    ICategoryRepository categoryRepository,
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateEventCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (request.StartsAtUtc < DateTime.UtcNow)
        {
            return Result.Failure<Guid>(EventErrors.StartDateInPast);
        }

        Category? category = await categoryRepository.GetAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(request.CategoryId));
        }

        Result<Event> result = Event.Create(
            category,
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
