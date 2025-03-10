using MediatR;

namespace Evently.Modules.Events.Application.Events.CreateEvent;
public sealed record CreateEventCommand(Guid id, string Title, string Description, string Location): IRequest<Guid>;
