using MediatR;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Domain.Events;
using MiniProject.Modules.Events.Domain.TicketTypes;

namespace MiniProject.Modules.Events.Application.TicketTypes.CreateTicketType;
internal sealed class CreateTicketTypeCommandHandler(
    IEventRepository eventRepository,
    ITicketTypeRepository ticketTypeRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTicketTypeCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure<Guid>(EventErrors.NotFound(request.EventId));
        }

        var ticketType = TicketType.Create(@event, request.Name, request.Price, request.Currency, request.Quantity);

        ticketTypeRepository.Insert(ticketType);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ticketType.Id;
    }
}
