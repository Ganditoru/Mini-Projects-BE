using MediatR;

namespace MiniProject.Modules.Events.Application.Events.CreateEvent;
public sealed record CreateEventCommand(
    string Title,
    string Description,
    string Location) : IRequest<Guid>;
